using System;
using System.Collections.Generic;

namespace CasinoService.Api.Models
{

    //CHIPHELPER IS MEANT TO BE USED WITH POCKET, USERS, GAMES (WHATEVER DEALS WITH CHIPS) TO CALCULATE AND MANAGE CHIPS
    public class ChipHelper
    {
        public int totalChipsOfTypeInPocket(Pocket pocket, string type)
        {
            int count = 0;
            switch (type)
            {
                case (ChipTypes.White):
                    foreach(var item in pocket.AllChips)
                    {
                        if(item.Type.Equals(ChipTypes.White))
                        {
                            count++;
                        }
                    }
                break;
                 case (ChipTypes.Red):
                    foreach(var item in pocket.AllChips)
                    {
                        if(item.Type.Equals(ChipTypes.Red))
                        {
                            count++;
                        }
                    }
                break;
                 case (ChipTypes.Blue):
                    foreach(var item in pocket.AllChips)
                    {
                        if(item.Type.Equals(ChipTypes.Blue))
                        {
                            count++;
                        }
                    }
                break;
                 case (ChipTypes.Green):
                    foreach(var item in pocket.AllChips)
                    {
                        if(item.Type.Equals(ChipTypes.Green))
                        {
                            count++;
                        }
                    }
                break;
                 case (ChipTypes.Black):
                    foreach(var item in pocket.AllChips)
                    {
                        if(item.Type.Equals(ChipTypes.Black))
                        {
                            count++;
                        }
                    }
                break;
                 case (ChipTypes.Purple):
                    foreach(var item in pocket.AllChips)
                    {
                        if(item.Type.Equals(ChipTypes.Purple))
                        {
                            count++;
                        }
                    }
                break;
                 case (ChipTypes.Orange):
                    foreach(var item in pocket.AllChips)
                    {
                        if(item.Type.Equals(ChipTypes.Orange))
                        {
                            count++;
                        }
                    }
                break;
            }
            
            return count;
        }

        public void AddToPocket(Pocket pocket, Chips chips, int amount)
        {
            //chips.Amount = amount;
            //pocket.AllChips.Add(chips);

            foreach(var item in pocket.AllChips)
            {
               
                if(item.Type.Equals(chips.Type))
                {
                    if(amount>0)
                    {
                        item.Amount += amount/item.Value;
                        amount -= item.Value*(amount/item.Value);
                        //System.Console.WriteLine("Remove From Pocket: " + item.Amount + " amount left to remove " + amount);
                    }
                }
            }
        }
        public void RemoveFromPocket(Pocket pocket, Chips chips, int amount)
        {
            //pocket.AllChips.Remove(chips);
            // if((chips.Amount - amount) > 0)
            // {
            //     chips.Amount = chips.Amount - amount;
            // }
            // else 
            // {
            //     chips.Amount = 0;
            // }
            // pocket.AllChips.Add(chips);
            foreach(var item in pocket.AllChips)
            {
                if(item.Type.Equals(chips.Type))
                {
                    if(amount>0)
                    {
                        item.Amount -= amount/item.Value;
                        amount -= item.Value*(amount/item.Value);
                        //System.Console.WriteLine("Remove From Pocket: " + item.Amount + " amount left to remove " + amount);
                    }
                }
            }

        }

        public double chipsToCash(Chips chips)
        {
            return chips.Amount*chips.Value;
        }
        public List<Chips> convertCashToChips(double money)
        {
            Chips OrangeChips = new Chips();
            Chips PurpleChips = new Chips();
            Chips BlackChips = new Chips();
            Chips GreenChips = new Chips();
            Chips BlueChips = new Chips();
            Chips RedChips = new Chips();
            Chips WhiteChips = new Chips();

            OrangeChips.Amount = (int)Math.Floor(money/1000);
            OrangeChips.Type = ChipTypes.Orange;
            money -= (int)Math.Floor(money/1000)*1000;

            PurpleChips.Amount = (int)Math.Floor(money/500);
            PurpleChips.Type = ChipTypes.Purple;
            money -= (int)Math.Floor(money/500)*500;

            BlackChips.Amount = (int)Math.Floor(money/100);
            BlackChips.Type = ChipTypes.Black;
            money -= (int)Math.Floor(money/100)*100;

            GreenChips.Amount = (int)Math.Floor(money/25);
            GreenChips.Type = ChipTypes.Green;
            money -= (int)Math.Floor(money/25)*25;

            BlueChips.Amount = (int)Math.Floor(money/10);
            BlueChips.Type = ChipTypes.Blue;
            money -= (int)Math.Floor(money/10)*10;

            RedChips.Amount = (int)Math.Floor(money/5);
            RedChips.Type = ChipTypes.Red;
            money -= (int)Math.Floor(money/5)*5;

            WhiteChips.Amount = (int)Math.Floor(money);
            WhiteChips.Type = ChipTypes.White;
            money -= (int)Math.Floor(money);

            List<Chips> convertedChips = new List<Chips>();
            
            convertedChips.Add(OrangeChips);
            convertedChips.Add(PurpleChips);
            convertedChips.Add(BlackChips);
            convertedChips.Add(GreenChips);
            convertedChips.Add(BlueChips);
            convertedChips.Add(RedChips);
            convertedChips.Add(WhiteChips);

            return convertedChips;
        }
        public void convertChips(int chipAmount)
        {
            Chips OrangeChips = new Chips();
            Chips PurpleChips = new Chips();
            Chips BlackChips = new Chips();
            Chips GreenChips = new Chips();
            Chips BlueChips = new Chips();
            Chips RedChips = new Chips();
            Chips WhiteChips = new Chips();

            
        }

        public void betChips(Pocket pocket, int bet)
        {
        }
    
        
        
    }
}