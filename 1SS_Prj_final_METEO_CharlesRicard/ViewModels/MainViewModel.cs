using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Prj_final_METEO.ViewModels.Delegates.ViewModelDelegates;
using Prj_final_METEO.ViewModels.Commands;
using Prj_final_METEO.Views;
using Prj_final_METEO.DataService.Repositories.Interfaces;

namespace Prj_final_METEO.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        public RelayCommand CmdOpenConfig { get; set; }

        private BaseViewModel viewModelActuel;
        public BaseViewModel ViewModelActuel
        {
            get => viewModelActuel;
            set
            {
                viewModelActuel = value;
                OnPropertyChanged();
            }

        }
        public MainViewModel()
        {
            CmdOpenConfig = new RelayCommand(OpenConfig, null);

            ViewModelActuel = new MeteoViewModel();
        }

        public void OpenConfig(object? obj)
        {
            ConfigView cfView = new ConfigView();

            cfView.Show();
        }
    }
}
