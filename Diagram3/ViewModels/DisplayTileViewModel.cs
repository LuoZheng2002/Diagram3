using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Diagram3.ViewModels
{
    internal class DisplayTileViewModel : ViewModelBase
    {
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
        private string _imageName;
        public string ImageName
        {
            get { return _imageName; }
            set
            {
                _imageName = value;
                Image = new BitmapImage(new Uri(_imageName, UriKind.Relative));
                OnPropertyChanged(nameof(Image));
            }
        }
        public ImageSource Image { get; set; }
        public WrappedEvent DragEvent { get; }
        public ICommand DragCommand { get; }
        public DisplayTileViewModel()
        {
            DragEvent = new WrappedEvent();
            DragEvent.Add(OnDrag);
            DragCommand = new Command(DragEvent);
        }
        void OnDrag(object obj)
        {
            MouseButtonEventArgs e = obj as MouseButtonEventArgs;
            DragDrop.DoDragDrop(new UIElement(), this, DragDropEffects.Move);
        }
    }
}
