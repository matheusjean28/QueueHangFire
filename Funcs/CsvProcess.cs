using ResponseMacListModel;
using GetFileCsvSavedFromServer1Funcs;
using MacDeviceModels;
using Read.Interfaces;
using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper;
using MethodsFuncs;
using DeviceContext;
using CustomExceptionFun;
using MainDatabaseContext;
using Microsoft.Extensions.Logging;
namespace CsvProcessFuncs
{
    public class ReadCsv : IRead
    {
        private readonly ILogger<ReadCsv> _logger;
        private readonly string folderName = "Temp";
        private readonly string folderPath = Directory.GetCurrentDirectory();
        public ReadCsv(ILogger<ReadCsv> logger)
        {
            _logger = logger;
        }
        public async Task<IEnumerable<ResponseMacList>> ReadCsvItens(int id, DeviceDb db, MainDatabase mainDatabase)
        {
            GetFileCsvSaved getFileCsvSaved = new();
            List<ResponseMacList> processingResults = new();
            var _folderPath = folderPath;
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }
            _folderPath = Path.GetFullPath(folderName);


            List<MacDevice> macList = new();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };
            var file = await getFileCsvSaved.GetCsvOnDatabaseAsync(db, id);
            if (file != null)
            {

                using var stream = new MemoryStream(file.Data);
                using var reader = new StreamReader(stream);
                using var csv = new CsvReader(reader, config);


                await foreach (var device in csv.GetRecordsAsync<MacDevice>())
                {
                    try
                    {
                        MacDevice deviceItem = new();
                        Methods methods = new();
                        ResponseMacList result = new();

                        var checkStrin = await methods.IsValidMacAddress(db, device.Mac);

                        if (checkStrin != null)
                        {
                            result.Mac = device.Mac;
                            result.MacExists = false;
                            result.CreatedSuccessfully = true;
                            result.Message = $"The {device.Mac} was created with sucess!";
                            processingResults.Add(result);

                            deviceItem.Mac = device.Mac;
                        }

                        if (device.Model.Length < 1 || device.Model.Length > 98)
                        {

                            string errorMessage = $"\n[Error Occurred at {DateTime.Now}] - Invalid Model: {device.Model}, MAC: {device.Mac}";
                            await File.AppendAllTextAsync(Path.Combine(_folderPath, "Error.csv"), errorMessage);
                        }
                        else
                        {
                            deviceItem.Model = device.Model;
                        }

                        macList.Add(deviceItem);

                        foreach (var item in macList)
                        {
                            await mainDatabase.DevicesToMain.AddAsync(item);
                        }
                    }
                    catch (MacAlreadyExistsException ex)
                    {
                        _logger.LogError(ex, ex.Message);
                        ResponseMacList result = new()
                        {
                            Mac = device.Mac,
                            MacExists = true,
                            CreatedSuccessfully = false,
                            Message = $"The {device.Mac} Already exists!"

                        };

                        processingResults.Add(result);
                        List<string> AlreadyExistsMacs = new()
                    {
                        $"{ex.Message}"
                    };

                        processingResults.Add(result);
                        AlreadyExistsMacs.Add(ex.Message);

                        string errorFilePath = Path.Combine(_folderPath, "Error.csv");

                        string errorMessage = $"\n[Error Occurred at {DateTime.Now}] - {ex.Message}";
                        await File.AppendAllTextAsync(errorFilePath, errorMessage);
                        await File.AppendAllTextAsync(Path.Combine(_folderPath, "Error.csv"), errorMessage);
                    }
                }
            }
            await mainDatabase.SaveChangesAsync();
            await db.SaveChangesAsync();

            return processingResults;
        }

    }
};