using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CasinoData.Db
{
    public partial class Pockets
    {
        public Pockets()
        {
            Users = new HashSet<Users>();
        }
        [Key]
        public int PocketId { get; set; }
        public decimal Cash { get; set; }
        public int Coins { get; set; }
        public int? ChipsId { get; set; }

        public Chips Chips { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
