using Microsoft.AspNetCore.Identity;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Backend.Data.Models.Dto;

namespace SystemSprawozdan.Backend.Data.Seeder
{
    public class DbSeeder
    {
        private readonly ApiDbContext _dbContext;
        private readonly IPasswordHasher<Admin> _passwordHasher;

        public DbSeeder(ApiDbContext dbContext, IPasswordHasher<Admin> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Admin.Any())
                {
                    var admin = GetAdmin();
                    _dbContext.Admin.AddRange(admin);
                    _dbContext.SaveChanges();
                }
            }
        }

        private Admin GetAdmin()
        {
            var admin = new Admin()
            {
                Id = 1,
                Login = "admin"
            };

            admin.Password = _passwordHasher.HashPassword(admin, "admin");
            return admin;
        }
    }
}
