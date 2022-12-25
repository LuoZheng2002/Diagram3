using Diagram3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Diagram3.ViewModels
{
    internal class MainViewModel:ViewModelBase
    {
        private Model _model;
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    OnPropertyChanged(nameof(CurrentViewModel));
                }
            }
        }
        public WrappedEvent KeyDownEvent { get; }
        public ICommand KeyDownCommand { get; }
        public event Action<object> KeyDown;
        public void NavigateToDiagram()
        {
            DiagramViewModel diagramViewModel = new DiagramViewModel(_model);
            KeyDown += diagramViewModel.OnKeyDown;
            CurrentViewModel = diagramViewModel;
        }
        public MainViewModel(Model model)
        {
            _model = model;
            NavigateToDiagram();
            KeyDownEvent = new WrappedEvent();
            KeyDownEvent.Add(OnKeyDown);
            KeyDownCommand = new Command(KeyDownEvent);
        }
        public void OnKeyDown(object obj)
        {
            KeyDown?.Invoke(obj);
        }
    }
}

