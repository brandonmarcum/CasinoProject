using System;
using System.Collections.Generic;

//MEANT TO BE USED FOR INTERACTION OF CHIPS WITH MONEY, MEANT FOR MAKING REAL WORLD TRANSACTIONS
//HOLDS ALL TYPES AND QUANTITIES OF CHIPS
namespace CasinoData.Db
{
    public class Pocket
    {
        public List<Chips> AllChips{ get; set; }
        public double CashInPocket{ get; set; }
        public int CoinsInPocket{ get; set; }
       // public ChipHelper ChipHelper{ get; set; }
        public Pocket(){
           
        }
    
        
    }
}