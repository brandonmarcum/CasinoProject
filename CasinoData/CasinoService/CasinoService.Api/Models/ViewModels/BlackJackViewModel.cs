using System;
using System.Collections.Generic;
using CasinoService.Api.Games;
using CasinoService.Api.Models;
using Newtonsoft.Json;

namespace CasinoService.Api.Models.ViewModels
{
    public class BlackJackViewModel
    {
        public string RequestId { get; set; }
        public User User{ get; set; }
        public Blackjack Blackjack{ get; set; }
        
        [JsonIgnore]
        public IDictionary<string, int> Bet{get; set;}
        public BlackJackViewModel()
        {
            Blackjack = new Blackjack();
            User = new User();
            Bet = new Dictionary<string, int>();
        }
    }
}