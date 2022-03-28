using DempApiCore.Data.Entitties;
using Microsoft.EntityFrameworkCore;

namespace DempApiCore.Data
{
    public class DBContext: DbContext
    {
        public DBContext(DbContextOptions<DBContext> options):base(options)
        {
        }

        public DbSet<Products> Products { set; get; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // optionsBuilder.UseSqlServer("Server =.;Databse=PracticeApi; User ID=sa;Password=Foyaz123;providerName = System.Data.SqlClient");
        //    optionsBuilder.UseSqlServer("Server =.;Databse=PracticeApi;Integrated Security= True");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
