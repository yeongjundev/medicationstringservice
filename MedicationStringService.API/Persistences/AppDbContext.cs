namespace MedicationStringService.API.Persistences
{
    public class AppDbContext : DbContext
    {
        public DbSet<MedicationString> MedicationStrings { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}