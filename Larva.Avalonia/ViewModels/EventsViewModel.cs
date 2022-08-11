using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Larva.Models.Events;

namespace Larva.Avalonia.ViewModels;

public sealed partial class EventsViewModel : ObservableObject
{
    public bool IsDirty { get; private set; }
    
    [ObservableProperty]
    private EventBase? currentEvent;

    [ObservableProperty]
    private bool hasEvents;

    [ObservableProperty]
    private ObservableCollection<EventBase> events = new ObservableCollection<EventBase>();

    [RelayCommand]
    private void CreateEvent(int index)
    {
        EventBase? @event;
        switch (index)
        {
            case 0:
                @event = new MemberJoinEvent();
                break;
            case 1:
                @event = new ChannelCreateEvent();
                break;
            default:
                @event = null;
                break;
        }

        if (@event is null || Events.Any(addedEvent => addedEvent.GetType().Name == @event.GetType().Name))
        {
            return;
        }

        Events.Add(@event);
        IsDirty = true;
        CurrentEvent = Events[^1];
        HasEvents = events.Count > 0;
    }

    public void UpdateEvents(int index)
    {
        if (index >= 0)
        {
            CurrentEvent = Events[index];
        }
    }
}