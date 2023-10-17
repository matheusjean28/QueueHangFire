using DeviceContext;
using CsvProcessFuncs;
using Hangfire;
using MainDatabaseContext;

namespace ReciveAndProcessCsvServices
{
    public class ReciveAndProcessCsv
    {

         private readonly MainDatabase _mainDatabase;
         private readonly DeviceDb _db;

        public ReciveAndProcessCsv(DeviceDb db, MainDatabase mainDatabase)
        {
            _db = db;
            _mainDatabase = mainDatabase;
        }

        public void ReciveAndProcessAsync(int id, MainDatabase mainDatabase)
        {
            ReadCsv readCsv = new();

            BackgroundJob.Enqueue(() =>
            readCsv.ReadCsvItens(id, _db,mainDatabase));

        }

        public void ProcessCsvInBackground(int id)
        {
            ReadCsv readCsv = new();
            var result = readCsv.ReadCsvItens(id, _db, _mainDatabase ).Result;
        }


    }
}