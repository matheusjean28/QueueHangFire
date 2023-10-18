using Microsoft.AspNetCore.Mvc;
using ReciveAndProcessCsvServices;
using DeviceContext;
using MainDatabaseContext;
using CsvFileModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Hangfire;

namespace FileControllerControllers
{
    [ApiController]
    [Route("/")]
    public class FileController : ControllerBase
    {
        private readonly DeviceDb _db;
        private readonly MainDatabase _mainDatabase;

        public FileController(DeviceDb db, MainDatabase mainDatabase)
        {
            _mainDatabase = mainDatabase;
            _db = db;
        }

        //route test 
        [HttpPost("Upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("File can not be empty.");
                }

                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                var csvFile = new CsvFile
                {
                    Name = file.FileName,
                    Data = memoryStream.ToArray()
                };
                _db.CsvFiles.Add(csvFile);
                await _db.SaveChangesAsync();
                var FileName = csvFile.Name;
                return Ok($"{FileName} was sent successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error on file upload: {ex.Message}");
            }
        }

        [HttpPost("process-csv")]
        public IActionResult ProcessCsv(int id)
        {
            try
            {
                var idNumber = id;
                ReciveAndProcessCsv reciveAndProcessCsv = new(_db, _mainDatabase);
                BackgroundJob.Enqueue(() => reciveAndProcessCsv.ReciveAndProcessAsync(id, _mainDatabase));

                return Ok($"CSV processing for ID {idNumber} has been enqueued.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error on processing CSV: {ex.Message}");
            }
        }


        [HttpGet("upload")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var AllCsv = await _db.CsvFiles.ToListAsync();
                return Ok(AllCsv);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error on locate files: {ex.Message}");
            }
        }

        [HttpGet("MacsExists")]
        public async Task<IActionResult> GetExistentMacsync()
        {
            try
            {
                var AllCsv = await _mainDatabase.DevicesToMain.ToListAsync();
                return Ok(AllCsv);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error on locate files: {ex.Message}");
            }
        }

        [HttpDelete("DeleteMacsExists/{id}")]
        public async Task<IActionResult> DeleteExistentMacsync(int id)
        {
            var _deletedItem = await  _mainDatabase.DevicesToMain.FindAsync(id);
            if (_deletedItem == null)
            {
                return BadRequest($"item {id} was not found!");
            }
             _mainDatabase.DevicesToMain.Remove(_deletedItem);
            await _mainDatabase.SaveChangesAsync();
            return Ok($"item {id} was deleted!");
        }
    }
}
