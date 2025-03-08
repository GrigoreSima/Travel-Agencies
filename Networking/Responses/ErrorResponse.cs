using System;

namespace Networking.Responses;

[Serializable]
public class ErrorResponse : IResponse
{
    private String _message;
    public ErrorResponse(string message)
    {
        this._message = message;
    }

    public string Message => _message;
}