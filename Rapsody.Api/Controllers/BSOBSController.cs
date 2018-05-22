using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rapsody.Api.Models;
using Rapsody.Api.Services;

namespace Rapsody.Api.Controllers
{
    [Route("api/v1/group/[controller]")]
    [Authorize]
    public class BSOBSController : Controller
    {
        private readonly ICsvService _csvService;
        private readonly IFileService _fileService;
        private readonly IBSOBSRepository _bSOBSRepository;

        public BSOBSController(ICsvService csvService, IFileService fileService, IBSOBSRepository bSOBSRepository)
        {
            _csvService = csvService;
            _fileService = fileService;
            _bSOBSRepository = bSOBSRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var records = await _bSOBSRepository.GetAsync();
            if (records == null)
                return NoContent();

            return Ok(records);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var record = await _bSOBSRepository.Find(x => x.Id == id);
            if (!record.Any())
                return NotFound();

            return Ok(record.First());
        }

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            var path = await _fileService.SaveFileAsync(file);

            if (path == null)
                return BadRequest();

            IList<BSOBS> records = null;
            using (TextReader fileReader = System.IO.File.OpenText(path))
            {
                records = _csvService.Get<BSOBS>(fileReader);
            }

            if (records != null)
            {
                await _bSOBSRepository.TruncateAsync(nameof(BSOBS));
                await _bSOBSRepository.CreateAsync(records);
            }
            _fileService.DeleteFile(path);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BSOBS bSOBS)
        {
            if (bSOBS == null)
                return BadRequest();

            await _bSOBSRepository.CreateAsync(bSOBS);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody]BSOBS bSOBS)
        {
            if (bSOBS == null)
                return BadRequest();

            var record = await _bSOBSRepository.Find(x => x.Id == id);
            if (!record.Any())
                return NotFound();

            bSOBS.Id = id;
            await _bSOBSRepository.UpdateAsync(bSOBS);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var record = await _bSOBSRepository.Find(x => x.Id == id);

            if (record.Any())
                await _bSOBSRepository.DeleteAsync(record.First());

            return NoContent();
        }
    }
}