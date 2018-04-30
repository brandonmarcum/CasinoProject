using System;
using Xunit;
using Casino.Library.Games;

namespace Casino.Test
{
    public class RockPaperScissorsTest
    {

        [Fact]
        public void T01ThrowRock()
        {
            RockPaperScissors rps = new RockPaperScissors();
            rps.MakeChoice("rock");
        }

        [Fact]
        public void T02ThrowPaper()
        {
            RockPaperScissors rps = new RockPaperScissors();
            rps.MakeChoice("paper");
        }

        [Fact]
        public void T03ThrowScissors()
        {
            RockPaperScissors rps = new RockPaperScissors();
            rps.MakeChoice("scissors");
        }

        [Fact]
        public void T04TestCPUChoices()
        {
            RockPaperScissors rps = new RockPaperScissors();
            rps.MakeChoice("paper");

            for (int i = 0; i < 500; i++)
            {
                string x = rps.GetCpuChoice();
                Assert.True(x == "rock" || x == "paper" || x == "scissors");
            }
        }

        [Fact]
        public void T05PRockCScissors()
        {
            RockPaperScissors rps = new RockPaperScissors();
            rps.MakeChoice("rock");
            rps.cpuChoice = "scissors";
            Assert.True(rps.GetResult() == "win");
        }

        [Fact]
        public void T06PRockCRock()
        {
            RockPaperScissors rps = new RockPaperScissors();
            rps.MakeChoice("rock");
            rps.cpuChoice = "rock";
            Assert.True(rps.GetResult() == "tie");
        }

        [Fact]
        public void T07PRockCPaper()
        {
            RockPaperScissors rps = new RockPaperScissors();
            rps.MakeChoice("rock");
            rps.cpuChoice = "paper";
            Assert.True(rps.GetResult() == "lose");
        }

        [Fact]
        public void T08PPaperCRock()
        {
            RockPaperScissors rps = new RockPaperScissors();
            rps.MakeChoice("paper");
            rps.cpuChoice = "rock";
            Assert.True(rps.GetResult() == "win");
        }

        [Fact]
        public void T09PPaperCPaper()
        {
            RockPaperScissors rps = new RockPaperScissors();
            rps.MakeChoice("paper");
            rps.cpuChoice = "paper";
            Assert.True(rps.GetResult() == "tie");
        }

        [Fact]
        public void T10PPaperCScissors()
        {
            RockPaperScissors rps = new RockPaperScissors();
            rps.MakeChoice("paper");
            rps.cpuChoice = "scissors";
            Assert.True(rps.GetResult() == "lose");
        }

        [Fact]
        public void T11PScissorsCRock()
        {
            RockPaperScissors rps = new RockPaperScissors();
            rps.MakeChoice("scissors");
            rps.cpuChoice = "rock";
            Assert.True(rps.GetResult() == "lose");
        }

        [Fact]
        public void T12ScissorsCPaper()
        {
            RockPaperScissors rps = new RockPaperScissors();
            rps.MakeChoice("scissors");
            rps.cpuChoice = "paper";
            Assert.True(rps.GetResult() == "win");
        }

        [Fact]
        public void T13PScissorsCScissors()
        {
            RockPaperScissors rps = new RockPaperScissors();
            rps.MakeChoice("scissors");
            rps.cpuChoice = "scissors";
            Assert.True(rps.GetResult() == "tie");
        }

        [Fact]
        public void T14GoldenTest()
        {
            RockPaperScissors rps = new RockPaperScissors();
            rps.MakeChoice("scissors");
            for (int i = 0; i < 500; i++)
            {
                string playerInput = rps.GetCpuChoice();
                RockPaperScissors trueRPS = new RockPaperScissors();
                trueRPS.MakeChoice(playerInput);
                string s = trueRPS.status;
                Assert.True(s == "win" || s == "lose" || s =="tie");
            }
            
        }
    }
}
