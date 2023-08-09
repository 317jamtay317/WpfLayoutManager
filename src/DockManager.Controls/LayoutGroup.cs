using System.Windows;
using System.Windows.Controls;

namespace DockManager.Controls;

public class LayoutGroup : ContentControl, ILayoutGroup
{
    public LayoutManagerLocation Location { get; internal set; } = LayoutManagerLocation.Content;
    
    public virtual bool IsAutoHide => false;

    public static readonly DependencyProperty HasChildrenProperty = DependencyProperty.Register(
        nameof(HasChildren),
        typeof(bool),
        typeof(LayoutGroup),
        new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.NotDataBindable));

    public bool HasChildren
    {
        get => (bool)GetValue(HasChildrenProperty);
        protected set => SetValue(HasChildrenProperty, value);
    }

    public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register(
        nameof(Caption),
        typeof(string),
        typeof(LayoutGroup),
        new PropertyMetadata(default(string)));

    public string Caption
    {
        get => (string)GetValue(CaptionProperty);
        set => SetValue(CaptionProperty, value);
    }
}