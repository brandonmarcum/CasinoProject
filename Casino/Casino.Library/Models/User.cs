using System;
using System.Collections.Generic;

namespace Casino.Library.Models
{
    public class User
    {
        public User()
        {
            UserID = DateTime.Now.Ticks;
            UserPocket = new Pocket();
        }
        public string Username{ get; set; }
        public string Password{ get; set; }
        public long UserID{ get; private set; }
        public string Name{ get; set; }
        public int Age{ get; set; }
        public string Email{ get; set; }
        public Pocket UserPocket{ get; set; }
        public double Cash{ get; set; }
    }
}