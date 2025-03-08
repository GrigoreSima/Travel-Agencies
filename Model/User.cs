using System;

namespace Model;

[Serializable]
public class User : Entity<long>
{
    public string Username { get; }
    public string Password { get; }

    public User(long id, string username, string password) : base(id)
    {
        Username = username;
        Password = password;
    }

    protected bool Equals(User other)
    {
        return base.Equals(other) && Username == other.Username && Password == other.Password;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((User)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ (Username != null ? Username.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Password != null ? Password.GetHashCode() : 0);
            return hashCode;
        }
    }


    public override string ToString()
    {
        return $"{base.ToString()}, " +
               $"{nameof(Username)}: {Username}, " +
               $"{nameof(Password)}: {Password}";
    }
    
}