using Microsoft.VisualStudio.TestTools.UnitTesting;
using FF.Service;

namespace FF.UnitTests.OldTests
{
    /// <summary>
    /// These are examples of tests that have problems and could be better.
    /// </summary>
    [TestClass()]
    public class LocationServiceTests
    {
        [TestMethod()]
        public void LocationServiceTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void KeyTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetPlaceDetailsTest()
        {
            var http = new HttpService(new WebClient(), null);
            var file = new FileService();
            var sut = new LocationService(http, file);
            var place = sut.GetPlaceDetails("ChIJMya5ZceVlocR7j6awgpuImw");
            Assert.IsNotNull(place);
            Assert.AreEqual("OK", place.status);
            Assert.AreEqual("Super Saver", place.result.name);
            place = sut.GetPlaceDetails("invalid");
            Assert.AreNotEqual("OK", place.status);
        }

        [TestMethod()]
        public void GetNearbyPlacesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ValidCoordinatesTest()
        {
            Assert.Fail();
        }
    }
}


