using System.Windows;
using System.Windows.Controls;
using DockManager.Controls.Core.Args;
using DockManager.Controls.Helpers.Builders;

namespace DockManager.Controls;

public class Regions : Control
{
    protected Grid MainGrid { get; set; }

    public Regions(
        LayoutDefinition rows,
        LayoutDefinition columns)
    {
        Rows = rows.ToString();
        Columns = columns.ToString();
    }
    
    public Regions(
        LayoutDefinition rows,
        LayoutDefinition columns,
        Grid grid)
    {
        Rows = rows.ToString();
        Columns = columns.ToString();
        MainGrid = grid;
    }

    #region Events

    // Register a custom routed event using the Bubble routing strategy.
    public static readonly RoutedEvent LayoutChangedEvent = EventManager.RegisterRoutedEvent(
        name: nameof(LayoutChangedEvent),
        routingStrategy: RoutingStrategy.Bubble,
        handlerType: typeof(LayoutManagerRegionChanged),
        ownerType: typeof(Regions));

    // Provide CLR accessors for assigning an event handler.
    public event LayoutManagerRegionChanged LayoutChanged
    {
        add => AddHandler(LayoutChangedEvent, value);
        remove => RemoveHandler(LayoutChangedEvent, value);
    }

    #endregion
    
    #region Properties

    public static readonly DependencyProperty RowsProperty = DependencyProperty.Register(
        nameof(Rows),
        typeof(string), 
        typeof(LayoutManager),
        new FrameworkPropertyMetadata(
            string.Empty, 
            FrameworkPropertyMetadataOptions.NotDataBindable,
            RaiseRowsChanged ));

    public string Rows
    {
        get => (string)GetValue(RowsProperty);
        set => SetValue(RowsProperty, value);
    }

    public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register(
        nameof(Columns),
        typeof(string), 
        typeof(LayoutManager), 
        new FrameworkPropertyMetadata(
            string.Empty, 
            FrameworkPropertyMetadataOptions.NotDataBindable,
            RaiseColumnsChanged));

    public string Columns
    {
        get => (string)GetValue(ColumnsProperty);
        set => SetValue(ColumnsProperty, value);
    }


    #endregion

    private static void RaiseRowsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
        RaiseLayoutChanged(d, e, LayoutManagerRegionArgs.RegionType.Rows);

    private static void RaiseLayoutChanged(DependencyObject d, DependencyPropertyChangedEventArgs e,
        LayoutManagerRegionArgs.RegionType type)
    {
        if (d is not Regions layoutManagerRegions)
        {
            return;
        }

        var args = new LayoutManagerRegionArgs(type)
        {
            NewValue = e.NewValue?.ToString(),
            OldValue = e.OldValue?.ToString()
        }.Init();
            
        layoutManagerRegions.RaiseEvent(args);
        layoutManagerRegions.AddLayoutGroupToRegion(args);
    }

    private void AddLayoutGroupToRegion(LayoutManagerRegionArgs layoutManagerRegionArgs)
    {
        
    }

    private static void RaiseColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
        RaiseLayoutChanged(d,  e, LayoutManagerRegionArgs.RegionType.Columns);

    
}

public delegate void LayoutManagerRegionChanged(object sender, LayoutManagerRegionArgs args);