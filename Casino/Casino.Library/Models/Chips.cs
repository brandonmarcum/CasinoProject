namespace Casino.Library.Models
{
    public class Chips
    {
        public Chips()
        {
            Amount = 0;
            Type = ChipTypes.White;

        }
        public int Amount { get; set; }
        public string Type { get; set; }

        public ChipTypes ChipTypes{ get; }
        public int Value
        { 
            get
            {
                
                
                switch (Type)
                {
                    case (ChipTypes.White):   
                        return 1;
                    case (ChipTypes.Red):
                       return 5;
                    case (ChipTypes.Blue):
                        return 10;
                    case (ChipTypes.Green):
                        return 25;
                    case (ChipTypes.Black):
                       return 100;
                    case (ChipTypes.Purple):
                       return 500;
                    case (ChipTypes.Orange):
                        return 1000;
                    default:
                        return -1;
                }
            }
        }
        
    }
}