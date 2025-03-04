using AutoMapper;
using Microsoft.Extensions.Logging;
using PackageTracking.Application.Receivers.Dtos;
using PackageTracking.Domain.Entities;
using PackageTracking.Domain.Repositories;

namespace PackageTracking.Application.Receivers;
public class ReceiverService(IReceiverRepository receiverRepository
    , ILogger<ReceiverService> logger
    , IMapper mapper
    ) : IReceiverService
{
    public async Task<ReceiverDto?> GetReceiverById(int id)
    {
        logger.LogInformation($"Getting receiver by Id: {id}");
        var receiver = await receiverRepository.GetById(id);
        var receiverDto = mapper.Map<ReceiverDto>(receiver);

        return receiverDto;
    }

    public async Task<IEnumerable<ReceiverDto>> GetReceivers()
    {
        logger.LogInformation("Getting all receivers");
        var receivers = await receiverRepository.GetAllAsync();
        var receiversDto = mapper.Map<IEnumerable<ReceiverDto>>(receivers);

        return receiversDto;
    }

    public async Task<int> Insert(CreateReceiverDto dto)
    {
        logger.LogInformation("Inserting a new receiver");
        var receiver = mapper.Map<Receiver>(dto);

        var id = await receiverRepository.Insert(receiver);

        return id;
    }
}
