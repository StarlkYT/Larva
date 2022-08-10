using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Larva.Avalonia.Models;

namespace Larva.Avalonia.Services;

public sealed class RecentProjectService
{
    private readonly string larvaPath = Path.Join(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        $"{nameof(Larva)}.xml");

    public async Task<Project[]?> FetchAsync()
    {
        if (File.Exists(larvaPath))
        {
            var content = await File.ReadAllTextAsync(larvaPath);
            var recent = XmlSerializationService.Deserialize<Project[]>(content);
            
            return recent?
                .Where(project => File.Exists(project.Path))
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
            if (currentRecent.Any(recent => recent.Name == project.Name))
            {
                return;
            }

            var projects = new List<Project>(currentRecent)
            {
                project
            };

            projects.Reverse();

            var xml = XmlSerializationService.Serialize(projects.ToArray());

            await File.WriteAllTextAsync(larvaPath, xml);
            return;
        }

        await File.WriteAllTextAsync(larvaPath, XmlSerializationService.Serialize(new Project[]
        {
            project
        }));
    }

    public async Task<Project[]?> RemoveAsync(Project project)
    {
        var currentRecent = await FetchAsync();

        if (currentRecent is null)
        {
            return null;
        }

        var projects = new List<Project>(currentRecent);

        var toRemove = projects.SingleOrDefault(recent => recent.Name == project.Name && recent.Path == project.Path);

        if (toRemove is not null)
        {
            projects.Remove(toRemove);
        }

        var recent = projects.ToArray();

        var xml = XmlSerializationService.Serialize(recent);
        await File.WriteAllTextAsync(larvaPath, xml);

        return recent.Length == 0 ? null : currentRecent;
    }
}