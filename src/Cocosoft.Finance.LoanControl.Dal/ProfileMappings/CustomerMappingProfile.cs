using AutoMapper;
using Cocosoft.Finance.LoanControl.Dal.Model.V2.Entities;
using Cocosoft.Finance.LoanControl.Domain.Customers;

namespace Cocosoft.Finance.LoanControl.Dal.ProfileMappings;

/// <summary>
/// AutoMapper profile for mapping between <see cref="CustomerDto"/> and <see cref="Customer"/>.
/// </summary>
internal class CustomerMappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerMappingProfile"/> class.
    /// </summary>
    public CustomerMappingProfile()
    {
        CreateMap<CustomerDto, Customer>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.LastName))
            .ReverseMap()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Surname));
    }
}
