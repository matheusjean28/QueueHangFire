using DeviceContext;
using Microsoft.EntityFrameworkCore;
namespace GetFileCsvSavedFromServer1Funcs
{
    public class GetFileCsvSaved
    {
        public async Task<MacDeviceModels.MacDevice?> GetCsvOnDatabaseAsync (DeviceDb db,int id)
        {
         var _File = await db.Devices.FindAsync(id);
         return _File;   
        }
    }
}