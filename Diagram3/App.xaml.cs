using Diagram3.Models;
using Diagram3.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Diagram3
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private MainWindow _mainWindow;
        private Model _model;
        public App()
        {
            _model = new Model();
            _mainWindow = new MainWindow();
            _mainWindow.DataContext = new MainViewModel(_model);
            _mainWindow.Show();
        }
    }
}
