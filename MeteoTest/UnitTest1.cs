using Moq;
using Prj_final_METEO.DataService;
using Prj_final_METEO.DataService.Repositories.Database;
using Prj_final_METEO.DataService.Repositories.Interfaces;
using Prj_final_METEO.Models;
using Prj_final_METEO.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MeteoTest
{
    public class UnitTest1
    {

        private readonly Mock<IRegionRepository> _regionDbMock = new Mock<IRegionRepository>();

        #region Test du constructeur - Doit être valide
        [Fact]
        public void ConstructorTest_ShouldBeEqual()
        {
            //Setup
            _regionDbMock.Setup(rdr => rdr.GetAll()).Returns(ListeAttendues());

            //Execution
            MeteoViewModel mvm = new MeteoViewModel(_regionDbMock.Object);

            Console.WriteLine(_regionDbMock.Object.GetAll());
            Console.WriteLine(mvm.SavedRegions.First().Name);

            //Assertion
            Assert.Equal(mvm.SavedRegions, new ObservableCollection<Region>(ListeAttendues()));
        }

        private List<Region> ListeAttendues()
        {
            List<Region> list =  new List<Region>()
            {
                new Region(1, "Shawinigan", 12.123456, -78.456132214),
                new Region(1, "Québec", 28.123456789, 32.456789123),
                new Region(1, "Montréal", -45.789132456, -7.89465132023)
            }.OrderBy(r => r.Id).ToList();

            return list;
        }
        #endregion
    }
}