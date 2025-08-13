using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individuellt_programmeringsprojekt
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public User() { }

        public User(string firstName, string lastName, string username, string password, bool isAdmin)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            IsAdmin = isAdmin;
        }
    }
}
