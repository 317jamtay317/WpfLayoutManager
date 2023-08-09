using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Xml.XPath;
using DockManager.Controls.Core.Args;

namespace DockManager.Controls
{
    public class LayoutManager : Control
    {
        public LayoutManager()
        {
        }

        static LayoutManager()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LayoutManager),
                new FrameworkPropertyMetadata(typeof(LayoutManager)));
        }

        #region Properties

        internal static readonly DependencyProperty RegionsProperty = DependencyProperty.Register(
            nameof(Regions), typeof(LayoutManagerRegions), typeof(LayoutManager), new PropertyMetadata(default(LayoutManagerRegions)));

        internal LayoutManagerRegions Regions
        {
            get => (LayoutManagerRegions)GetValue(RegionsProperty);
            set => SetValue(RegionsProperty, value);
        }

        public static readonly DependencyProperty DisplayPanelsProperty = DependencyProperty.Register(
            nameof(DisplayPanels), 
            typeof(DisplayPanels), 
            typeof(LayoutManager), 
            new PropertyMetadata(default(DisplayPanels), OnDisplayPanelsChanged));

        public DisplayPanels DisplayPanels
        {
            get => (DisplayPanels)GetValue(DisplayPanelsProperty);
            set => SetValue(DisplayPanelsProperty, value);
        }

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var grid = Template.FindName("PART_MainGrid", this) as Grid;
            if (grid is null)
            {
                throw new NoNullAllowedException("Grid should never be null"); 
            }
            
            Regions = new ("*", "*", grid);
            Regions.LayoutChanged += OnLayoutChanged;
            Unloaded += CleanUp;
        }

        private static void OnDisplayPanelsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LayoutManager layoutManager)
            {
                layoutManager.DisplayPanels.CollectionChanged -= layoutManager.OnDisplayPanelsCollectionChanged;
                layoutManager.DisplayPanels.CollectionChanged += layoutManager.OnDisplayPanelsCollectionChanged;
            }
        }

        private void OnLayoutChanged(object sender, LayoutManagerRegionArgs args)
        {
            throw new System.NotImplementedException();
        }

        private void OnDisplayPanelsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            
        }

        private void CleanUp(object sender, RoutedEventArgs e)
        {
            Regions.LayoutChanged -= OnLayoutChanged;
            Unloaded -= CleanUp;
        }
    }

    public class DisplayPanels : ObservableCollection<ILayoutGroup>
    {
        
    }

    public class AutoHideGroups : ObservableCollection<ILayoutGroup>
    {
        
    }
}