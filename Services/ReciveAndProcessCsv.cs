using DeviceContext;
using CsvFileModels;
using CsvProcessFuncs;
using Hangfire;

namespace ReciveAndProcessCsvServices
{
    public class ReciveAndProcessCsv
    {
        private readonly DeviceDb _db;
        public ReciveAndProcessCsv(DeviceDb db)
        {
            _db = db;
        }

        public void ReciveAndProcessAsync(int id,DeviceDb db)
        {   
            ReadCsv readCsv = new();
             BackgroundJob.Enqueue(() => readCsv.ReadCsvItens(id, db));
            
        }
        
        
        
    }
}