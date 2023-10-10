using Microsoft.AspNetCore.Mvc;
using ReciveAndProcessCsvServices;
using DeviceContext;
using CsvProcessFuncs;
using CsvFileModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using System;
using System.IO;
using System.Threading.Tasks;
using CsvSerializeDataViewModels;
using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FileControllerControllers
{
    [ApiController]
    [Route("/")]
    public class FileController : ControllerBase
    {
        private readonly DeviceDb _db;

        public FileController(DeviceDb db)
        {
            _db = db;
        }

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
                    FileName = file.FileName,
                    Data = memoryStream.ToArray()
                };

                _db.CsvFiles.Add(csvFile);
                await _db.SaveChangesAsync();
                var FileName = csvFile.FileName;
                return Ok($"{FileName} was sent successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error on file upload: {ex.Message}");
            }
        }

        [HttpPost("process-csv")]
        public IActionResult ProcessCsv(int id, DeviceDb db)
        {
            try
            {
                ReciveAndProcessCsv reciveAndProcessCsv = new(db);
                BackgroundJob.Enqueue(() => reciveAndProcessCsv.ProcessCsvInBackground(id));

                return Accepted($"CSV processing for ID {id} has been enqueued.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error on processing CSV: {ex.Message}");
            }
        }

         [HttpPost("recive-process")]
        public IActionResult Recive(int id)
        {
         return Ok($"the id has recived with sucess");
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
                var AllCsv = await _db.Devices.ToListAsync();
                return Ok(AllCsv);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error on locate files: {ex.Message}");
            }
        }


    }
}
