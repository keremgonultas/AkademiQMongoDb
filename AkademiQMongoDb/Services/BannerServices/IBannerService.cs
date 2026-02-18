using AkademiQMongoDb.DTOs.BannerDtos;

namespace AkademiQMongoDb.Services.BannerServices
{
    public interface IBannerService
    {
        Task<List<ResultBannerDto>> GetAllAsync();

        Task<UpdateBannerDto> GetByIdBannerAsync(string id);

        Task CreateBannerAsync(CreateBannerDto createBannerDto);
        Task UpdateBannerAsync(UpdateBannerDto updateBannerDto);
        Task DeleteBannerAsync(string id);
    }
}