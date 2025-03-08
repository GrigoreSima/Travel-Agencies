using System;
using Model;

namespace Networking.Responses;

[Serializable]
public class AllTripsResponse : IResponse
{
    private Trip[] _trips;
    public AllTripsResponse(Trip[] trips)
    {
        this._trips = trips;
    }

    public Trip[] Trips => _trips;
}