using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Casino.Library.Games.Bingo;
using Casino.Library.Models;
using Newtonsoft.Json;

namespace Casino.Client.Models
{
    public class BingoViewModel
    {
        public Bingo bingo { get; set; }
        public User User { get; set; }
        public string status { get; set; }
        public string RequestId { get; set; }

        [JsonIgnore]
        public IDictionary<string, int> Bet { get; set; }
        public BingoViewModel()
        {
            bingo = new Bingo(40);
            User = new User();
            Bet = new Dictionary<string, int>();
        }
    }
}
