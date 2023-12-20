using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Prj_final_METEO.Models;
using static Prj_final_METEO.ViewModels.Delegates.ViewModelDelegates;
using Newtonsoft.Json;
using Prj_final_METEO.ViewModels.Commands;
using System.Text.Unicode;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.DirectoryServices;
using System.Windows.Controls.Ribbon;
using Prj_final_METEO.DataService.Repositories.Interfaces;
using Prj_final_METEO.DataService.Repositories.Database;
using Prj_final_METEO.DataService;

namespace Prj_final_METEO.ViewModels
{
    public class MeteoViewModel : BaseViewModel
    {

        public RelayCommand AddBTN { get; set; }
        public RelayCommand DelBTN { get; set; }
        public RelayCommand ClearResult {  get; set; }

        private ObservableCollection<Result> resultList;

        public ObservableCollection<Result> ResultList
        {
            get { return resultList; }
            set
            {
                resultList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Region> savedRegions;

        public ObservableCollection<Region> SavedRegions
        {
            get { return savedRegions; }
            set
            {
                savedRegions = value;
                OnPropertyChanged();
            }
        }

        public bool deleteAccess;
        public bool DeleteAccess
        {
            get { return deleteAccess; }
            set {
                deleteAccess = value;
                OnPropertyChanged();
            }
        }


        private Region? selectedRegion;
        public Region? SelectedRegion
        {
            get { return selectedRegion; }
            set
            {
                selectedRegion = value;
                OnPropertyChanged();
                if(value != null)
                {
                    GetMeteoCommand(value);
                    DeleteAccess = true;
                }
                else
                {
                    DeleteAccess = false;
                }
                    
            }
        }

        private double latitude = 0;
        public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                OnPropertyChanged();
            }
        }

        private double longitude = 0;
        public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                OnPropertyChanged();
            }
        }

        private string region = "";
        public string Region
        {
            get { return region; }
            set
            {
                region = value;
                OnPropertyChanged();
            }
        }

        private string regionName;

        public string RegionName
        {
            get { return regionName; }
            set
            {
                regionName = value;
                OnPropertyChanged();
            }
        }
        private string regionCountry;

        public string RegionCountry
        {
            get { return regionCountry; }
            set
            {
                regionCountry = value;
                OnPropertyChanged();
            }
        }


        private RegionDatabaseRepository _regionRepository;


        public MeteoViewModel()
        {
            ResultList = new ObservableCollection<Result>();

            AddBTN = new RelayCommand(AddLocation, null);
            DelBTN = new RelayCommand(DelLocation, null);
            ClearResult = new RelayCommand(ClearResultInfos, null);

            _regionRepository = new RegionDatabaseRepository(new SqliteRegionDbContext());

            SavedRegions = new ObservableCollection<Region>(_regionRepository.GetAll());
        }

        private void ClearResultInfos(object? obj)
        {
            ResultList.Clear();
            RegionCountry = "";
            RegionName = "";
            SelectedRegion = null;
        }

        private void AddLocation(object? obj)
        {
            if (Region.Trim() == "")
            {
                MessageBox.Show(Properties.Traduction.ErrorMsgRegionEmpty);
                return;
            }
            
            int newId = savedRegions.Last().Id + 1;

            Region newReg = new Region(newId, Region, Latitude, Longitude);
            _regionRepository.Add(newReg);
            savedRegions.Add(newReg);

            Region = "";
            Latitude = 0;
            Longitude = 0;
        }

        private void DelLocation(object? obj)
        {
            Region regToDel = SelectedRegion;
            SelectedRegion = null;

            _regionRepository.Del(regToDel);
            savedRegions.Remove(regToDel);
        }

        private void GetMeteoCommand(object? obj)
        {
            if (Properties.Settings.Default.JetonKey == "")
            {
                MessageBox.Show("Veuillez entrer un clée dans la configuration.");
                return;
            }
            ResultList.Clear();
            GetMeteo(SelectedRegion);
        }

        private async void GetMeteo(Region region)
        {
            ApiClient client = new ApiClient("https://api.weatherbit.io/v2.0/forecast/daily");

            #region Conversion de lat/loo
            // Conversion de lat/lon parce que mon pc est en français mais pas mon portable lol
            string lat = "" + region.Latitude;
            if (lat.IndexOf(',') != -1)
                lat = lat.Split(",").First() + "." + lat.Split(",").Last();

            string lon = "" + region.Longitude;
            if (lon.IndexOf(',') != -1)
                lon = lon.Split(",").First() + "." + lon.Split(",").Last();

            //Trace.WriteLine(lat + " : " + lon);
            #endregion

            string lang = Prj_final_METEO.Properties.Settings.Default.langue.Split("-").First();
            string request = "?lat=" + lat + "&lon=" + lon + "&lang=" + lang;

            String response = await client.RequeteGetAsync(request);

            try
            {
                JObject jsonObj = JObject.Parse(response);

                // ONLY FOR DEBUG
                //JObject jsonObj = JObject.Parse("{\"city_name\":\"Shawinigan\",\"country_code\":\"CA\",\"data\":[{\"app_max_temp\":8.2,\"app_min_temp\":1,\"clouds\":91,\"clouds_hi\":70,\"clouds_low\":99,\"clouds_mid\":83,\"datetime\":\"2023-12-18\",\"dewpt\":5.2,\"high_temp\":9.3,\"low_temp\":-1.2,\"max_dhi\":null,\"max_temp\":9.3,\"min_temp\":3.3,\"moon_phase\":0.431671,\"moon_phase_lunation\":0.2,\"moonrise_ts\":1702917905,\"moonset_ts\":1702958013,\"ozone\":296.5,\"pop\":95,\"precip\":47.68,\"pres\":974.2,\"rh\":94,\"slp\":990.7,\"snow\":0,\"snow_depth\":0,\"sunrise_ts\":1702902517,\"sunset_ts\":1702933488,\"temp\":6.1,\"ts\":1702875660,\"uv\":0.7,\"valid_date\":\"2023-12-18\",\"vis\":5.66,\"weather\":{\"icon\":\"r03d\",\"description\":\"Heavy rain\",\"code\":502},\"wind_cdir\":\"SSE\",\"wind_cdir_full\":\"south-southeast\",\"wind_dir\":148,\"wind_gust_spd\":6,\"wind_spd\":4.1},{\"app_max_temp\":1.2,\"app_min_temp\":-10.8,\"clouds\":73,\"clouds_hi\":0,\"clouds_low\":74,\"clouds_mid\":4,\"datetime\":\"2023-12-19\",\"dewpt\":-4.9,\"high_temp\":0.1,\"low_temp\":-9.1,\"max_dhi\":null,\"max_temp\":4,\"min_temp\":-7.5,\"moon_phase\":0.551619,\"moon_phase_lunation\":0.24,\"moonrise_ts\":1703005434,\"moonset_ts\":1702962693,\"ozone\":328.5,\"pop\":75,\"precip\":2.51,\"pres\":995.3,\"rh\":77,\"slp\":1012.6,\"snow\":19.5,\"snow_depth\":19.5,\"sunrise_ts\":1702988955,\"sunset_ts\":1703019913,\"temp\":-1.4,\"ts\":1702962060,\"uv\":0.6,\"valid_date\":\"2023-12-19\",\"vis\":16.663,\"weather\":{\"icon\":\"s04d\",\"description\":\"Mix snow/rain\",\"code\":610},\"wind_cdir\":\"WNW\",\"wind_cdir_full\":\"west-northwest\",\"wind_dir\":293,\"wind_gust_spd\":5.1,\"wind_spd\":3.4},{\"app_max_temp\":-4.8,\"app_min_temp\":-13,\"clouds\":27,\"clouds_hi\":0,\"clouds_low\":22,\"clouds_mid\":1,\"datetime\":\"2023-12-20\",\"dewpt\":-8.4,\"high_temp\":-0.6,\"low_temp\":-9.1,\"max_dhi\":null,\"max_temp\":-0.6,\"min_temp\":-9.1,\"moon_phase\":0.666972,\"moon_phase_lunation\":0.27,\"moonrise_ts\":1703092903,\"moonset_ts\":1703053715,\"ozone\":327.6,\"pop\":0,\"precip\":0,\"pres\":1015.1,\"rh\":74,\"slp\":1032.9,\"snow\":0,\"snow_depth\":19.5,\"sunrise_ts\":1703075390,\"sunset_ts\":1703106341,\"temp\":-4.5,\"ts\":1703048460,\"uv\":1.8,\"valid_date\":\"2023-12-20\",\"vis\":19.322,\"weather\":{\"icon\":\"c02d\",\"description\":\"Scattered clouds\",\"code\":802},\"wind_cdir\":\"WSW\",\"wind_cdir_full\":\"west-southwest\",\"wind_dir\":242,\"wind_gust_spd\":4.2,\"wind_spd\":2.8},{\"app_max_temp\":-9.8,\"app_min_temp\":-16.9,\"clouds\":11,\"clouds_hi\":3,\"clouds_low\":2,\"clouds_mid\":2,\"datetime\":\"2023-12-21\",\"dewpt\":-12.8,\"high_temp\":-4.5,\"low_temp\":-12.5,\"max_dhi\":null,\"max_temp\":-3.2,\"min_temp\":-9.8,\"moon_phase\":0.771391,\"moon_phase_lunation\":0.31,\"moonrise_ts\":1703180403,\"moonset_ts\":1703144748,\"ozone\":346.3,\"pop\":0,\"precip\":0,\"pres\":1020.4,\"rh\":62,\"slp\":1038.3,\"snow\":0,\"snow_depth\":19.5,\"sunrise_ts\":1703161823,\"sunset_ts\":1703192771,\"temp\":-6.6,\"ts\":1703134860,\"uv\":1.8,\"valid_date\":\"2023-12-21\",\"vis\":24.128,\"weather\":{\"icon\":\"c02d\",\"description\":\"Few clouds\",\"code\":801},\"wind_cdir\":\"NW\",\"wind_cdir_full\":\"northwest\",\"wind_dir\":311,\"wind_gust_spd\":11.5,\"wind_spd\":7.7},{\"app_max_temp\":-11.6,\"app_min_temp\":-18.8,\"clouds\":18,\"clouds_hi\":16,\"clouds_low\":0,\"clouds_mid\":8,\"datetime\":\"2023-12-22\",\"dewpt\":-15.3,\"high_temp\":-4.7,\"low_temp\":-11.4,\"max_dhi\":null,\"max_temp\":-4.7,\"min_temp\":-12.5,\"moon_phase\":0.859672,\"moon_phase_lunation\":0.34,\"moonrise_ts\":1703268031,\"moonset_ts\":1703235818,\"ozone\":376.3,\"pop\":0,\"precip\":0,\"pres\":1019.3,\"rh\":58,\"slp\":1037.3,\"snow\":0,\"snow_depth\":19.5,\"sunrise_ts\":1703248253,\"sunset_ts\":1703279204,\"temp\":-8.5,\"ts\":1703221260,\"uv\":1.7,\"valid_date\":\"2023-12-22\",\"vis\":24.128,\"weather\":{\"icon\":\"c02d\",\"description\":\"Few clouds\",\"code\":801},\"wind_cdir\":\"NNW\",\"wind_cdir_full\":\"north-northwest\",\"wind_dir\":356,\"wind_gust_spd\":6.9,\"wind_spd\":4.5},{\"app_max_temp\":-3.2,\"app_min_temp\":-15.3,\"clouds\":48,\"clouds_hi\":72,\"clouds_low\":64,\"clouds_mid\":67,\"datetime\":\"2023-12-23\",\"dewpt\":-12.8,\"high_temp\":-0.3,\"low_temp\":-5.1,\"max_dhi\":null,\"max_temp\":-0.3,\"min_temp\":-11.4,\"moon_phase\":0.927901,\"moon_phase_lunation\":0.37,\"moonrise_ts\":1703355898,\"moonset_ts\":1703326861,\"ozone\":365.1,\"pop\":0,\"precip\":0,\"pres\":1010.3,\"rh\":62,\"slp\":1028,\"snow\":0,\"snow_depth\":19.5,\"sunrise_ts\":1703334680,\"sunset_ts\":1703365640,\"temp\":-6.8,\"ts\":1703307660,\"uv\":1.5,\"valid_date\":\"2023-12-23\",\"vis\":17.435,\"weather\":{\"icon\":\"s04d\",\"description\":\"Mix snow/rain\",\"code\":610},\"wind_cdir\":\"WNW\",\"wind_cdir_full\":\"west-northwest\",\"wind_dir\":298,\"wind_gust_spd\":4.5,\"wind_spd\":3},{\"app_max_temp\":-4.9,\"app_min_temp\":-10.3,\"clouds\":5,\"clouds_hi\":1,\"clouds_low\":4,\"clouds_mid\":2,\"datetime\":\"2023-12-24\",\"dewpt\":-7.9,\"high_temp\":-2.3,\"low_temp\":-7,\"max_dhi\":null,\"max_temp\":-2.3,\"min_temp\":-6,\"moon_phase\":0.973593,\"moon_phase_lunation\":0.41,\"moonrise_ts\":1703444144,\"moonset_ts\":1703417692,\"ozone\":356.3,\"pop\":0,\"precip\":0,\"pres\":1014,\"rh\":75,\"slp\":1031.8,\"snow\":0,\"snow_depth\":19.5,\"sunrise_ts\":1703421105,\"sunset_ts\":1703452078,\"temp\":-4.1,\"ts\":1703394060,\"uv\":2.3,\"valid_date\":\"2023-12-24\",\"vis\":24.128,\"weather\":{\"icon\":\"c02d\",\"description\":\"Few clouds\",\"code\":801},\"wind_cdir\":\"NW\",\"wind_cdir_full\":\"northwest\",\"wind_dir\":312,\"wind_gust_spd\":6.2,\"wind_spd\":2.6}],\"lat\":46.5698,\"lon\":-72.7381,\"state_code\":\"10\",\"timezone\":\"America/Toronto\"}\r\n");

                IList<JToken> objList = jsonObj["data"].Children().ToList();

                RegionCountry = jsonObj["country_code"].ToString();
                RegionName = jsonObj["city_name"].ToString();

                IList<Result> detections = new List<Result>();
                foreach (JToken r in objList)
                {
                    Result newRes = new Result(DateOnly.Parse(r["datetime"].ToString()), Convert.ToDouble(r["low_temp"].ToString()), Convert.ToDouble(r["high_temp"].ToString()), "https://cdn.weatherbit.io/static/img/icons/" + r["weather"]["icon"].ToString() + ".png");
                    ResultList.Add(newRes);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n" + ex);
            }

            client.Dispose();
        }
    }
}
