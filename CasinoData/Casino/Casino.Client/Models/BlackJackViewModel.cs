using System;
using System.Collections.Generic;
using Casino.Library;
using Casino.Library.Games;
using Casino.Library.Models;
using Newtonsoft.Json;

namespace Casino.Client.Models
{
    public class BlackJackViewModel
    {
        public Blackjack Blackjack{ get; set; }

        public string RequestId { get; set; }
        public User User { get; set; }
        public string status { get; set; }

        [JsonIgnore]
        public IDictionary<string, int> Bet { get; set; }
        public BlackJackViewModel()
        {
            Blackjack = new Blackjack();
            User = new User();
            Bet = new Dictionary<string, int>();
        }
    }
}