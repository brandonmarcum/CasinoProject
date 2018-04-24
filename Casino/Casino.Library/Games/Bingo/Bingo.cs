using System;
using System.Collections.Generic;
using System.Text;
using Casino.Library.Models;

namespace Casino.Library.Games.Bingo
{
    public class Bingo
    {
        public string GameName = "Bingo";
        public int chipLimit;
        public BingoCard bingoCard;
        public string status;
        public List<int> usedNumbers;
        public int numberChosen;
        public Pocket GamePocket { get; set; }

        public Bingo()
        {
            chipLimit = 32;
            bingoCard = new BingoCard();
            status = "playing";
            usedNumbers = new List<int>();
            GamePocket = new Pocket();
            GamePocket.AllChips = new List<Chips>();
            numberChosen = 0;
        }

        public Bingo(int chips)
        {
            chipLimit = chips;
            bingoCard = new BingoCard();
            status = "playing";
            usedNumbers = new List<int>();
            GamePocket = new Pocket();
            GamePocket.AllChips = new List<Chips>();
        }

        public int RollNumber()
        {
            Random rand = new Random();
            int number;
            bool unique;

            do
            {
                unique = true;
                number = rand.Next(1, 76);
                foreach (var item in usedNumbers)
                {
                    if (item == number)
                        unique = false;
                }

            } while (!unique);

            numberChosen = number;

            return number;
        }

        public void CommenceGame()
        {
            if(chipLimit > 0 && status == "playing")
            {
                int number = RollNumber();

                ChipNumber(number);

                CheckForBingo();

                chipLimit--;
            }

            if (status == "playing" && chipLimit == 0)
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
