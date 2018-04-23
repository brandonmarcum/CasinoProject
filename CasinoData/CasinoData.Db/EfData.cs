using System.Collections.Generic;
using System.Linq;
using CasinoData.Db.DbContexts;
using Casino.Db;

namespace CasinoData.Db
{
    public class EfData
    {
        //add all contexts here to insert into database 

        private UserContext userdb = new UserContext();
        private PocketContext pocketdb = new PocketContext();
        private ChipsContext chipsdb = new ChipsContext();


        public bool InsertUser(Users user)
        {
            userdb.Users.AddRange
            (
                user
            );

            return userdb.SaveChanges() != 0;

        }

        public bool InsertPocket(Pockets pocket)
        {
            pocketdb.Pockets.AddRange
            (
                pocket

            );

            return pocketdb.SaveChanges() != 0;
        }
        public bool InsertChips(Chips chips)
        {
            chipsdb.Chips.AddRange
            (
                chips

            );

            return chipsdb.SaveChanges() != 0;
        }
    }
}