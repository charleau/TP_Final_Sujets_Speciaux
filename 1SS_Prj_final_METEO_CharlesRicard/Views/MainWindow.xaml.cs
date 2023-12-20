using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Prj_final_METEO.DataService;
using Prj_final_METEO.DataService.Repositories.Interfaces;
using Prj_final_METEO.ViewModels;

namespace Prj_final_METEO.Views
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
        public void OpenConfig()
        {
            ConfigView cfView = new ConfigView();

            cfView.Show();
        }
    }
}
