using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.Drawing;
using System.Security.Cryptography;

namespace FileProviderTest.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        
        private readonly IFileProvider _fileProvider;        

        public FileUploadController(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;          
        }

        [HttpGet]
        public IActionResult GetImage([FromQuery] string fileName)
        {           
            //var fileInfo = _fileProvider.GetFileInfo($"/{fileName}");
            //return File(fileInfo.CreateReadStream(), "image/jpeg");

            var fileInfo = _fileProvider.GetFileInfo(fileName);
            if (!fileInfo.Exists)
            {
                return NotFound();
            }

            // 讀取文件內容並返回
            using (var stream = fileInfo.CreateReadStream())
            {
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                memoryStream.Position = 0;
                return File(memoryStream, "application/octet-stream", fileName);
            }
        }
    
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var fileProvider = _fileProvider as PhysicalFileProvider;
            var filePath = Path.Combine(fileProvider.Root, Path.GetRandomFileName() + ".jpg");
           
            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }
            return Ok("上傳成功");
        }
    }
}
