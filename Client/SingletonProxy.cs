using System;
using Networking.Client;
using Services;

namespace Client;

public class SingletonProxy
{
    public IService Server => _instance;

    private static IService _instance = null;

    private static String _ip;
    private static Int32 _port;

    public static void Init(String ip, Int32 port)
    {
        _ip = ip;
        _port = port;
    }

    public static IService GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ServiceProxy(_ip, _port);
        }

        return _instance;
    }
    
    
    
}