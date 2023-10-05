using Microsoft.AspNetCore.Mvc;
using DeviceContext;
using CsvProcessFuncs;
using CsvFileModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

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
        public async Task<IActionResult> UploadFile(DeviceDb db, IFormFile file)
        {

            if (file == null || file.Length == 0)
            {
                return BadRequest("File can not be empty .");
            }

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var csvFile = new CsvFile
            {
                FileName = file.FileName,
                Data = memoryStream.ToArray()
            };

            db.CsvFiles.Add(csvFile);
            await db.SaveChangesAsync();
            var FileName = csvFile.FileName;
            return Ok($"{FileName} was send with sucess");
        }

        [HttpPost("process-csv")]
        public async Task<IActionResult> ProcessCsv(int id)
        {
            ReadCsv readCsv = new();
            await readCsv.ReadCsvItens(id,_db); 

            return Ok("Processing csv.");
        }

        [HttpGet("upload")]
        public async Task<IActionResult> GetAllAsync(DeviceDb db)
        {
            try
            {
                var AllCsv = await db.CsvFiles.ToListAsync();
                return Ok(AllCsv);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error on locate files: {ex.Message}");
            }
        }
    }
}