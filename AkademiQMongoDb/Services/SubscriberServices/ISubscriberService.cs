using AkademiQMongoDb.DTOs.SubscriberDtos;

namespace AkademiQMongoDb.Services.SubscriberServices
{
    public interface ISubscriberService
    {
        Task CreateAsync(CreateSubscriberDto createSubscriberDto);
        Task DeleteAsync(string id);
        Task<List<ResultSubscriberDto>> GetAllAsync();
    }
}