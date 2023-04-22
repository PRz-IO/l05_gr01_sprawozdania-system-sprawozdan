using Microsoft.EntityFrameworkCore;
using SystemSprawozdan.Backend.Data.Models.DbModels;

namespace SystemSprawozdan.Backend.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        //TODO: Kuba: wstawić encje bazy danych
        public DbSet<Student> Student { get; set; }
        public DbSet<Major> Major { get; set; }
        public DbSet<Term> Term { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<SubjectGroup> SubjectGroup { get; set; }
        public DbSet<SubjectSubgroup> SubjectSubgroup { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<ReportTopic> ReportTopic { get; set; }
        public DbSet<StudentReport> StudentReport { get; set; }
        public DbSet<ReportComment> ReportComment { get; set; }
        public DbSet<StudentReportFile> StudentReportFile { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
    }
}
