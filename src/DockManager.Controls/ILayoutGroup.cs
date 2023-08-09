using System.Windows;

namespace DockManager.Controls;

public interface ILayoutGroup
{
    LayoutManagerLocation Location { get; }

    bool IsAutoHide { get; }
    
    bool HasChildren { get; }
    
    string Caption { get; set; }
    
    object Content { get; set; }
}

public enum LayoutManagerLocation
{
    Content,
    Left,
    Right,
    Bottom
}