namespace CasinoService.Api.Models
{
    public abstract class CasinoGame
    {
        string GameName{ get; set; }

        //results of actual game sets Result field 0 for loss 1 for won
        int Result{ get; set; }
        //game returns amount earned for the win of this particular game
        double Earnings{ get; set; }

    }
}