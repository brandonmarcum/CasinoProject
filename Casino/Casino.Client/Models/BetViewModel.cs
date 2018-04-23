using System.Collections.Generic;
using Casino.Library.Models;
using Newtonsoft.Json;

namespace Casino.Client.Models
{
    public class BetViewModel
    {
        public string RequestId { get; set; }
        public User User { get; set; }
        public string status { get; set; }

        [JsonIgnore]
        public IDictionary<string, int> Bet { get; set; }
    }
}
