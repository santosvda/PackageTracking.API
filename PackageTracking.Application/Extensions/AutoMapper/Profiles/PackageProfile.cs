using AutoMapper;
using PackageTracking.Application.Packages.Dtos;
using PackageTracking.Application.Packagess.Commands.CreatePackage;
using PackageTracking.Application.Packagess.Commands.UpdatePackage;
using PackageTracking.Domain.Entities;

namespace PackageTracking.Application.Extensions.AutoMapper.Profiles;
public class PackageProfile : Profile
{
    public PackageProfile()
    {
        CreateMap<Package, PackageDto>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Adress == null ? null : src.Adress.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Adress == null ? null : src.Adress.Street))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Adress == null ? null : src.Adress.PostalCode))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Adress == null ? null : src.Adress.Country))
            .ForMember(dest => dest.Statuses, opt => opt.MapFrom(src => src.Statuses));


        CreateMap<CreatePackageCommand, Package>()
            .ForMember(dest => dest.Adress, opt => opt.MapFrom(src => new Adress
            {
                City = src.City,
                Street = src.Street,
                PostalCode = src.PostalCode,
                Country = src.Country
            }));

        CreateMap<UpdatePackageCommand, Package>()
            .ForMember(dest => dest.Adress, opt => opt.MapFrom(src => new Adress
            {
                City = src.City,
                Street = src.Street,
                PostalCode = src.PostalCode,
                Country = src.Country
            }));
    }
}
