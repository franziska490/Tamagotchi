using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTamagotchi.Models
{
    public class User
    {
        public int Id { get; set; } 
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "admin" oder "user"

        public User(int id,string username, string password, string role)
        {
            Id = id;
            Username = username;
            Password = password;
            Role = role;
        }

        public bool IsAdmin()
        {
            if (Role == "admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
