using AutoMapper;
using Manga.Dto.MangaSerie;
using Manga.Dto.Publishers;
using Manga.Model.Entities;
using Manga.Repository.MangaSerie;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manga.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MangaSeriesController : ControllerBase
    {
        private readonly IMangaSeriesRepository _seriesRepository;
        private readonly IMapper _mapper;

        public MangaSeriesController(IMangaSeriesRepository seriesRepository, IMapper mapper)
        {
            _seriesRepository = seriesRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var mangaSeries = await _seriesRepository.GetMangaSeriesByIdAsync(id);
            if (mangaSeries == null) { return NotFound(); }

            var mangaSeriesDto = _mapper.Map<MangaSeriesDto>(mangaSeries);

            return Ok(mangaSeriesDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mangaSeries = await _seriesRepository.GetAllMangaSeriesAsync();
            if (mangaSeries == null) { return NotFound(); }

            var mangaSeriesDto = _mapper.Map<List<MangaSeriesDto>>(mangaSeries);

            return Ok(mangaSeriesDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MangaSeriesInputDto mangaSeries)
        {
            if (mangaSeries == null) { return BadRequest(); }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newMangaSeries = new MangaSeries()
            {
                Name = mangaSeries.Name,
                SeriesISBN = mangaSeries.SeriesISBN,
                Completed = mangaSeries.Completed,
                PublisherId = mangaSeries.PublisherId,
            };

            var result = await _seriesRepository.SaveMangaSeriesAsync(newMangaSeries);

            if (!result)
            {
                throw new Exception("Error saving manga series");
            }

            var mangaSeriesDto = _mapper.Map<MangaSeriesDto>(newMangaSeries);

            return Ok(mangaSeriesDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MangaSeriesInputDto mangaSeries)
        {
            if (mangaSeries == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingMangaSeries = await _seriesRepository.GetMangaSeriesByIdAsync(id);

            if (existingMangaSeries == null)
            {
                return NotFound();
            }

            existingMangaSeries.Name = mangaSeries.Name;
            existingMangaSeries.SeriesISBN = mangaSeries.SeriesISBN;
            existingMangaSeries.Completed = mangaSeries.Completed;

            var result = await _seriesRepository.SaveMangaSeriesAsync(existingMangaSeries);

            if (!result)
            {
                throw new Exception("Error updating manga series");
            }

            var mangaSeriesDto = _mapper.Map<MangaSeriesDto>(existingMangaSeries);
            return Ok(mangaSeriesDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingMangaSeries = await _seriesRepository.GetMangaSeriesByIdAsync(id);
            if (existingMangaSeries == null) return NotFound();

            var result = await _seriesRepository.DeleteMangaSeriesAsync(id);
            if (!result)
            {
                throw new Exception("Error deleting manga series");
            }
            return Ok();
        }

    }
}
