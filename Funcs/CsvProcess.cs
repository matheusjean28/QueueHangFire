using ResponseMacListModel;
using GetFileCsvSavedFromServer1Funcs;
using MacDeviceModels;
using Read.Interfaces;
using System.Text.RegularExpressions;
using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using MethodsFuncs;
using DeviceContext;
using CustomExceptionFun;
namespace CsvProcessFuncs
{
    public class ReadCsv : IRead
    {
        private readonly string folderName = "Temp";
        private readonly string folderPath = Directory.GetCurrentDirectory();
        public async Task<IEnumerable<ResponseMacList>> ReadCsvItens(int id, DeviceDb db)
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
            var file = await getFileCsvSaved.GetCsvOnDatabaseAsync(db, 2);
            if(file != null )
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

                    if (device.Model.Length <= 0 || device.Model.Length >= 99)
                    {

                        string errorMessage = $"\n[Error Occurred at {DateTime.Now}] - Invalid Model: {device.Model}, MAC: {device.Mac}";
                        await File.AppendAllTextAsync(Path.Combine(_folderPath, "Error.csv"), errorMessage);
                        continue;
                    }
                    else
                    {
                        deviceItem.Model = device.Model;
                    }

                    macList.Add(deviceItem);

                    foreach (var item in macList)
                    {
                        await db.Devices.AddAsync(item);
                    }
                }
                catch (MacAlreadyExistsException ex)
                {
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

                    string errorMessage = $"\n[Error Occurred at {DateTime.Now}] - {ex.Message}";
                    await File.AppendAllTextAsync(Path.Combine(_folderPath, "Error.csv"), errorMessage);
                    continue;
                }
            }}
            await db.SaveChangesAsync();

            return processingResults;
        }

    }
};