using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Diagram3.ViewModels
{
    internal class HierarchicalViewModel:ViewModelBase, ISelectable
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
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
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged(nameof(Text));
                }
            }
        }
        private Brush _background;
        public Brush Background
        {
            get { return _background; }
            set { _background = value;
                OnPropertyChanged(nameof(Background)); }
        }
        public Command DropCommand { get; }
        public Command SelectCommand { get; }
        public Command KeyDownCommand { get; }
        public event Action CanvasClicked;
        public event Action<object> KeyDown;
        public HierarchicalViewModel()
        {
            DropCommand = new Command(OnDrop);
            SelectCommand = new Command(OnSelect);
            KeyDownCommand = new Command(OnKeyDown);
            Background = Brushes.AliceBlue;
        }
        public event Action<HierarchicalViewModel, object> Dropped;//second arg: dropeventargs
        public event Action<object> DeleteChild;
        public void OnDrop(object obj)
        {
            Dropped?.Invoke(this, obj);
        }
        public void OnDeselect(object obj)
        {
            IsSelected = false;
        }
        public void OnSelect(object obj)
        {
            MouseButtonEventArgs e = obj as MouseButtonEventArgs;
            e.Handled = true;
            IsSelected = true;
        }
        public void OnCanvasClicked()
        {
            CanvasClicked?.Invoke();
            OnDeselect(null);
        }
        public void OnKeyDown(object obj)
        {
            KeyDown?.Invoke(obj);
            KeyEventArgs e = obj as KeyEventArgs;
            if (e.Key==Key.Delete && IsSelected)
            {
                DeleteChild?.Invoke(this);
            }
        }
        ~HierarchicalViewModel()
        {
            Console.WriteLine("Hierarchical Destructed!");
        }
    }
}
