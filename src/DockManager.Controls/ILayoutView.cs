using System.Collections.Generic;

namespace DockManager.Controls;

public interface ILayoutView
{
    /// <summary>
    /// Gets the panes in this view
    /// </summary>
    IEnumerable<ILayoutPane> Panes { get; }

    /// <summary>
    /// Gets the active pane
    /// </summary>
    ILayoutPane ActivePane { get; }
    
    /// <summary>
    /// Adds a new pane to the view
    /// </summary>
    /// <param name="pane"></param>
    void AddPane(ILayoutPane pane);

    /// <summary>
    /// Removes the pane from the view
    /// </summary>
    /// <param name="pane"></param>
    void RemovePane(ILayoutPane pane);

    
    ILayoutPane GetActivePane(int index);
}