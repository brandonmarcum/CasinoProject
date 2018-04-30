using System;
using Casino.Library.Games;
using Casino.Library.Models;

namespace Casino.Client.Models
{
    public class ChipHelperViewModel
    {
        public string RequestId { get; set; }
        public ChipHelper ChipHelper{ get; set; }
        public Chips Chips { get; set; }
        public ChipHelperViewModel()
        {
            ChipHelper = new ChipHelper();
            Chips = new Chips();
            Chips.Type = "white";
        }
    }
}