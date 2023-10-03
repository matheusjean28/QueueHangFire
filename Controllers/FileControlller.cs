using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileControllerControllers
{
    public class FileController
    {
        [ApiController]
        [Route("api/[controller]")]
        public class FileUploadController : ControllerBase
        {
            [HttpPost("upload")]
            public async Task<IActionResult> UploadFile(IFormFile file)
            {
                return Ok("Arquivo enviado com sucesso. Será processado em breve.");
            }
        }
    }
}
