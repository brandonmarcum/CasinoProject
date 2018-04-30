using System;
using Xunit;
using Casino.Library.Games;

namespace Casino.Test
{
    public class BlackjackTest
    {
        Blackjack blackjack = new Blackjack();

        [Fact]
        public void T01Access()
        {
            Blackjack blackjack = new Blackjack();
            Assert.True(blackjack.status == "playing");
        }

        [Fact]
        public void T02PlayerHit()
        {
            Assert.True(blackjack.PlayerHit() == "playing");
            Assert.False(blackjack.playerTotal == 0);
        }

        [Fact]
        public void T03DealerHit()
        {
            Assert.True(blackjack.DealerHit() == "playing");
            Assert.False(blackjack.dealerTotal == 0);
        }

        [Fact]
        public void T04PlayerOver21()
        {
            for(int i = 0; i<21; i++)
            {
                blackjack.PlayerHit();
            }
            Assert.True(blackjack.PlayerHit() == "lose");
        }

        [Fact]
        public void T05DealerOver21()
        {
            for (int i = 0; i < 21; i++)
            {
                blackjack.DealerHit();
            }
            Assert.True(blackjack.DealerHit() == "win");
        }

        [Fact]
        public void T06DealerGreaterPlayer()
        {
            blackjack.dealerTotal = 20;
            blackjack.playerTotal = 19;
            Assert.True(blackjack.DecideOnWinner() == "lose");
        }

        [Fact]
        public void T07DealerEqualsPlayer()
        {
            blackjack.dealerTotal = 19;
            blackjack.playerTotal = 19;
            Assert.True(blackjack.DecideOnWinner() == "lose");
        }

        [Fact]
        public void T08DealerLessThanPlayer()
        {
            blackjack.dealerTotal = 18;
            blackjack.playerTotal = 19;
            Assert.True(blackjack.DecideOnWinner() == "win");
        }

        [Fact]
        public void T09Generate500Cards()
        {
            for(int i = 0; i <500; i++)
            {
                Assert.False(blackjack.GenerateCard() == 0);
            }
        }

        [Fact]
        public void T10Check21PlayerGreater()
        {
            blackjack.playerTotal = 22;
            Assert.True(blackjack.Check21("player") == "lose");
        }

        [Fact]
        public void T11Check21PlayerEqual()
        {
            blackjack.playerTotal = 21;
            Assert.True(blackjack.Check21("player") == "win");
        }

        [Fact]
        public void T12Check21PlayerLessButDealerStand()
        {
            blackjack.dealerStand = true;
            blackjack.dealerTotal = 18;
            blackjack.playerTotal = 19;
            Assert.True(blackjack.Check21("player") == "win");
        }

        [Fact]
        public void T13Check21DealerGreater()
        {
            blackjack.dealerTotal = 22;
            Assert.True(blackjack.Check21("dealer") == "win");
        }

        [Fact]
        public void T14Check21DealerEqual()
        {
            blackjack.dealerTotal = 21;
            Assert.True(blackjack.Check21("dealer") == "lose");
        }

        [Fact]
        public void T15Check21PlayerPlaying()
        {
            blackjack.playerTotal = 20;
            blackjack.dealerStand = false;
            Assert.True(blackjack.Check21("player") == "playing");
        }

        [Fact]
        public void T16Check21DealerPlaying()
        {
            blackjack.dealerTotal = 15;
            Assert.True(blackjack.Check21("dealer") == "playing");
        }

        [Fact]
        public void T17Check21PlayerLessButPlayerStand()
        {
            blackjack.PlayerStand();
            blackjack.dealerTotal = 19;
            blackjack.playerTotal = 18;
            Assert.True(blackjack.Check21("dealer") == "lose");
        }

        [Fact]
        public void T18NextTurnNoStand()
        {
            blackjack.playerTotal = 0;
            blackjack.dealerTotal = 0;
            blackjack.NextTurn();
            Assert.False(blackjack.playerTotal == 0);
            Assert.False(blackjack.dealerTotal == 0);
            Assert.True(blackjack.status == "playing");
        }

        [Fact]
        public void T19NextTurnPlayerStand()
        {
            blackjack.PlayerStand();
            blackjack.playerTotal = 0;
            blackjack.dealerTotal = 0;
            blackjack.NextTurn();
            Assert.True(blackjack.playerTotal == 0);
            Assert.False(blackjack.dealerTotal == 0);
            Assert.True(blackjack.status == "lose");
        }

        [Fact]
        public void T20NextTurnDealerStand()
        {
            blackjack.dealerStand = true;
            blackjack.playerTotal = 0;
            blackjack.dealerTotal = 0;
            blackjack.NextTurn();
            Assert.False(blackjack.playerTotal == 0);
            Assert.True(blackjack.dealerTotal == 0);
            Assert.True(blackjack.status == "win");
        }

        [Fact]
        public void T21CheckDealerStand()
        {
            blackjack.dealerTotal = 16;
            blackjack.CheckForDealerStand();
            Assert.True(blackjack.dealerStand);

            blackjack.dealerStand = false;
            blackjack.dealerTotal = 21;
            blackjack.CheckForDealerStand();
            Assert.True(blackjack.dealerStand);

            blackjack.dealerStand = false;
            blackjack.dealerTotal = 18;
            blackjack.CheckForDealerStand();
            Assert.True(blackjack.dealerStand);

            blackjack.playerTotal = 17;
            blackjack.dealerTotal = 18;
            blackjack.CheckForDealerStand();
            Assert.True(blackjack.dealerStand);
        }

        [Fact]
        public void T22CheckNotDealerStand()
        {
            blackjack.dealerTotal = 5;
            blackjack.CheckForDealerStand();
            Assert.False(blackjack.dealerStand);

            blackjack.dealerTotal = 22;
            blackjack.CheckForDealerStand();
            Assert.False(blackjack.dealerStand);

            blackjack.dealerTotal = 15;
            blackjack.CheckForDealerStand();
            Assert.False(blackjack.dealerStand);

            blackjack.playerTotal = 19;
            blackjack.dealerTotal = 18;
            blackjack.CheckForDealerStand();
            Assert.False(blackjack.dealerStand);
        }

        [Fact]
        public void T23GoldenTestA()
        {
            Blackjack trueBlackjack = new Blackjack();
            while(trueBlackjack.status == "playing")
            {
                trueBlackjack.NextTurn();
                if (trueBlackjack.playerTotal >= 16 && trueBlackjack.playerTotal > trueBlackjack.dealerTotal)
                    trueBlackjack.PlayerStand();
            }
        }

        [Fact]
        public void T24GoldenTestB()
        {
            //500 Games of Blackjack
            for (int i = 0; i < 500; i++)
            {
                Blackjack trueBlackjack = new Blackjack();
                while (trueBlackjack.status == "playing")
                {
                    trueBlackjack.NextTurn();
                    if (trueBlackjack.playerTotal >= 16 && trueBlackjack.playerTotal > trueBlackjack.dealerTotal)
                        trueBlackjack.PlayerStand();
                }
                Assert.False(trueBlackjack.playerTotal == 0);
                Assert.False(trueBlackjack.dealerTotal == 0);
                Assert.True(trueBlackjack.status == "win" || trueBlackjack.status == "lose");
            }
        }

    }
}
