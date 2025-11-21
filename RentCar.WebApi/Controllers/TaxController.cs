using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Contracts.Services;
using RentCar.Application.Dtos.Requests;
using RentCar.Application.Dtos.Responses;
using System.Net;

namespace RentCar.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TaxController(ITaxService taxService) : ControllerBase
    {
        private readonly ITaxService _taxService = taxService;

        [HttpGet("taxes")]
        [ActionName(nameof(GetTaxesAsync))]
        public async Task<IActionResult> GetTaxesAsync()
        {
            var result = await _taxService.GetTaxesAsync();
            
            return Ok(result.Data);
        }

        [Authorize]
        [HttpGet("taxes/{taxId}")]
        public async Task<IActionResult> GetTaxByIdAsync(int taxId)
        {
            var result = await _taxService.GetTaxByIdAsync(taxId);

            if (result.Status == ResponseStatus.NotFound)
            {
                return NotFound(new { message = result.Message });
            }

            return Ok(result.Data!.First());
        }

        [Authorize]
        [HttpPost("taxes")]
        public async Task<IActionResult> CreateTaxAsync([FromBody] TaxRequestDto taxDto)
        {
            var result = await _taxService.CreateTaxAsync(taxDto);

            if (result.Status == ResponseStatus.ValidationError)
            {
                return BadRequest(new { message = result.Message });
            }

            var response = result.Data!.First();

            return CreatedAtAction(
                nameof(GetTaxByIdAsync),
                new { taxId = response.TaxId },
                response);
        }

        [Authorize]
        [HttpPut("taxes/{taxId}")]
        public async Task<IActionResult> UpdateTaxAsync(int taxId, [FromBody] TaxRequestDto taxDto)
        {
            var result = await _taxService.UpdateTaxAsync(taxId, taxDto);

            if (result.Status == ResponseStatus.NotFound)
            {
                return NotFound(new { message = result.Message });
            }

            if (result.Status == ResponseStatus.ValidationError)
            {
                return BadRequest(new { message = result.Message });
            }

            return NoContent();
        }

        [Authorize]
        [HttpDelete("taxes/{taxId}")]
        public async Task<IActionResult> DeleteTaxAsync(int taxId)
        {
            var result = await _taxService.DeleteTaxAsync(taxId);

            if (result.Status == ResponseStatus.NotFound)
            {
                return NotFound(new { message = result.Message });
            }

            if (result.Status == ResponseStatus.NotAllowed)
            {
                return StatusCode(
                    (int)HttpStatusCode.MethodNotAllowed,
                    new { message = result.Message });
            }

            if (result.Status == ResponseStatus.Conflict)
            {
                return Conflict(new { message = result.Message });
            }

            return NoContent();
        }
    }
}
