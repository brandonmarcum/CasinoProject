
using Microsoft.EntityFrameworkCore;


namespace CasinoData.Db.DbContexts
{
    public class UserContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        protected override void  OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("data source=pizzastoredb.crpcv0ktk5tm.us-east-2.rds.amazonaws.com;database=casinodb; user id =jnr0303;password=Silversword1");
        }
    }
}