using System;
using System.Collections.Generic;
using System.Data;
using Model;
using Persistence.Interfaces;
using Persistence.Utils;

namespace Persistence;

public class UserRepository : IUserRepository
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    IDictionary<String, string> props;

    public UserRepository(IDictionary<string, string> props)
    {
        Log.Info("Creating UserRepository");
        this.props = props;
    }

    public User FindOne(long id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> FindAll()
    {
        throw new NotImplementedException();
    }

    public Boolean Save(User entity)
    {
        throw new NotImplementedException();
    }

    public Boolean Delete(long id)
    {
        throw new NotImplementedException();
    }

    public Boolean Update(User entity)
    {
        throw new NotImplementedException();
    }

    public bool VerifyPasswordForUser(string username, string password)
    {
        Log.InfoFormat("Entering VerifyPasswordForUser with user: {0}, password={1}", username, password);
        IDbConnection? connection = DbUtils.GetConnection(props);

        using (var command = connection?.CreateCommand())
        {
            if (command != null)
            {
                command.CommandText = "SELECT username, password FROM Users " +
                                      "WHERE username=@username AND password=@password";
                IDbDataParameter usernameParam = command.CreateParameter();
                usernameParam.ParameterName = "@username";
                usernameParam.Value = username;
                command.Parameters.Add(usernameParam);
                
                IDbDataParameter passwordParam = command.CreateParameter();
                passwordParam.ParameterName = "@password";
                passwordParam.Value = password;
                command.Parameters.Add(passwordParam);

                using (var dataR = command.ExecuteReader())
                {
                    var result = dataR.Read();
                    Log.InfoFormat("Exiting VerifyPasswordForUser with result: {0}", result);
                    return result;
                }
            }
        }
        Log.InfoFormat("Exiting VerifyPasswordForUser with result: false");
        return false;
    }
}