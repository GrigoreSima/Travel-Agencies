using System;
using Model;

namespace Networking.Requests;

[Serializable]
public class SaveReservationRequest : IRequest
{
    private Reservation _reservation;

    public SaveReservationRequest(Reservation reservation)
    {
        _reservation = reservation;
    }

    public Reservation Reservation => _reservation;
}