using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace DockManager.Controls.Helpers.Builders;

/// <summary>
/// the definition of what size the dock manager will
/// create the panels at
/// </summary>
public sealed class LayoutDefinition : IReadOnlyCollection<GridLength> , IEquatable<LayoutDefinition>
{
    public static implicit operator LayoutDefinition(string layoutString) => Create(layoutString);
    public static bool operator ==(LayoutDefinition left, LayoutDefinition right)
    {
        if (left is null)
        {
            return right is null;
        }

        if (right is null)
        {
            return false;
        }
        
        return left.Equals(right);
    }
    public static bool operator !=(LayoutDefinition left, LayoutDefinition right)
    {
        return !(left == right);
    }
    
    private List<GridLength> _lengths = new();

    private LayoutDefinition(){}
    
    private void Add(string length)
    {
        if (double.TryParse(length, out var doubleLength))
        {
            Add(new GridLength(doubleLength));
            return;
        }

        if (length.Trim().Equals("*"))
        {
            Add(new GridLength(1, GridUnitType.Star));
            return;
        }

        if (length.Contains("*") && 
            TryParseStar(length, out double count))
        {
            Add(new GridLength(count, GridUnitType.Star));
            return;
        }
        
        Add(new GridLength());
    }

    private bool TryParseStar(string length, out double d)
    {
        d = 0;
        if (!length.Contains("*"))
        {
            return false;
        }

        var totalStars = length.Replace("*", "");

        if (double.TryParse(totalStars, out d))
        {
            return true;
        }

        return false;
    }

    private void Add(GridLength length)
    {
        _lengths.Add(length);
    }

    /// <summary>
    /// Creates a LayoutDefinition from the provided string
    /// </summary>
    /// <param name="layoutString">the string to create the definition</param>
    /// <remarks>
    /// provide a comma seperated string in the order that you would like the lengths to be added
    /// Example:
    /// provide: *,*,* returns: a collection with 3 grid lengths of 1 star
    /// provide: an empty string returns a collection with 1 grid length of auto
    /// provide: auto,auto,2* returns 2 grid lengths of auto, and 1 with a 2* length
    /// </remarks>
    internal static LayoutDefinition Create(string layoutString)
    {
        if (layoutString is null)
        {
            layoutString = string.Empty;
        }
        var spiltString = layoutString.Split(",");
        var definition = new LayoutDefinition();

        foreach (var length in spiltString)
        {
            definition.Add(length);
        }

        return definition;
    }

    private static readonly Regex _starRegex = new Regex("\\*", RegexOptions.Compiled);
    private static readonly Regex _autoRegex = new Regex("auto", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    
    internal static bool IsValidString(string? layoutString)
    {
        if (layoutString is null)
        {
            return false;
        }
        var spiltDefinitions = layoutString.Split(",");
        foreach (var definition in spiltDefinitions)
        {
            if (definition == string.Empty)
            {
                continue;
            }

            var starMatch = _starRegex.IsMatch(definition);
            var autoMatch = _autoRegex.IsMatch(definition);
            var isNumeric = double.TryParse(definition, out _);
            if (!(starMatch || autoMatch || isNumeric))
            {
                return false;
            }
        }
        return true;
    }
    
    public IEnumerator<GridLength> GetEnumerator()
    {
        return _lengths.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public GridLength this[int index]
    {
        get => _lengths[index];
    }

    public int Count => _lengths.Count;

    public bool Equals(LayoutDefinition? other)
    {
        if (ReferenceEquals(this, other))
        {
            return true;
        }
        if (other.Count != Count)
        {
            return false;
        }

        for (var i = 0; i < Count; i++)
        {
            var gridLength = _lengths[i];
            var comparer = other[i];
            if (gridLength != comparer)
            {
                return false;
            }
        }
        return true;
    }

    public override bool Equals(object? obj)
    {
        if (obj is LayoutDefinition layoutDefinition)
        {
            return Equals(layoutDefinition);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_lengths, Count);
    }

    public override string ToString()
    {
        var builder = new StringBuilder();

        for(var i=0; i < Count; i++)
        {
            var length = _lengths[i];
            var layoutString = AddString(length);
            if (i == Count - 1)
            {
                builder.Append(layoutString);
            }
            else
            {
                builder.Append($"{layoutString},");
            }
        }
        
        return builder.ToString();
    }

    private string AddString(GridLength length)
    {
        if (length.IsAuto)
        {
            return "auto";
        }

        if (length.IsAbsolute)
        {
            return length.Value.ToString();
        }

        return length.Value == 1 ? "*" : $"{length.Value}*";
    }
}