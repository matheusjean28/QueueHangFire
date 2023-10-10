using DeviceContext;
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

        public void ReciveAndProcessAsync(int id, DeviceDb _db)
        {
            ReadCsv readCsv = new();

            BackgroundJob.Enqueue(() =>
            readCsv.ReadCsvItens(id, _db));

        }

        public void ProcessCsvInBackground(int id)
        {
            ReadCsv readCsv = new();
            var result = readCsv.ReadCsvItens(id, _db).Result;
        }


    }
}