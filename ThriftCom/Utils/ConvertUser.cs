using Model;
using ThriftCom;

namespace Thrift.Utils;

public class ConvertUser
{
    public static User toUser(ThriftUser user)
    {
        return new User(
            user.Id,
            user.Username,
            user.Password
        );
    }
    
    public static ThriftUser toThriftUser(User user)
    {
        ThriftUser thriftUser = new ThriftUser();
        thriftUser.Id = user.Id;
        thriftUser.Username = user.Username;
        thriftUser.Password = user.Password;

        return thriftUser;
    }
}