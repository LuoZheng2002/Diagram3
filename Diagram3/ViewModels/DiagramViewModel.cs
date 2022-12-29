using Diagram3.Adorners;
using Diagram3.Models;
using Diagram3.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Diagram3.ViewModels
{
    internal class DiagramViewModel:ViewModelBase
    {
        private Model _model;
        public ObservableCollection<UserControl> DiagramItems { get; }
        public List<ViewModelBase> DiagramItemViewModels { get; }
        public DisplayTileViewModel DisplayTile0 { get; }
        public DisplayTileViewModel DisplayTile1 { get; }
        public DisplayTileViewModel DisplayTile2 { get; }
        public DisplayTileViewModel DisplayTile3 { get; }
        public Command DropCommand { get; }
        public Command ClickCommand { get; }
        public event Action CanvasClicked;
        public event Action<object> KeyDown;
        public DiagramViewModel(Model model)
        {
            _model = model;
            DisplayTile0 = new DisplayTileViewModel() { Text = "HS", ImageName = "../Images/114514.jpeg" };
            DisplayTile1 = new DisplayTileViewModel() { Text = "PS", ImageName = "../Images/fufu.jpg" };
            DisplayTile2 = new DisplayTileViewModel() { Text = "H", ImageName = "../Images/53.jpg" };
            DisplayTile3 = new DisplayTileViewModel() { Text = "P", ImageName = "../Images/gua.jpg" };
            DropCommand = new Command(OnDrop);
            ClickCommand = new Command(OnClick);
            DiagramItems = new ObservableCollection<UserControl>();
            DiagramItemViewModels = new List<ViewModelBase>();
        }
        public void OnDrop(object obj)
        {
            DragEventArgs e = obj as DragEventArgs;
            DisplayTileViewModel displayTile = e.Data.GetData(typeof(DisplayTileViewModel)) as DisplayTileViewModel;
            if (displayTile != null)
            {
                Canvas canvas = e.Source as Canvas;
                if (canvas != null)
                {
                    Point pos = e.GetPosition(canvas);
                    switch(displayTile.Text)
                    {
                        default:
                            {
                                HierarchicalSetView hierarchicalSetView = new HierarchicalSetView();
                                HierarchicalSetViewModel hierarchicalSetViewModel = new HierarchicalSetViewModel();
                                hierarchicalSetViewModel.Text = "Hierarchical Set Name Here!";
                                hierarchicalSetViewModel.ImageName = "../Images/114514.jpeg";
                                CanvasClicked += hierarchicalSetViewModel.OnCanvasClicked;
                                KeyDown += hierarchicalSetViewModel.OnKeyDown;
                                hierarchicalSetView.DataContext= hierarchicalSetViewModel;
                                DiagramItems.Add(hierarchicalSetView);
                                DiagramItemViewModels.Add(hierarchicalSetViewModel);
                                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(hierarchicalSetView);
                                if (adornerLayer==null)
                                {
                                    Console.WriteLine("Failed to get adorner layer!");
                                }
                                else
                                {
                                    MoveAdorner moveAdorner = new MoveAdorner(hierarchicalSetView);
                                    adornerLayer.Add(moveAdorner);
                                }
                                Canvas.SetLeft(hierarchicalSetView, pos.X);
                                Canvas.SetTop(hierarchicalSetView, pos.Y);
                                break;
                            }
                    }
                }
                else
                {
                    Console.WriteLine("The item doesn't drop on canvas!");
                }
            }
        }
        public void OnClick(object obj)
        {
            Console.WriteLine("Clicked on canvas!");
            CanvasClicked?.Invoke();
        }
        public void OnKeyDown(object obj)
        {
            KeyDown?.Invoke(obj);
        }
    }
}
