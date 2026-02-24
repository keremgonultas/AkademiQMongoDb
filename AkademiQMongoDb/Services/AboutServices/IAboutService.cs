using AkademiQMongoDb.DTOs.AboutDTOs; // Kendi namespace'ine göre düzelt

namespace AkademiQMongoDb.Services.AboutServices
{
    public interface IAboutService
    {
        Task<List<ResultAboutDto>> GetAllAboutAsync();
        Task CreateAboutAsync(CreateAboutDto createAboutDto);
        Task UpdateAboutAsync(UpdateAboutDto updateAboutDto);
        Task DeleteAboutAsync(string id);
        Task<ResultAboutDto> GetByIdAboutAsync(string id);
    }
}