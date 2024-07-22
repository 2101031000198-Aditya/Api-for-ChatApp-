using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chatapp.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public string MessageText { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
    }

    public class UserLoginInfo
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserRegistrationInfo
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }

}