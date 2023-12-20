using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prj_final_METEO.ViewModels.Commands;
using Prj_final_METEO.Views;
using Prj_final_METEO.DataService.Repositories.Interfaces;
using Prj_final_METEO.DataService.Repositories.Database;
using Prj_final_METEO.DataService;
using Prj_final_METEO.Models;
using System.Net.Http;

namespace Prj_final_METEO.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        public RelayCommand CmdOpenConfig { get; set; }
        private RegionDatabaseRepository RegionDbRepository { get; set; }
        private ApiClient _apiClient { get; set; }

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

            RegionDbRepository = new RegionDatabaseRepository(new SqliteRegionDbContext());
            _apiClient = new ApiClient("https://api.weatherbit.io/v2.0/forecast/daily");

            ViewModelActuel = new MeteoViewModel(RegionDbRepository, _apiClient);
        }

        public void OpenConfig(object? obj)
        {
            ConfigView cfView = new ConfigView();

            cfView.Show();
        }
    }
}
