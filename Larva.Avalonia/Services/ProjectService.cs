using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using FluentResults;
using Larva.Avalonia.Message;
using Larva.Avalonia.Models;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Larva.Avalonia.Services;

public sealed class ProjectService
{
    private readonly RecentProjectService recentProjectService;

    public ProjectService(RecentProjectService recentProjectService)
    {
        this.recentProjectService = recentProjectService;
    }

    public async Task CreateAsync(Project project)
    {
        WeakReferenceMessenger.Default.Send(new ProjectCreateMessage(project));

        var json = JsonSerializer.Serialize(project, new JsonSerializerOptions()
        {
            WriteIndented = true
        });

        await File.WriteAllTextAsync(project.Path, json);
        await recentProjectService.AddAsync(project);
    }

    public async Task<Result> OpenAsync(string path)
    {
        try
        {
            var content = await File.ReadAllTextAsync(path);

            try
            {
                var project = JsonConvert.DeserializeObject<Project>(content, new JsonSerializerSettings()
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                });

                if (project is null)
                {
                    return Result.Fail("An unknown error has occured.");
                }

                WeakReferenceMessenger.Default.Send(new ProjectCreateMessage(project));
                await recentProjectService.AddAsync(project);
                return Result.Ok();
            }
            catch (Exception)
            {
                return Result.Fail("The project file is invalid.");
            }
        }
        catch (FileNotFoundException)
        {
            return Result.Fail("Could not open the project file.");
        }
    }
}