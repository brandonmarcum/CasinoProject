
using Casino.Db;
using Microsoft.EntityFrameworkCore;


namespace CasinoData.Db.DbContexts
{
    public class PocketContext : DbContext
    {
        public DbSet<Pockets> Pockets { get; set; }
        protected override void  OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("data source=pizzastoredb.crpcv0ktk5tm.us-east-2.rds.amazonaws.com;initial catalog=casinodb; user id =jnr0303;password=Silversword1");
        }
    }
}