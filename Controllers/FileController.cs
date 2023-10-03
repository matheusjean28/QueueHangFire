using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileControllerControllers
{
    [ApiController]
    [Route("/")]
    public class FileController : ControllerBase
    {
        [HttpPost("Upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {

            return Ok("Arquivo enviado com sucesso. Será processado em breve.");
        }
    }
}
