using FF.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.Contracts.Service
{
    public interface ILocationService
    {
        GooglePlace GetPlaceDetails(string placeId);
        GoogleNearby GetNearbyPlaces(double latitude, double longitude);
    }
}
