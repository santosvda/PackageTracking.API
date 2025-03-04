using PackageTracking.Application.Receivers.Dtos;

namespace PackageTracking.Application.Receivers;

public interface IReceiverService
{
    Task<IEnumerable<ReceiverDto>> GetReceivers();
    Task<ReceiverDto?> GetReceiverById(int id);
    Task<int> Insert(CreateReceiverDto dto);

}
