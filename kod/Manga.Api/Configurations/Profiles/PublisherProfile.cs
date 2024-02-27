using AutoMapper;
using Manga.Dto.Publishers;
using Manga.Model.Entities;

namespace Manga.Api.Configurations.Profiles
{
    public class PublisherProfile : Profile
    {
        public PublisherProfile() 
        {
            CreateMap<Publisher, PublisherDto>().ForMember(x => x.MangaCount, d => d.MapFrom(s => s.MangaSeries == null ? 0 : s.MangaSeries.Count));
        }
    }
}
