using System;
using System.IO;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using FluentResults;
using Larva.Avalonia.Messages;
using Larva.Avalonia.Models;

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

        var xml = XmlSerializationService.Serialize(project);

        await File.WriteAllTextAsync(project.Path, xml);
        await recentProjectService.AddAsync(project);
    }

    public async Task<Result> SaveAsync(Project newProject)
    {
        try
        {
            var xml = XmlSerializationService.Serialize(newProject);
            await File.WriteAllTextAsync(newProject.Path, xml);
            
            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail("Could not find the file.");
        }
    }
    
    public async Task<Result<Project>> OpenAsync(string path)
    {
        try
        {
            var content = await File.ReadAllTextAsync(path);

            var project = XmlSerializationService.Deserialize<Project>(content);

            if (project is null || !project.IsValid())
            {
                return Result.Fail("Invalid project file.");
            }

            WeakReferenceMessenger.Default.Send(new ProjectCreateMessage(project));
            await recentProjectService.AddAsync(project);
            return Result.Ok(project);
        }
        catch (FileNotFoundException)
        {
            return Result.Fail("Could not open the project file.");
        }
    }
}