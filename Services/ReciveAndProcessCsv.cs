using DeviceContext;
using CsvProcessFuncs;
using Hangfire;
using MainDatabaseContext;
using Microsoft.Extensions.Logging;


namespace ReciveAndProcessCsvServices
{
    public class ReciveAndProcessCsv
    {

        private readonly MainDatabase _mainDatabase;
        private readonly DeviceDb _db;
        private readonly ILogger<ReadCsv> _logger;


        public ReciveAndProcessCsv(DeviceDb db, MainDatabase mainDatabase, ILogger<ReadCsv> logger)
        {
            _db = db;
            _logger = logger;
            _mainDatabase = mainDatabase;
        }

        public async void ReciveAndProcessAsync(int id, MainDatabase mainDatabase)
        {
            try
            {
                ReadCsv readCsv = new(_logger);
                await readCsv.ReadCsvItens(id, _db, mainDatabase);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public async Task ProcessCsvInBackgroundAsync(int id)
        {
            ReadCsv readCsv = new(_logger);
            var result = await readCsv.ReadCsvItens(id, _db, _mainDatabase);
        }



    }
}