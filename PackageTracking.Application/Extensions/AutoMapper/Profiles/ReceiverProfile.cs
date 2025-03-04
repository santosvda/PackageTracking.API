using AutoMapper;
using PackageTracking.Application.Receivers.Commands.CreateReceiver;
using PackageTracking.Application.Receivers.Commands.UpdateReceiver;
using PackageTracking.Application.Receivers.Dtos;
using PackageTracking.Domain.Entities;

namespace PackageTracking.Application.Extensions.AutoMapper.Profiles;
public class ReceiverProfile : Profile
{
    public ReceiverProfile()
    {
        CreateMap<Receiver, ReceiverDto>()
            .ForMember(dest => dest.Packages, opt => opt.MapFrom(src => src.Packages));

        CreateMap<CreateReceiverCommand, Receiver>();
        CreateMap<UpdateReceiverCommand, Receiver>();
    }
}
