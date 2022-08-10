using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Avalonia.Data.Converters;

namespace Larva.Avalonia.Converters;

public sealed class ClassNameToReadableNameConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return value;
        }

        var name = value.GetType().Name.Replace("Event", string.Empty);

        return Regex.Replace(name, @"(\B[A-Z]+?(?=[A-Z][^A-Z])|\B[A-Z]+?(?=[^A-Z]))",
            " $1", RegexOptions.Compiled);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return ((string?) value)?.Replace(" ", string.Empty) ?? value;
    }
}