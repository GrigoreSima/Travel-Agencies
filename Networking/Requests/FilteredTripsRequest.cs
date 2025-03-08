using System;
using Model;

namespace Networking.Requests;

[Serializable]
public class FilteredTripsRequest : IRequest
{
    private String _landmark;
    private Int32 _lowerLimit;
    private Int32 _upperLimit;
    private User _user;

    public FilteredTripsRequest(string landmark, int lowerLimit, int upperLimit, User user)
    {
        this._landmark = landmark;
        this._lowerLimit = lowerLimit;
        this._upperLimit = upperLimit;
        this._user = user;
    }

    public string Landmark => _landmark;

    public int LowerLimit => _lowerLimit;

    public int UpperLimit => _upperLimit;

    public User User => _user;
}