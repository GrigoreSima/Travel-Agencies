using System;
using Model;

namespace Networking.Responses;

[Serializable]
public class FilteredTripsResponse : IResponse
{
    private Trip[] _trips;

    public FilteredTripsResponse(Trip[] trips)
    {
        this._trips = trips;
    }

    public Trip[] Trips => _trips;
}