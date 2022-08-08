using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Larva.Avalonia.Models;

namespace Larva.Avalonia.Services;

public sealed class RecentProjectService
{
    private readonly string larvaPath = Path.Join(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        $"{nameof(Larva)}.json");

    public async Task<Project[]?> FetchAsync()
    {
        if (File.Exists(larvaPath))
        {
            var content = await File.ReadAllTextAsync(larvaPath);
            var recent = JsonSerializer.Deserialize<Project[]>(content);

            return recent?
                .Where(project => File.Exists(Path.Join(project.Path, $"{project.Name}.json")))
                .Take(3)
                .ToArray();
        }

        return null;
    }

    public async Task AddAsync(Project project)
    {
        var currentRecent = await FetchAsync();

        if (currentRecent is not null)
        {
            var projects = new List<Project>(currentRecent)
            {
                project
            };

            var json = JsonSerializer.Serialize(projects.ToArray(), new JsonSerializerOptions()
            {
                WriteIndented = true
            });

            await File.WriteAllTextAsync(larvaPath, json);
            return;
        }

        await File.WriteAllTextAsync(larvaPath, JsonSerializer.Serialize(new Project[]
        {
            project
        }));
    }
}