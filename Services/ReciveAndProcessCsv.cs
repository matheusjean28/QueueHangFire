using DeviceContext;

namespace ReciveAndProcessCsvServices
{
    public class ReciveAndProcessCsv
    {
        private readonly DeviceDb _db;
        public ReciveAndProcessCsv(DeviceDb db)
        {
            _db = db;
        }
    }
}