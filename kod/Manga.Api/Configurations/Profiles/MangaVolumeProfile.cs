using AutoMapper;
using Manga.Dto.MangaVolumes;
using Manga.Model.Entities;

namespace Manga.Api.Configurations.Profiles
{
    public class MangaVolumeProfile : Profile 
    {
        public MangaVolumeProfile() 
        {
            CreateMap<MangaVolume, MangaVolumeDto>();
        }
    }
}
