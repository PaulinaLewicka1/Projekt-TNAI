using AutoMapper;
using Manga.Dto.MangaVolumes;
using Manga.Dto.Publishers;
using Manga.Model.Entities;
using Manga.Repository.MangaVolumes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manga.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MangaVolumesController : ControllerBase
    {
        private readonly IMangaVolumeRepository _volumesRepository;
        private readonly IMapper _mapper;

        public MangaVolumesController(IMangaVolumeRepository volumesRepository, IMapper mapper)
        {
            _volumesRepository = volumesRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var mangaVolume = await _volumesRepository.GetMangaVolumeByIdAsync(id);
            if (mangaVolume == null) { return NotFound(); }

            var mangaVolumeDto = _mapper.Map<MangaVolumeDto>(mangaVolume);

            return Ok(mangaVolumeDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mangaVolumes = await _volumesRepository.GetAllMangaVolumesAsync();
            if (mangaVolumes == null) { return NotFound(); }

            var mangaVolumesDto = _mapper.Map<List<MangaVolumeDto>>(mangaVolumes);

            return Ok(mangaVolumesDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MangaVolumeInputDto mangaVolume)
        {
            if (mangaVolume == null) { return BadRequest(); }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newMangaVolume = new MangaVolume()
            {
                ISBN = mangaVolume.ISBN,
                Name = mangaVolume.Name,
                Description = mangaVolume.Description,
                Authors = mangaVolume.Authors,  
                VolumeNumber = mangaVolume.VolumeNumber,
                Year = mangaVolume.Year,
                MangaSeriesId = mangaVolume.MangaSeriesId
            };

            var result = await _volumesRepository.SaveMangaVolumeAsync(newMangaVolume);

            if (!result)
            {
                throw new Exception("Error saving volume");
            }

            var mangaVolumeDto = _mapper.Map<MangaVolumeDto>(newMangaVolume);

            return Ok(mangaVolumeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MangaVolumeInputDto mangaVolume)
        {
            if (mangaVolume == null) { return BadRequest(); }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingVolume = await _volumesRepository.GetMangaVolumeByIdAsync(id);

            if (existingVolume == null) return NotFound();

            existingVolume.ISBN = mangaVolume.ISBN;
            existingVolume.Name = mangaVolume.Name;
            existingVolume.Description = mangaVolume.Description;
            existingVolume.Authors  = mangaVolume.Authors;
            existingVolume.VolumeNumber = mangaVolume.VolumeNumber;
            existingVolume.Year = mangaVolume.Year;

            var result = await _volumesRepository.SaveMangaVolumeAsync(existingVolume);

            if (!result)
            {
                throw new Exception("Error updating volume");
            }

            var volumeDto = _mapper.Map<MangaVolumeDto>(existingVolume);

            return Ok(volumeDto);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingVolume = await _volumesRepository.GetMangaVolumeByIdAsync(id);
            if (existingVolume == null) return NotFound();

            var result = await _volumesRepository.DeleteMangaVolumeAsync(id);
            if (!result)
            {
                throw new Exception("Error deleting volume");
            }
            return Ok();
        }
    }
}
