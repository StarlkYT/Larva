using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Larva.Models.Events;

namespace Larva.Avalonia.ViewModels;

public sealed partial class EventsViewModel : ObservableObject
{
    [ObservableProperty]
    private EventBase? currentEvent;
    
    [ObservableProperty]
    private bool hasEvents;
    
    [ObservableProperty]
    private int selectedEventIndex;

    [ObservableProperty]
    private ObservableCollection<EventBase> events = new ObservableCollection<EventBase>();
    
    [RelayCommand]
    private void CreateEvent(int index)
    {
        var @event = index switch
        {
            0 => new MemberJoinEvent(),
            _ => null
        };

        if (@event is null || Events.Any(addedEvent => addedEvent.GetType().Name == @event.GetType().Name))
        {
            return;
        }
        Events.Add(@event);
        CurrentEvent = Events[^1];
        HasEvents = events.Count > 0;
    }

    public void UpdateEvents()
    {
        CurrentEvent = Events[selectedEventIndex];
    }
}