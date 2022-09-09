

using AutoMapper;

using src.Entities;
using src.Entities.ViewModel;

namespace src.Mappers;

public class ModelToEntityMapping : Profile
{
    public ModelToEntityMapping()
    {
        CreateMap<NewsViewModel, News>();
    }
}
