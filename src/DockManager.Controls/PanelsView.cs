using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using DockManager.Controls.Helpers.Builders;

namespace DockManager.Controls;

public class PanelsView : Regions, ILayoutView
{
    public virtual IEnumerable<ILayoutPane> Panes { get; }

    public static readonly DependencyProperty ActivePaneProperty = DependencyProperty.Register(
        nameof(ActivePane), 
        typeof(ILayoutPane), 
        typeof(PanelsView),
        new PropertyMetadata(default(ILayoutPane)));

    public ILayoutPane ActivePane
    {
        get => (ILayoutPane)GetValue(ActivePaneProperty);
        set => SetValue(ActivePaneProperty, value);
    }
    
    public void AddPane(ILayoutPane pane)
    {
        throw new System.NotImplementedException();
    }

    public void RemovePane(ILayoutPane pane)
    {
        throw new System.NotImplementedException();
    }

    public ILayoutPane GetActivePane(int index)
    {
        throw new System.NotImplementedException();
    }

    public PanelsView(Grid grid) : base("*", "*")
    {
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        
        MainGrid = Template.FindName("PART_MainGrid", this) as Grid;
    }
}