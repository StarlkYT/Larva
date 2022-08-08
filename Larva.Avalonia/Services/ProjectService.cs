using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using Larva.Avalonia.Message;
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

        var json = JsonSerializer.Serialize(project, new JsonSerializerOptions()
        {
            WriteIndented = true
        });

        await File.WriteAllTextAsync(Path.Join(project.Path, $"{project.Name}.json"), json);
        await recentProjectService.AddAsync(project);
    }
}