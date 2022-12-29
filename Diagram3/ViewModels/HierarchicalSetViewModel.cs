using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using Diagram3.Views;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Diagram3.ViewModels
{
    internal class HierarchicalSetViewModel:ViewModelBase, ISelectable
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected;  }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    if (_isSelected)
                    {
                        Background = Brushes.LightGreen;
                    }
                    else
                    {
                        Background = Brushes.AliceBlue;
                    }
                    OnPropertyChanged(nameof(Background));
                }
            }
        }
        private int number = 0;
        public ImageSource Image { get; set; }
        public Brush Background { get; set; }
        private string _imageName;
        public string ImageName
        {
            get { return _imageName; }
            set
            {
                if (_imageName != value)
                {
                    _imageName = value;
                    Image = new BitmapImage(new Uri(_imageName, UriKind.Relative));
                    OnPropertyChanged(nameof(Image));
                }
            }
        }
        public string Text { get; set; }
        public ObservableCollection<HierarchicalView> HierarchicalViews { get; }
        public List<HierarchicalViewModel> HierarchicalViewModels { get; }
        public Command DropCommand { get; }
        public Command SelectCommand { get; }

        public event Action CanvasClicked;
        public event Action<object> KeyDown;
        public HierarchicalSetViewModel()
        {
            HierarchicalViews = new ObservableCollection<HierarchicalView>();
            HierarchicalViewModels = new List<HierarchicalViewModel>();
            DropCommand = new Command(OnDrop);
            SelectCommand = new Command(OnSelect);
            Background = Brushes.AliceBlue;
        }
        private void AddChild(int index)
        {
            HierarchicalView hierarchicalView = new HierarchicalView();
            HierarchicalViewModel hierarchicalViewModel = new HierarchicalViewModel();
            hierarchicalView.DataContext = hierarchicalViewModel;
            hierarchicalViewModel.Dropped += OnChildDrop;
            HierarchicalViews.Insert(index, hierarchicalView);
            HierarchicalViewModels.Insert(index, hierarchicalViewModel);
            hierarchicalViewModel.Text = "Hierarchical " + number.ToString();
            CanvasClicked += hierarchicalViewModel.OnCanvasClicked;
            KeyDown += hierarchicalViewModel.OnKeyDown;
            hierarchicalViewModel.DeleteChild += OnDeleteChild;
            number++;
        }

        public void OnDrop(object obj)
        {
            DragEventArgs e = obj as DragEventArgs;
            AddChild(0);
            e.Handled = true;
        }
        public void OnChildDrop(HierarchicalViewModel dropped, object obj)
        {
            int childIndex = HierarchicalViewModels.FindIndex(x => x == dropped);
            DragEventArgs e = obj as DragEventArgs;
            if (childIndex != -1)
            {
                DisplayTileViewModel displayTile = e.Data.GetData(typeof(DisplayTileViewModel)) as DisplayTileViewModel;
                if (displayTile != null)
                {
                    AddChild(childIndex + 1);
                }
                else
                {
                    Console.WriteLine("This is not a display tile!");
                }
            }
            e.Handled = true;
        }
        public void OnSelect(object obj)
        {
            MouseButtonEventArgs e = obj as MouseButtonEventArgs;
            e.Handled = true;
            IsSelected = true;
        }
        public void OnDeselect(object obj)
        {
            IsSelected = false;
        }
        public void OnCanvasClicked()
        {
            OnDeselect(null);
            CanvasClicked?.Invoke();
        }
        public void OnKeyDown(object obj)
        {
            KeyDown?.Invoke(obj);
        }
        public void OnDeleteChild(object obj)
        {
            HierarchicalViewModel hierarchicalViewModel = obj as HierarchicalViewModel;
            int index = HierarchicalViewModels.FindIndex(x => x == hierarchicalViewModel);
            if (index != -1)
            {
                hierarchicalViewModel.IsSelected = false;
                HierarchicalViewModels.RemoveAt(index);
                HierarchicalViews.RemoveAt(index);
            }
            else
            {
                Console.WriteLine("Cannot delete hierarchical!");
            }
        }

    }
}
