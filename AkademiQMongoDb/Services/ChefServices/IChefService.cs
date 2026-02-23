using AkademiQMongoDb.DTOs.ChefDTOs; // Kendi namespace'ine göre düzeltmeyi unutma

namespace AkademiQMongoDb.Services.ChefServices
{
    public interface IChefService
    {
        Task<List<ResultChefDto>> GetAllChefAsync();
        Task CreateChefAsync(CreateChefDto createChefDto);
        Task UpdateChefAsync(UpdateChefDto updateChefDto);
        Task DeleteChefAsync(string id);
        Task<ResultChefDto> GetByIdChefAsync(string id);
    }
}