using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DockManager.Controls.Core.Exceptions;
using DockManager.Controls.Helpers.Builders;
using Microsoft.Xaml.Behaviors;

namespace DockManager.Controls.Core.Behaviors;

public class GridBehavior : Behavior<Grid>
{
    public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register(
        nameof(Columns), 
        typeof(string), 
        typeof(GridBehavior), 
        new (string.Empty, OnColumnsPropertyChanged));

    public string Columns
    {
        get => (string)GetValue(ColumnsProperty);
        set => SetValue(ColumnsProperty, value);
    }

    public static readonly DependencyProperty RowsProperty = DependencyProperty.Register(
        nameof(Rows),
        typeof(string),
        typeof(GridBehavior), 
        new (string.Empty, OnRowsPropertyChanged));

    public string Rows
    {
        get => (string)GetValue(RowsProperty);
        set => SetValue(RowsProperty, value);
    }

    private static void OnColumnsPropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (sender is GridBehavior behavior)
        {
            behavior.AddColumns();
        }
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        
        AddRows();
        AddColumns();
    }

    private static void OnRowsPropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (sender is GridBehavior behavior)
        {
            behavior.AddRows();
        }
    }

    private void AddRows()
    {
        if (!LayoutDefinition.IsValidString(Rows))
        {
            throw new InvalidLayoutStringException(Rows);
        }

        AssociatedObject.RowDefinitions.Clear();
        var rows = GetDefinitions<RowDefinition>(Rows,
            length=> new (){Height = length});

        foreach (var row in rows)
        {
            AssociatedObject.RowDefinitions.Add(row);
        }
    }

    private void AddColumns()
    {
        if (!LayoutDefinition.IsValidString(Columns))
        {
            throw new InvalidLayoutStringException(Columns);
        }
        AssociatedObject.ColumnDefinitions.Clear();
        
        var columns = GetDefinitions<ColumnDefinition>(Columns, length =>
            new (){Width = length}
        );

        foreach (var column in columns)
        {
            AssociatedObject.ColumnDefinitions.Add(column);
        }
    }

    private IEnumerable<T> GetDefinitions<T>(string layoutString, Func<GridLength, T> createDefinition)
        where T : DefinitionBase, new()
    {
        var definitions = LayoutDefinition.Create(layoutString);
        return definitions.Select(createDefinition).ToList();
    }
}