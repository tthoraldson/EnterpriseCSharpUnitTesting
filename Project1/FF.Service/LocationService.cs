using FF.Contracts;
using FF.Contracts.Dto;
using FF.Contracts.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FF.Service
{
    public class LocationService : ILocationService
    {
        private IHttpService _httpService;
        private IFileService _fileService;
        private string _urlNearBy = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={0},{1}&radius=500&types=grocery_or_supermarket&key={2}";
        private string _urlDetails = "https://maps.googleapis.com/maps/api/place/details/json?placeid={0}&key={1}";

        public LocationService(IHttpService httpService, IFileService fileService)
        {
            _httpService = httpService;
            _fileService = fileService;
        }

        private string Key()
        {
            return _fileService.ReadAllLines("C:/Key/google.txt").First<string>();
        }

        public GooglePlace GetPlaceDetails(string placeId)
        {
            var url = string.Format(_urlDetails, placeId, Key());
            string result = _httpService.GetResponse(url);
            return (GooglePlace)JsonConvert.DeserializeObject<GooglePlace>(result);
        }

        public GoogleNearby GetNearbyPlaces(double latitude, double longitude)
        {
            var url = string.Format(_urlNearBy, latitude, longitude, Key());
            string result = _httpService.GetResponse(url);
            return (GoogleNearby)JsonConvert.DeserializeObject<GoogleNearby>(result);
        }


    }
}
