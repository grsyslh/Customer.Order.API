using AutoMapper;
using Order.Contract.Request.Command.CustomerOrders;
using Order.Contract.Response.Command.CustomerOrders;
using Order.Contract.Response.Query.CustomerOrders;
using Order.Domain.Entity;

namespace Order.Contract.Mappings
{
    public class CustomerOrderMappingProfile : Profile
    {
        public CustomerOrderMappingProfile()
        {
            CreateMap<GetCustomerOrderByIdQueryResponse, CustomerOrder>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId)).ReverseMap();

            CreateMap<GetCustomerOrdersByCustomerIdQueryResponse, CustomerOrder>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId)).ReverseMap();

            CreateMap<CreateCustomerOrderCommandResponse, CustomerOrder>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId)).ReverseMap();

            CreateMap<UpdateCustomerOrderCommandResponse, CustomerOrder>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId)).ReverseMap();

        }
    }
}
