using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rapsody.Api.DB;
using Rapsody.Api.Models;
using Rapsody.Api.Services;

namespace Rapsody.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class MagnitudeController : Controller
    {
        private readonly ICsvService _csvService;
        private readonly IFileService _fileService;
        private readonly IMagnitudeRepository _repository;

        public MagnitudeController(ICsvService csvService, IFileService fileService,
            IMagnitudeRepository repository)
        {
            _csvService = csvService;
            _fileService = fileService;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var records = await _repository.GetAsync();
            if (records == null)
                return NoContent();

            return Ok(records);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var record = await _repository.GetAsync(id);
            if (record == null)
                return NotFound();

            return Ok(record);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Magnitude magnitude)
        {
            if (magnitude == null)
                return BadRequest();

            await _repository.SaveAsync(magnitude);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            var path = await _fileService.SaveFileAsync(file);

            if (path == null)
                return BadRequest();

            IList<Magnitude> records = null;
            using (TextReader fileReader = System.IO.File.OpenText(path))
            {
                records = _csvService.Get<Magnitude>(fileReader);
            }

            if (records != null)
            {
                await _repository.TruncateAsync(nameof(Magnitude));
                await _repository.SaveAsync(records);
            }
            _fileService.DeleteFile(path);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody]Magnitude magnitude)
        {
            if (magnitude == null)
                return BadRequest();

            var record = await _repository.GetAsync(id);
            if (record == null)
                return NotFound();

            magnitude.Id = record.Id;
            await _repository.UpdateAsync(magnitude);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var record = await _repository.GetAsync(id);

            if (record != null)
                await _repository.DeleteAsync(record);

            return NoContent();
        }
    }
}
