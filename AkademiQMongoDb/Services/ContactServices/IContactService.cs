using AkademiQMongoDb.DTOs.ContactDtos;

namespace AkademiQMongoDb.Services.ContactServices
{
    public interface IContactService
    {
        Task CreateAsync(CreateContactDto createContactDto);
        Task DeleteAsync(string id);
        Task<List<ResultContactDto>> GetAllAsync();
        Task<ResultContactDto> GetByIdAsync(string id);
        
    }
}