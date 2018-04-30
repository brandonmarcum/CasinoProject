using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Casino.Library.Models;

namespace Casino.Client.Models
{
    public class UserProfileViewModel
    {
        public string username { get; set; }
        public User User{ get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public double WinLose { get; set; }
        public int White { get; set; }
        public int Red { get; set; }
        public int Blue { get; set; }
        public int Green { get; set; }
        public int Black { get; set; }
        public int Purple { get; set; }
        public int Orange { get; set; }
        public int TotalChips { get; set; }
        public string MostPlayed { get; set; }
        public UserProfileViewModel()
        {
            username = "Brandon Marcum";
            User = new User();
            Wins = 8;
             Losses = 3;
             WinLose = Wins / Losses;
             White = 50;
             Red = 50;
             Blue = 50;
             Green = 50;
             Black = 50;
             Purple = 50;
             Orange = 50;
             TotalChips = White + Red + Blue + Green + Black + Purple + Orange;
             MostPlayed = "Russian Roulette";
        }
    }
}
