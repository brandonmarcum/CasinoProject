using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CasinoData.Db
{
    [Table("Chips")]
    public partial class Chips
    {
        public Chips()
        {
            Pockets = new HashSet<Pockets>();
        }
        [Key]
        public int ChipsId { get; set; }
        public int Amount { get; set; }
        public string Type { get; set; }

        public ICollection<Pockets> Pockets { get; set; }
    }
}
