using CsvFileModels;
using Microsoft.EntityFrameworkCore;
using MacDeviceModels;

namespace DeviceContext;

public class DeviceDb : DbContext
{
    public DeviceDb(DbContextOptions<DeviceDb> options)
        : base(options) { }

    public DbSet<MacDevice> Devices => Set<MacDevice>();
    public DbSet<CsvFile> CsvFiles => Set<CsvFile>();
    

}
