using Microsoft.EntityFrameworkCore;
using SystemSprawozdan.Backend.Data.Models.DbModels;

namespace SystemSprawozdan.Backend.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        //TODO: Kuba: wstawić encje bazy danych
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
    }
}
