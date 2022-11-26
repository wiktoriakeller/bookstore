using Bookstore.Core.Dtos.Publishers;
using Bookstore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Api.Controllers
{
    [ApiController]
    [Route("api/v1/publishers")]
    public class PublishersController : ControllerBase
    {
        private readonly IPublishersService _publishersService;

        public PublishersController(IPublishersService publishersService)
        {
            _publishersService = publishersService;
        }

        [HttpGet]
        public IActionResult GetAllPublishers()
        {
            return Ok(_publishersService.GetAllPublishers());
        }

        [HttpGet("filters")]
        public IActionResult GetFilteredPublishers([FromQuery] string? publishersNameFilter)
        {
            var publishersFiltersDto = new PublishersFiltersDto { NameFilter = publishersNameFilter };
            return Ok(_publishersService.FilterPublishers(publishersFiltersDto));
        }

        [HttpPost]
        public async Task<IActionResult> AddPublisher([FromBody] AddPublisherDto addPublisherDto)
        {
            var publisherId = await _publishersService.AddPublisher(addPublisherDto);
            return Created($"api/vi/publishers/{publisherId}", null);
        }

        [HttpDelete("{publisherId}")]
        public async Task<IActionResult> DeletePublisher([FromRoute] Guid publisherId)
        {
            var deletePublisherDto = new DeletePublisherDto { Id = publisherId };
            await _publishersService.DeletePublisher(deletePublisherDto);
            return Ok();
        }

        [HttpPut("{publisherId}")]
        public async Task<IActionResult> UpdatePublisher([FromBody] UpdatePublisherDto updatePublisherDto)
        {
            await _publishersService.UpdatePublisher(updatePublisherDto);
            return Ok();
        }
    }
}
