using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendIO.Application.Dto;
using SendIO.Application.Interfaces;
using SendIO.Application.Services;
using SendIO.Domain.Entities;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SendIO.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class FileHeadController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMinIO _minIO;

        public FileHeadController(IUnitOfWork unitOfWork, IMinIO minIO)
        {
            _unitOfWork = unitOfWork;
            _minIO = minIO;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<FileHead>> Get()
        {
            var data = await _unitOfWork.fileHeadRepository.GetAll();
            return (IEnumerable<FileHead>)data;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<FileHead> Get(Guid id)
        {
            var data = await _unitOfWork.fileHeadRepository.FindBy(x => x.Id == id, z => z.FileContents);
            return data.First();
        }


        [HttpPost("Add")]
        public async void Add([FromBody] FileHead fileHead)
        {
            var id = await _unitOfWork.fileHeadRepository.Add(fileHead);
            _unitOfWork.Complete();
        }

        [HttpPost("Upload")]
        [RequestSizeLimit(2000 * 1024 * 1024)]
        [RequestFormLimits(MultipartBodyLengthLimit = 2000 * 1024 * 1024)]
        public async Task<IActionResult> Upload(FileHeadUploadDTO fileHeadUploadDTO, List<IFormFile> files)
        {
            if (files.Count == 0)
                return Ok(new { status = "FAIL" });

            Guid id = await _unitOfWork.fileHeadRepository.Add(new FileHead { title = fileHeadUploadDTO.title, description = fileHeadUploadDTO.description, enddate = DateTime.Now.AddDays(7) });
            _unitOfWork.Complete();

            foreach (var file in files)
            {
                if(file.Length > 0)
                {
                    using (MemoryStream streamdata = new MemoryStream())
                    {
                        await file.CopyToAsync(streamdata);
                        streamdata.Position = 0;

                        string filename = await _minIO.Add(id.ToString(), streamdata, file.FileName, file.ContentType);
                        await _unitOfWork.fileContentRepository.Add(new FileContent { originalname = file.FileName, generatedname = filename, type = file.ContentType, size = file.Length ,FileHeadId = id });
                        _unitOfWork.Complete();
                    }
                }
                
            }

            return Ok(new { status = "OK", id = id });
        }

        [HttpGet("Share/{fileid}")]
        public async Task<IActionResult> Share(string fileid)
        {
            string filename="";
            FileContent file = await _unitOfWork.fileContentRepository.GetById(Guid.Parse(fileid));
            filename = file.FileHeadId.ToString() + "/" + file.generatedname;
            string link = await _minIO.ShareLink(filename,60);
            return Ok(new { status = "OK", link = link });
        }

    }
}

