using Manga.Dto.MangaSerie;
using Manga.Model.Entities;
using AutoMapper;


namespace Manga.Api.Configurations.Profiles
{
    public class MangaSeriesProfile : Profile
    {
        public MangaSeriesProfile()
        {
            CreateMap<MangaSeries, MangaSeriesDto>().ForMember(x => x.VolumeCount, d => d.MapFrom(s => s.Volumes == null ? 0 : s.Volumes.Count));
        }
    }
}
