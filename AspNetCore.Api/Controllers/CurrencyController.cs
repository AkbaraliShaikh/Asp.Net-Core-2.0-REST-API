using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AspNetCore.Api.Models;
using AspNetCore.Api.Services;

namespace AspNetCore.Api.Controllers
{
    [Route("api/v1/group/[controller]")]
    [Authorize]
    public class CurrencyController : Controller
    {
        private readonly ICsvService _csvService;
        private readonly IFileService _fileService;
        private readonly ICurrencyCodeRepository _currencyCodeRepository;

        public CurrencyController(ICsvService csvService, IFileService fileService, ICurrencyCodeRepository currencyCodeRepository)
        {
            _csvService = csvService;
            _fileService = fileService;
            _currencyCodeRepository = currencyCodeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var records = await _currencyCodeRepository.GetAsync();
            if (!records.Any())
                return NoContent();

            return Ok(records);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var record = await _currencyCodeRepository.Find(x => x.Id == id);
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

            IList<CurrencyCode> records = null;
            using (TextReader fileReader = System.IO.File.OpenText(path))
            {
                records = _csvService.Get<CurrencyCode>(fileReader);
            }

            if (records != null)
            {
                await _currencyCodeRepository.TruncateAsync(nameof(CurrencyCode));
                await _currencyCodeRepository.CreateAsync(records);
            }
            _fileService.DeleteFile(path);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CurrencyCode currencyCode)
        {
            if (currencyCode == null)
                return BadRequest();

            await _currencyCodeRepository.CreateAsync(currencyCode);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody]CurrencyCode currencyCode)
        {
            if (currencyCode == null)
                return BadRequest();

            var record = await _currencyCodeRepository.Find(x => x.Id == id);
            if (!record.Any())
                return NotFound();

            currencyCode.Id = id;
            await _currencyCodeRepository.UpdateAsync(currencyCode);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var record = await _currencyCodeRepository.Find(x => x.Id == id);

            if (record.Any())
                await _currencyCodeRepository.DeleteAsync(record.First());

            return NoContent();
        }
    }
}