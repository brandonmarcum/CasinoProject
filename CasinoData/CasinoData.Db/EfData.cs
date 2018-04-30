using System.Collections.Generic;
using System.Linq;
using CasinoData.Db.DbContexts;

namespace CasinoData.Db
{
    public class EfData
    {
        //add all contexts here to insert into database 


        private casinodbContext cc = new casinodbContext();


        public bool InsertUser(Users user)
        {
            cc.Users.AddRange
            (
                user
            );

            return cc.SaveChanges() != 0;

        }

        public bool InsertPocket(Pockets pocket)
        {
            cc.Pockets.AddRange
            (
                pocket

            );

            return cc.SaveChanges() != 0;
        }
        public bool InsertChips(Chips chips)
        {
            cc.Chips.AddRange
            (
                chips

            );

            return cc.SaveChanges() != 0;
        }
    }
}