using System;
using Xunit;
using System.Collections.Generic;
using Casino.Library.Games.Bingo;

namespace Casino.Test
{
    public class BingoTest
    {
        Bingo bingo = new Bingo(20);

        [Fact]
        public void T01Access()
        {
            Bingo bingo = new Bingo(20);
            Assert.True(bingo.status == "playing");
        }

        [Fact]
        public void T02RollNumber()
        {
            Bingo bingo = new Bingo(20);
            for(int i = 0; i < 2000; i++)
            {
                int x = bingo.RollNumber();
                Assert.True(x >= 1 && x <= 75);
            }
        }

        [Fact]
        public void T03FillCard()
        {
            Bingo bingo = new Bingo(20);
            for(int r = 0; r<5; r++)
            {
                for(int c = 0; c<5;c++)
                {
                    Assert.NotEqual(0, bingo.bingoCard.card[r][c]);
                }
            }
        }

        [Fact]
        public void T04MatchRowTrue()
        {
            Bingo bingo = new Bingo(20);
            for (int r = 0; r < 5; r++)
            {
                //Sets the row to chipped
                for (int c = 0; c < 5; c++)
                {
                    bingo.bingoCard.card[r][c] = 0;
                }
                Assert.True(bingo.bingoCard.CheckRows());
                //Refills the row
                for (int c = 0; c < 5; c++)
                {
                    bingo.bingoCard.card[r][c] = 1;
                }
            }
        }

        [Fact]
        public void T05MatchRowFalse()
        {
            Bingo bingo = new Bingo(20);
            for (int r = 0; r < 5; r++)
            {
                //Sets the row to chipped
                for (int c = 0; c < 5; c++)
                {
                    bingo.bingoCard.card[r][c] = 0;
                }
                bingo.bingoCard.card[r][4] = 1;
                Assert.False(bingo.bingoCard.CheckRows());
                //Refills the row
                for (int c = 0; c < 5; c++)
                {
                    bingo.bingoCard.card[r][c] = 1;
                }
                Assert.False(bingo.bingoCard.CheckRows());
            }
        }

        [Fact]
        public void T06MatchCollumnTrue()
        {
            Bingo bingo = new Bingo(20);
            for (int c = 0; c < 5; c++)
            {
                //Sets the collumn to chipped
                for (int r = 0; r < 5; r++)
                {
                    bingo.bingoCard.card[r][c] = 0;
                }
                Assert.True(bingo.bingoCard.CheckCollumn());
                //Refills the collumn
                for (int r = 0; r < 5; r++)
                {
                    bingo.bingoCard.card[r][c] = 1;
                }
            }
        }

        [Fact]
        public void T07MatchCollumnFalse()
        {
            Bingo bingo = new Bingo(20);
            for (int c = 0; c < 5; c++)
            {
                //Sets the collumn to chipped
                for (int r = 0; r < 5; r++)
                {
                    bingo.bingoCard.card[r][c] = 0;
                }
                bingo.bingoCard.card[4][c] = 1;
                Assert.False(bingo.bingoCard.CheckCollumn());
                //Refills the collumn
                for (int r = 0; r < 5; r++)
                {
                    bingo.bingoCard.card[r][c] = 1;
                }
            }
        }

        [Fact]
        public void T08MatchDiagonalTrue()
        {
            Bingo bingo = new Bingo(20);
            for (int i = 0; i < 5; i++)
            {
                bingo.bingoCard.card[i][i] = 0;
            }
            Assert.True(bingo.bingoCard.CheckDiagonals());
            for (int i = 0; i < 5; i++)
            {
                int j = 4 - i;
                bingo.bingoCard.card[i][j] = 0;
            }
            Assert.True(bingo.bingoCard.CheckDiagonals());
        }

        [Fact]
        public void T09MatchDiagonalFalse()
        {
            Bingo bingo = new Bingo(20);
            for (int i = 0; i < 5; i++)
            {
                bingo.bingoCard.card[i][i] = 0;
            }
            bingo.bingoCard.card[2][2] = 1;
            Assert.False(bingo.bingoCard.CheckDiagonals());
            for (int i = 0; i < 5; i++)
            {
                int j = 4 - i;
                bingo.bingoCard.card[i][j] = 0;
            }
            bingo.bingoCard.card[2][2] = 1;
            Assert.False(bingo.bingoCard.CheckDiagonals());
        }

        [Fact]
        public void T10MatchDiagonalTrue()
        {
            Bingo bingo = new Bingo(20);
            for (int i = 0; i < 5; i++)
            {
                bingo.bingoCard.card[i][i] = 0;
            }
            Assert.True(bingo.bingoCard.CheckDiagonals());
            for (int i = 0; i < 5; i++)
            {
                int j = 4 - i;
                bingo.bingoCard.card[i][j] = 0;
            }
            Assert.True(bingo.bingoCard.CheckDiagonals());
        }

        [Fact]
        public void T11CheckProperNumbers()
        {
            Bingo bingo = new Bingo(20);
            for (int i = 0; i < 500; i++)
            {
                List<int> x = bingo.bingoCard.FillRow();
                for(int j = 0; j < 5; j++)
                {
                    Assert.True(x[j] >= (j * 15) + 1 && x[j] <= (j + 1) * 15);
                }
            }
        }

        [Fact]
        public void T12CheckForBingo()
        {
            Bingo bingo1 = new Bingo(20);
            for (int r = 0; r < 5; r++)
            {
                //Sets the row to chipped
                for (int c = 0; c < 5; c++)
                {
                    bingo1.bingoCard.card[r][c] = 0;
                }
                Assert.True(bingo1.bingoCard.CheckRows());
            }
            bingo1.CheckForBingo();
            Assert.True(bingo1.status == "win");

            Bingo bingo2 = new Bingo(20);
            for (int c = 0; c < 5; c++)
            {
                //Sets the collumn to chipped
                for (int r = 0; r < 5; r++)
                {
                    bingo2.bingoCard.card[r][c] = 0;
                }
                Assert.True(bingo2.bingoCard.CheckCollumn());
            }
            bingo2.CheckForBingo();
            Assert.True(bingo2.status == "win");

            Bingo bingo3 = new Bingo(20);
            for (int i = 0; i < 5; i++)
            {
                bingo3.bingoCard.card[i][i] = 0;
            }
            Assert.True(bingo3.bingoCard.CheckDiagonals());
            bingo3.CheckForBingo();
            Assert.True(bingo3.status == "win");
        }

        [Fact]
        public void T13CheckForNoBingo()
        {
            Bingo bingo = new Bingo(20);
            bingo.CheckForBingo();
            Assert.True(bingo.status == "playing");
        }

        [Fact]
        public void T14GoldenTest()
        {
            for(int j = 0; j<500; j++)
            {
                Bingo bingo = new Bingo(20);
                BingoCard bc = bingo.bingoCard;
                bingo.CommenceGame();
                Assert.True(bc.card == bingo.bingoCard.card);
                Assert.True(bingo.status == "win" || bingo.status == "lose");
            }
        }
    }
}
