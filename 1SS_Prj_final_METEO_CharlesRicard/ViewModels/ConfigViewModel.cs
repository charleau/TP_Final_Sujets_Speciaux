using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prj_final_METEO.ViewModels.Commands;
using System.Windows;

namespace Prj_final_METEO.ViewModels
{
    public class ConfigViewModel : BaseViewModel
    {
        public RelayCommand cmdSaveConfig { get; set; }

        private int selectedLanguage;
        public int SelectedLanguage
        {
            get { return selectedLanguage; }
            set
            {
                selectedLanguage = value;
            }
        }

        private bool restartAppApply;
        public bool RestartAppApply
        {
            get { return restartAppApply; }
            set { restartAppApply = value; }
        }

        private string _ApiKey;
        public string ApiKey
        {
            get { return _ApiKey; }
            set
            {
                _ApiKey = value;
                OnPropertyChanged();
            }
        }

        public ConfigViewModel()
        {
            ApiKey = Prj_final_METEO.Properties.Settings.Default.JetonKey;

            cmdSaveConfig = new RelayCommand(SaveKey, null);
            checkLanguage();
        }

        private void SaveKey(object? obj)
        {
            Prj_final_METEO.Properties.Settings.Default.JetonKey = ApiKey;
            Prj_final_METEO.Properties.Settings.Default.Save();

            if (SelectedLanguage == 0)
            {
                ChangeToFrench();
            }
            else if (SelectedLanguage == 1)
            {
                ChangeToEnglish();
            }
        }

        private void checkLanguage()
        {
            string currentLangue = Prj_final_METEO.Properties.Settings.Default.langue;

            if (currentLangue == "fr-CA")
            {
                SelectedLanguage = 0;
            }
            else if (currentLangue == "en-US")
            {
                SelectedLanguage = 1;
            }

        }

        private void ChangeToFrench()
        {

            string currentLangue = Prj_final_METEO.Properties.Settings.Default.langue;

            Prj_final_METEO.Properties.Settings.Default.langue = "fr-CA";
            Prj_final_METEO.Properties.Settings.Default.Save();

            if (currentLangue == "en-US")
            {
                System.Diagnostics.Process.Start(fileName: Environment.ProcessPath);
                Application.Current.Shutdown();
            }
        }

        private void ChangeToEnglish()
        {
            string currentLangue = Prj_final_METEO.Properties.Settings.Default.langue;

            Prj_final_METEO.Properties.Settings.Default.langue = "en-US";
            Prj_final_METEO.Properties.Settings.Default.Save();

            if (currentLangue == "fr-CA")
            {
                System.Diagnostics.Process.Start(fileName: Environment.ProcessPath);
                Application.Current.Shutdown();
            }
        }
    }
}
