using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DockManager.Controls
{
    public class LayoutManager : Control
    {
        public LayoutManager()
        {
        }

        #region Properties

        internal static readonly DependencyProperty RegionsProperty = DependencyProperty.Register(
            nameof(Regions), 
            typeof(Regions), 
            typeof(LayoutManager),
            new PropertyMetadata(default(Regions)));
        
        public static readonly DependencyProperty ContentPanelsProperty = DependencyProperty.Register(
            nameof(ContentPanels), 
            typeof(DisplayPanels), 
            typeof(LayoutManager), 
            new PropertyMetadata(default(DisplayPanels), OnDisplayPanelsChanged));
       
        public static readonly DependencyProperty AutoHideGroupsProperty = DependencyProperty.Register(
            nameof(AutoHideGroups), typeof(AutoHideGroups), 
            typeof(LayoutManager),
            new PropertyMetadata(default(AutoHideGroups)));

        public static readonly DependencyProperty ContentTypeProperty = DependencyProperty.Register(
            nameof(ContentType), 
            typeof(ContentType), 
            typeof(LayoutManager),
            new (Controls.ContentType.Groups));

        /// <summary>
        /// Gets or sets whether we display a tab control
        /// or panels in the content area
        /// </summary>
        public ContentType ContentType
        {
            get => (ContentType)GetValue(ContentTypeProperty);
            set => SetValue(ContentTypeProperty, value);
        }

        /// <summary>
        /// Regions Represent the sections of the layout manager
        /// </summary>
        internal Regions Regions
        {
            get => (Regions)GetValue(RegionsProperty);
            set => SetValue(RegionsProperty, value);
        }

        /// <summary>
        /// The items presented in the content area of the
        /// LayoutManager
        /// </summary>
        /// <remarks>
        /// This is used for both panels and tabs
        /// </remarks>
        public DisplayPanels ContentPanels
        {
            get => (DisplayPanels)GetValue(ContentPanelsProperty);
            set => SetValue(ContentPanelsProperty, value);
        }

        /// <summary>
        /// The panels on the left side of the app
        /// that only display the caption until you
        /// hover over them.
        /// </summary>
        /// <remarks>
        /// If you pin an autohide group it moves to the content area
        /// and adds a tab or a panel which ever is being used at the time
        /// </remarks>
        public AutoHideGroups AutoHideGroups
        {
            get => (AutoHideGroups)GetValue(AutoHideGroupsProperty);
            set => SetValue(AutoHideGroupsProperty, value);
        }

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _mainGrid = Template.FindName("PART_MainGrid", this) as Grid;
            if (_mainGrid is null)
            {
                throw new NoNullAllowedException("Grid should never be null"); 
            }
            
            Regions = new ("*", "*", _mainGrid);
            Loaded += UpdatePanels;
            Unloaded += CleanUp;
        }

        private void UpdatePanels(object sender, RoutedEventArgs e)
        {
            OnDisplayPanelsCollectionChanged(sender, null);
        }

        private static void OnDisplayPanelsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LayoutManager layoutManager)
            {
                layoutManager.ContentPanels.CollectionChanged -= layoutManager.OnDisplayPanelsCollectionChanged;
                layoutManager.ContentPanels.CollectionChanged += layoutManager.OnDisplayPanelsCollectionChanged;
            }
        }

        private void OnDisplayPanelsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            var columns = new string('*', ContentPanels.Count).ToCharArray().Select(x=>x.ToString());
            Regions.Columns = string.Join(",", columns);
            for (var i = 0; i < ContentPanels.Count; i++)
            {
                var group = ContentPanels[i] as LayoutPane;
                _mainGrid.Children.Add(group!);
                
                Grid.SetColumn(group, i);
            }
        }

        private static void OnContentTypeChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private void CleanUp(object sender, RoutedEventArgs e)
        {
            Unloaded -= CleanUp;
        }
        
        //fields
        private Grid? _mainGrid;
    }

    public enum ContentType
    {
        Groups,
        Tabs
    }

    public class DisplayPanels : ObservableCollection<ILayoutPane>
    {
        
    }

    public class AutoHideGroups : ObservableCollection<ILayoutPane>
    {
        
    }
}