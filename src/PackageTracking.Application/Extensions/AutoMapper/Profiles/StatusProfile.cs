using AutoMapper;
using PackageTracking.Application.Statuses.Dtos;
using PackageTracking.Domain.Entities;

namespace PackageTracking.Application.Extensions.AutoMapper.Profiles;
public class StatusProfile : Profile
{
    public StatusProfile()
    {
        CreateMap<Status, StatusDto>();
    }
}
