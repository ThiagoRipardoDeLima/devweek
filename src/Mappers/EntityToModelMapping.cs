
using AutoMapper;

using src.Entities;
using src.Entities.ViewModel;

namespace src.Mappers;
public class EntityToModelMapping : Profile
{
    public EntityToModelMapping()
    {
        CreateMap<News, NewsViewModel>();
    }
}
