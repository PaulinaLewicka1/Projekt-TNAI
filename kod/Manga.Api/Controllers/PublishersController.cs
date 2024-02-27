using AutoMapper;
using Manga.Dto.Publishers;
using Manga.Model.Entities;
using Manga.Repository.Publishers;
using Microsoft.AspNetCore.Mvc;

namespace Manga.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly IMapper _mapper;

        public PublishersController(IPublisherRepository publisherRepository, IMapper mapper)
        {
            _publisherRepository = publisherRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var publisher = await _publisherRepository.GetPublisherByIdAsync(id);
            if (publisher == null) { return  NotFound(); }

            var publisherDto = _mapper.Map<PublisherDto>(publisher);

            return Ok(publisherDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var publishers = await _publisherRepository.GetAllPublishersAsync();
            if(publishers == null) { return NotFound(); }

            var publishersDto = _mapper.Map<List<PublisherDto>>(publishers);

            return Ok(publishersDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PublisherInputDto publisher)
        {
            if(publisher == null) { return BadRequest(); }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newPublisher = new Publisher()
            {
                Name = publisher.Name,
                Website = publisher.Website
            };

            var result = await _publisherRepository.SavePublisherAsync(newPublisher);

            if(!result)
            {
                throw new Exception("Error saving publisher");
            }

            var publisherDto = _mapper.Map<PublisherDto>(newPublisher);

            return Ok(publisherDto);    
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PublisherInputDto publisher)
        {
            if (publisher == null) { return BadRequest(); }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPublisher = await _publisherRepository.GetPublisherByIdAsync(id);

            if(existingPublisher == null) return NotFound();

            existingPublisher.Name = publisher.Name;
            existingPublisher.Website = publisher.Website;

            var result = await _publisherRepository.SavePublisherAsync(existingPublisher);

            if(! result)
            {
                throw new Exception("Error updating publisher");
            }

            var publisherDto = _mapper.Map<PublisherDto>(existingPublisher);

            return Ok(publisherDto);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingPublisher = await _publisherRepository.GetPublisherByIdAsync(id);
            if (existingPublisher == null) return NotFound();

            var result = await _publisherRepository.DeletePublisherAsync(id);
            if (!result)
            {
                throw new Exception("Error deleting publisher");
            }
            return Ok();
        }

    }
}
