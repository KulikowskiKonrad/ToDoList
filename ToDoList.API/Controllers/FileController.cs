using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.BL.Helpers;
using ToDoList.BL.LogicInterfaces;
using ToDoList.BL.ServiceInterfaces;

namespace ToDoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IPdfService _pdfService;
        private readonly IUserLogic _userLogic;

        public FileController(IFileService fileService, IUserLogic userLogic, IPdfService pdfService)
        {
            _fileService = fileService;
            _userLogic = userLogic;
            _pdfService = pdfService;
        }

        [HttpPost("upload")]
        public IActionResult Upload(IFormFile file)
        {
            var userId = _userLogic.GetCurrentUserId();
            using (var fileStream = file.OpenReadStream())
            {
                _fileService.Upload(fileStream, file.FileName, userId);
            }

            return Ok();
        }

        [HttpPost("uploadPdf")]
        public IActionResult UploadPdf(IFormFile file)
        {
            var userId = _userLogic.GetCurrentUserId();
            using (var fileStream = file.OpenReadStream())
            {
                _fileService.Upload(fileStream, file.FileName, userId, "documents");
            }

            return Ok();
        }

        [HttpPost("uploadLogo")]
        public IActionResult UploadLogo(IFormFile file)
        {
            var userId = _userLogic.GetCurrentUserId();
            using (var fileStream = file.OpenReadStream())
            {
                _fileService.Upload(fileStream, file.FileName, userId, "images/logo");
            }

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = _fileService.Delete(id);
            if (result == DeleteFileResult.FileNotFound)
            {
                return BadRequest($"{DeleteFileResult.FileNotFound.GetDescription()}");
            }

            return Ok();
        }

        [HttpGet("get/{id}")]
        public IActionResult Get(Guid id)
        {
            var file = _fileService.GetFileBytes(id);

            return File(file, "application/octet-stream");
        }

        [HttpPost("createPdf")]
        public IActionResult CreatePdf()
        {
            var fileName = $"{DateTime.UtcNow.Date.ToShortDateString()}_test.pdf";
            var file = _pdfService.GeneratePdf();
            var userId = _userLogic.GetCurrentUserId();
            using (var stream = new MemoryStream(file))
            {
                _fileService.Upload(stream, fileName, userId, "documents/pdf");
            }

            return File(file, "application/pdf", fileName);
        }
    }
}