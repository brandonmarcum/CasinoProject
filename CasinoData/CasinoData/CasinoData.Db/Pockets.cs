using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CasinoData.Db
{
    [Table("Pockets")]
    public partial class Pockets
    {
        public Pockets()
        {
            Users = new HashSet<Users>();
        }
        [Key]
        public int PocketID { get; set; }
        public decimal Cash { get; set; }
        public int Coins { get; set; }
        [ForeignKey("ChipsId")]
        public int? ChipsId { get; set; }

        public ICollection<Chips> AllChips { get; set; }

        public Chips Chips{ get; set; }
        [NotMapped]
        public ICollection<Users> Users { get; set; }
    }
}
