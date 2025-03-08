using System;
using Model;

namespace Persistence.Interfaces;

public interface IUserRepository : IRepository<long, User>
{
    /// Verifies if the user and password are correct and present
    /// <param name="username">username the username to login</param>
    /// <param name="password">password the password for that user</param>
    /// <returns>
    /// true if the password matches, false otherwise
    /// </returns>
    Boolean VerifyPasswordForUser(String username, String password);
}