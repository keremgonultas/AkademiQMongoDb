using AkademiQMongoDb.DTOs.GalleryDtos;

namespace AkademiQMongoDb.Services.GalleryServices
{
    public interface IGalleryService
    {
        Task CreateAsync(CreateGalleryDto createGalleryDto);
        Task UpdateAsync(UpdateGalleryDto updateGalleryDto);
        Task DeleteAsync(string id);
        Task<List<ResultGalleryDto>> GetAllAsync();
        Task<UpdateGalleryDto> GetByIdAsync(string id);
    }
}