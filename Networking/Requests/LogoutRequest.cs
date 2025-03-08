using System;
using Model;

namespace Networking.Requests;

[Serializable]
public class LogoutRequest : IRequest
{
    private User user;

    public LogoutRequest(User user)
    {
        this.user = user;
    }

    public User User => user;
}