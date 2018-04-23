using System;
using Xunit;
using Casino.Library.Games;
using Casino.Library.Games.ChickenFight;

namespace Casino.Test
{
    public class ChickenTest
    {
        Fight fight = new Fight();

        [Fact]
        public void T01Access()
        {
            Fight fight = new Fight();
            Assert.True(fight.chickenA.Standing);
            Assert.True(fight.chickenB.Standing);
        }

        [Fact]
        public void T02PlaceBetA()
        {
            Fight fight = new Fight();
            fight.PlaceBetA();
            Assert.True(fight.chickenA.Betted);
        }

        [Fact]
        public void T04PlaceBetB()
        {
            Fight fight = new Fight();
            fight.PlaceBetB();
            Assert.True(fight.chickenB.Betted);
        }

        [Fact]
        public void T05GenerateStats()
        {
            Fight fight = new Fight();
            int statTotal = fight.chickenA.Attack + fight.chickenA.Evasion + fight.chickenA.Health;
            Assert.Equal(400, statTotal);
            statTotal = fight.chickenB.Attack + fight.chickenB.Evasion + fight.chickenB.Health;
            Assert.Equal(400, statTotal);
        }

        [Fact]
        public void T06CheckWin_Win()
        {
            Fight fight = new Fight();
            fight.PlaceBetA();
            fight.chickenB.Standing = false;
            Assert.True(fight.CheckWin() == "win");
            fight = new Fight();
            fight.PlaceBetB();
            fight.chickenA.Standing = false;
            Assert.True(fight.CheckWin() == "win");
        }

        [Fact]
        public void T07CheckWin_Lose()
        {
            Fight fight = new Fight();
            fight.PlaceBetA();
            fight.chickenA.Standing = false;
            Assert.True(fight.CheckWin() == "lose");
            fight = new Fight();
            fight.PlaceBetB();
            fight.chickenB.Standing = false;
            Assert.True(fight.CheckWin() == "lose");
        }

        [Fact]
        public void T08CheckWin_Lose()
        {
            Fight fight = new Fight();
            fight.PlaceBetA();
            fight.chickenA.Standing = false;
            Assert.True(fight.CheckWin() == "lose");
            fight = new Fight();
            fight.PlaceBetB();
            fight.chickenB.Standing = false;
            Assert.True(fight.CheckWin() == "lose");
        }

        [Fact]
        public void T09ChickenAAttack()
        {
            Fight fight = new Fight();
            fight.chickenB.Evasion = 0;
            fight.chickenB.Health = 600;
            fight.chickenA.Attack = 599;
            fight.ChickenATurn();
            Assert.Equal(1, fight.chickenB.Health);
        }

        [Fact]
        public void T10ChickenBAttack()
        {
            Fight fight = new Fight();
            fight.chickenA.Evasion = 0;
            fight.chickenA.Health = 600;
            fight.chickenB.Attack = 599;
            fight.ChickenBTurn();
            Assert.Equal(1, fight.chickenA.Health);
        }

        [Fact]
        public void T11CheckEvasionA()
        {
            Fight fight = new Fight();
            int x = fight.chickenA.Health;
            fight.chickenA.Evasion = fight.chickenA.StatTotal * 2;
            fight.ChickenBTurn();
            Assert.Equal(x, fight.chickenA.Health);
        }

        [Fact]
        public void T12CheckEvasionB()
        {
            Fight fight = new Fight();
            int x = fight.chickenB.Health; 
            fight.chickenB.Evasion = fight.chickenB.StatTotal * 2;
            fight.ChickenATurn();
            Assert.Equal(x, fight.chickenB.Health);
        }

        [Fact]
        public void T13GoldenTest()
        {
            Fight fight;
            Random rand;

            for(int i = 0; i < 500; i++)
            {
                fight = new Fight();
                rand = new Random();

                if (rand.Next(0, 2) == 0)
                    fight.PlaceBetA();
                else
                    fight.PlaceBetB();

                string status = fight.Engage();

                Assert.True(status == "win" || status == "lose");
            }
        }
    }
}
