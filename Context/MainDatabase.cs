using Microsoft.EntityFrameworkCore;
using MacDeviceModels;

namespace MainDatabaseContext
{
    public class MainDatabase : DbContext
    {
        public MainDatabase(DbContextOptions<MainDatabase> options)
        : base(options) { }
        public DbSet<MacDevice> DevicesToMain => Set<MacDevice>();

    }
}