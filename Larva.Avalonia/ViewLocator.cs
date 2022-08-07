using System;
using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace Larva.Avalonia;

public sealed class ViewLocator : IDataTemplate
{
    public IControl Build(object data)
    {
        var name = data.GetType().FullName!.Replace("ViewModel", "View");
        var type = Type.GetType(name);

        if (type is not null)
        {
            return (Control) Activator.CreateInstance(type)!;
        }

        return new TextBlock()
        {
            Text = $"Could not find '{name}'."
        };
    }

    public bool Match(object data)
    {
        return data is INotifyPropertyChanged;
    }
}