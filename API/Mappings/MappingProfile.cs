using AutoMapper;
using E = Domain.Entity;
using V1 = API.Models.V1;
using VO = Domain.ValueObjects;

namespace API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            _ = CreateMap<E.Order, V1.Order>().ReverseMap();
            _ = CreateMap<VO.Address, V1.Address>().ReverseMap();
            _ = CreateMap<E.OrderItem, V1.OrderItem>().ReverseMap();
        }
    }
}
