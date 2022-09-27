using AutoMapper;
using CatalogService.Application.Features.Commands.CreateEvent;
using CatalogService.Application.Features.Commands.UpdateEvent;
using CatalogService.Domain;

namespace CatalogService.Application.Profiles
{
    public  class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<UpdateCatalogCommandRequest, CatalogItem>().ReverseMap();
            CreateMap<CreateCatalogCommandRequest, CatalogItem>().ReverseMap();
        }
    }
}
