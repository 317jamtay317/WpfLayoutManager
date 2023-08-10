using System.Collections.Generic;
using System.Windows;
using DockManager.Controls.Helpers.Builders;

namespace DockManager.Controls.Core.Args;

public class LayoutManagerRegionArgs : RoutedEventArgs
{
    public LayoutManagerRegionArgs(RegionType regionType)
        : base(Regions.LayoutChangedEvent)
    {
        ChangedRegion = regionType;
    }

    /// <summary>
    /// Represents the type of region that is being changed
    /// </summary>
    public enum RegionType
    {
        Rows,
        Columns
    }

    /// <summary>
    /// Represents what changes have taken place
    /// </summary>
    public enum ChangeType
    {
        Add,
        Remove,
        NoChanges,
        ValueChanged
    }

    /// <summary>
    /// The value before the change
    /// </summary>
    public string? OldValue { get; set; }

    /// <summary>
    /// The value after the change
    /// </summary>
    public string? NewValue { get; set; }

    /// <summary>
    /// Total definitions before the change
    /// </summary>
    public int OldDefinitionsCount
    {
        get => GetDefinitionCount(OldValue);
    }

    /// <summary>
    /// the indexes that changed
    /// </summary>
    public IEnumerable<int> ChangedIndexes
    {
        get => _changedIndexes;
    }

    /// <summary>
    /// A readonly dictionary of all the definitions and what changes were made
    /// </summary>
    public IReadOnlyDictionary<int, ChangeType> Changes
    {
        get => _changes;
    }

    /// <summary>
    /// Total definitions after the change
    /// </summary>
    public int NewDefinitionsCount
    {
        get => GetDefinitionCount(NewValue);
    }

    /// <summary>
    /// What change has taken place
    /// </summary>
    public ChangeType Change
    {
        get
        {
            if (NewDefinitionsCount == OldDefinitionsCount)
            {
                if ((LayoutDefinition)OldValue == (LayoutDefinition)NewValue)
                {
                    return ChangeType.NoChanges;
                }
                else
                {
                    return ChangeType.ValueChanged;
                }
            }

            if (NewDefinitionsCount > OldDefinitionsCount)
            {
                return ChangeType.Add;
            }

            return ChangeType.Remove;
        }
    }

    /// <summary>
    /// Updates all collections with the correct
    /// changes that were made
    /// </summary>
    public LayoutManagerRegionArgs Init()
    {
        
        
        return this;
    }

    /// <summary>
    /// Which region has changed
    /// </summary>
    public RegionType ChangedRegion { get; }

    private int GetDefinitionCount(string? value)
    {
        return value?.Split(",")?.Length ?? 1;
    }

    private readonly Dictionary<int, ChangeType> _changes = new();
    private readonly List<int> _changedIndexes = new();
}