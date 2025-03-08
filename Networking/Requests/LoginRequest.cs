using System;
using Model;

namespace Networking.Requests;

[Serializable]
public class LoginRequest : IRequest
{
    private User user;

    public LoginRequest(User user)
    {
        this.user = user;
    }

    public User User => user;
}