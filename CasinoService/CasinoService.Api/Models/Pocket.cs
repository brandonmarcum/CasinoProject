using System;
using System.Collections.Generic;

//MEANT TO BE USED FOR INTERACTION OF CHIPS WITH MONEY, MEANT FOR MAKING REAL WORLD TRANSACTIONS
//HOLDS ALL TYPES AND QUANTITIES OF CHIPS
namespace CasinoService.Api.Models
{
    public class Pocket
    {
        public List<Chips> AllChips{ get; set; }
        public double CashInPocket{ get; set; }
        public double CoinsInPocket{ get; set; }
        public Pocket(){
            AllChips = new List<Chips>();
            AllChips.Add(new Chips(){Type = ChipTypes.Orange, Amount = 0});
            AllChips.Add(new Chips(){Type = ChipTypes.Purple, Amount = 0});
            AllChips.Add(new Chips(){Type = ChipTypes.Black, Amount = 0});
            AllChips.Add(new Chips(){Type = ChipTypes.Green, Amount = 0});
            AllChips.Add(new Chips(){Type = ChipTypes.Blue, Amount = 0});
            AllChips.Add(new Chips(){Type = ChipTypes.Red, Amount = 0});
            AllChips.Add(new Chips(){Type = ChipTypes.White, Amount = 0});
        }
        public void cashOutPocket()
        {
            //convert all of the chips to money using chiphelper and add to user payment method
            ChipHelper ch = new ChipHelper();
            double cashValue = 0;
            foreach(var item in AllChips)
            {
                cashValue += ch.chipsToCash(item);
            }
            CashInPocket = cashValue;
        }
        public void convertToChips(double money)
        {
            //convert inserted money to chips for user and add to pocket
            if(CashInPocket - money > 0)
            {
                CashInPocket -= money;
                AllChips = new ChipHelper().convertCashToChips(money);
            }
            else
            {
                throw new Exception("Not Enough Cash in Pocket.");
            }
        }
        public void moneyToCoins(double money)
        {
            //converting to quarters for slot machine
            if(CashInPocket - money >= 0)
            {
            //remove the money being converted from the pocket
            CashInPocket = CashInPocket - money;

            //convert money into change
            int change = (int)(money - Math.Floor(money))*100;

            //change into quarters
            double quarters = (double)change/(double)25;

            //keep amount of cents over nearest quarter
            int centsOverQuarter = (int)(quarters - Math.Floor(quarters))*25;
            
            //put everything into pocket
            CoinsInPocket = Math.Floor(quarters);
            CashInPocket += centsOverQuarter/100;
            }
            else
            {
                throw new Exception("Not Enough Cash in Pocket.");
            }
        }
        public double getTotalMoney()
        {
            return CashInPocket + CoinsInPocket/4;
        }
        
    }
}