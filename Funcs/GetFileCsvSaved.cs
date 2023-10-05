using DeviceContext;
namespace GetFileCsvSavedFromServer1Funcs
{
    public class GetFileCsvSaved
    {
        public async Task<CsvFileModels.CsvFile?> GetCsvOnDatabaseAsync (DeviceDb db,int id)
        {
         var _File = await db.CsvFiles.FindAsync(id);
         if (_File == null)
         {
            return null;
         }
         return _File;   
        }
    }
}