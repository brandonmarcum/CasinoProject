using System;
using System.Collections.Generic;
using System.Text;

namespace CasinoService.Api.Games
{
    public class Slots
    {
        public string GameName = "Slots";
        public int left { get; set; }
        public int middle { get; set; }
        public int right { get; set; }
        public string status { get; set; }

        public Slots()
        {
            left = 0;
            middle = 0;
            right = 0;
            status = "playing";
        }

        public void SetLeft()
        {
            left = ChangeSlot();
        }

        public void SetMiddle()
        {
            middle = ChangeSlot();
        }

        public void SetRight()
        {
            right = ChangeSlot();
        }

        public void SetSlots()
        {
            SetLeft();
            SetMiddle();
            SetRight();
        }
        public void StopPlaying()
        {
            status = "finished";
        }


        public int ChangeSlot()
        {
            Random random = new Random();
            //7 Slot (Rare)
            //give back 1000 for 3 pairs
            if (random.Next(0, 8) == 1)
                return 7;
            //Gold Slot (Uncommon)
            //give back 500 for 3 pairs
            if (random.Next(0, 4) == 1)
                return 1;
            //Silver Slot (Somewhat Common)
            //give back 50 for 3 pairs
            if (random.Next(0, 3) == 1)
                return 2;
            //Bronze Slot (Extremely Common)
            //give back 25 for 3 pairs
            else
                return 3;
        }

        public string CheckForMatch()
        {
            if (left == middle && middle == right)
                return "win";

            return "lose";
        }
    }
}
