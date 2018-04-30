using System;
using System.Collections.Generic;
using System.Text;
using Casino.Library.Models;

namespace Casino.Library.Games
{
    public class RockPaperScissors
    {
        public string GameName = "Rock-Paper-Scissors";
        public string playerChoice { get; set; }
        public string cpuChoice { get; set; }
        public string status { get; set; }
        public Pocket GamePocket { get; set; }

        public RockPaperScissors()
        {
            GamePocket = new Pocket();
            GamePocket.AllChips = new List<Chips>();
            status = "playing";
        }

        public void MakeChoice(string choice)
        {
            playerChoice = choice;
            cpuChoice = GetCpuChoice();

            status = GetResult();
        }

        public string GetCpuChoice()
        {
            Random rand = new Random();
            switch(rand.Next(0,3))
            {
                case 0: return "rock";
                case 1: return "paper";
                case 2: return "scissors";
                default: throw new Exception("RPS failed @ GetCpuChoice, int rand = " + rand);
            }
        }

        public string GetResult()
        {
            if (cpuChoice.Equals(playerChoice))
                return "tie";
            else if (CheckWin())
                return "win";
            else if (CheckLose())
                return "lose";
            else
                throw new Exception("RockPaperScissors does not satisfy a status statement");
        }

        private bool CheckWin()
        {
            if (playerChoice == "paper" && cpuChoice == "rock")
                return true;
            if (playerChoice == "rock" && cpuChoice == "scissors")
                return true;
            if (playerChoice == "scissors" && cpuChoice == "paper")
                return true;

            return false;
        }

        private bool CheckLose()
        {
            if (playerChoice == "rock" && cpuChoice == "paper")
                return true;
            if (playerChoice == "scissors" && cpuChoice == "rock")
                return true;
            if (playerChoice == "paper" && cpuChoice == "scissors")
                return true;

            return false;
        }

    }
}
