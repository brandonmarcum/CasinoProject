using System;
using System.Collections.Generic;
using System.Text;

namespace CasinoService.Api.Games.Bingo
{
    public class Bingo
    {
        public string GameName = "Bingo";
        public int chipLimit;
        public BingoCard bingoCard;
        public string status;

        public Bingo(int chips)
        {
            chipLimit = chips;
            bingoCard = new BingoCard();
            status = "playing";
        }

        public int RollNumber()
        {
            Random rand = new Random();
            return rand.Next(1, 76);
        }

        public void CommenceGame()
        {
            while(chipLimit > 0 && status == "playing")
            {
                int number = RollNumber();

                ChipNumber(number);

                CheckForBingo();

                chipLimit--;
            }

            if (status == "playing")
                status = "lose";
        }

        public void CheckForBingo()
        {
            if (bingoCard.CheckRows())
                status = "win";
            if (bingoCard.CheckCollumn())
                status = "win";
            if (bingoCard.CheckDiagonals())
                status = "win";
        }

        public void ChipNumber(int number)
        {
            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 5; c++)
                {
                    if (bingoCard.card[r][c] == number)
                        bingoCard.card[r][c] = 0;
                }
            }
        }
    }
}
