using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CasinoData.Db
{
    public partial class Users
    {
        public Users()
        {
            
        }
        [Key]
        public int Id { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public int? UserPocketId { get; set; }
        public string Password { get; set; }

        public Pockets UserPocket { get; set; }
    }
}
