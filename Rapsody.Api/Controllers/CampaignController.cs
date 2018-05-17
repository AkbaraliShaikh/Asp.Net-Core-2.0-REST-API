using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rapsody.Api.Models;
using Rapsody.Api.Services;

namespace Rapsody.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    public class CampaignController : Controller
    {
        private readonly ICampaignRepository _campaignRepository;

        public CampaignController(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var records = await _campaignRepository.GetAsync();
            if (records == null)
                return NoContent();

            return Ok(records);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var record = await _campaignRepository.GetAsync(id);
            if (record == null)
                return NotFound();

            return Ok(record);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Campaign campaign)
        {
            if (campaign == null)
                return BadRequest();

            campaign.LastModifiedDate = DateTime.Now;
            await _campaignRepository.CreateAsync(campaign);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody]Campaign campaign)
        {
            if (campaign == null)
                return BadRequest();

            var record = await _campaignRepository.GetAsync(id);
            if (record == null)
                return NotFound();

            campaign.Id = id;
            await _campaignRepository.UpdateAsync(campaign);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var record = await _campaignRepository.GetAsync(id);

            if (record != null)
                await _campaignRepository.DeleteAsync(record);

            return NoContent();
        }
    }
}