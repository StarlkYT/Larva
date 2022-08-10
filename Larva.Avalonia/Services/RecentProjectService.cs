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

    public async Task<Recent[]?> FetchAsync()
    {
        if (File.Exists(larvaPath))
        {
            var content = await File.ReadAllTextAsync(larvaPath);
            var recent = XmlSerializationService.Deserialize<Recent[]>(content);

            return recent?
                .Where(currentRecent => File.Exists(currentRecent.Path) && currentRecent.IsValid())
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
            if (currentRecent.Any(recent => recent.Path == project.Path))
            {
                return;
            }

            var projects = new List<Recent>(currentRecent)
            {
                new Recent()
                {
                    Name = project.Name,
                    Path = project.Path
                }
            };

            projects.Reverse();

            var xml = XmlSerializationService.Serialize(projects.ToArray());

            await File.WriteAllTextAsync(larvaPath, xml);
            return;
        }

        await File.WriteAllTextAsync(larvaPath, XmlSerializationService.Serialize(new Recent[]
        {
            new Recent()
            {
                Name = project.Name,
                Path = project.Path
            }
        }));
    }

    public async Task<Recent[]?> RemoveAsync(Project project)
    {
        var currentRecent = await FetchAsync();

        if (currentRecent is null)
        {
            return null;
        }

        var recentProjects = new List<Recent>(currentRecent);

        var toRemove = recentProjects.SingleOrDefault(recent => recent.Path == project.Path);

        if (toRemove is not null)
        {
            recentProjects.Remove(toRemove);
        }

        var recent = recentProjects.ToArray();

        var xml = XmlSerializationService.Serialize(recent);
        await File.WriteAllTextAsync(larvaPath, xml);

        return recent.Length == 0 ? null : currentRecent;
    }
}