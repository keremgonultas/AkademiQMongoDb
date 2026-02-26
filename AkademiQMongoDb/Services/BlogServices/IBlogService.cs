using AkademiQMongoDb.DTOs.BlogDtos;

namespace AkademiQMongoDb.Services.BlogServices
{
    public interface IBlogService
    {
        Task CreateAsync(CreateBlogDto createBlogDto);
        Task DeleteAsync(string id);
        Task<List<ResultBlogDto>> GetAllAsync();
        Task<UpdateBlogDto> GetByIdAsync(string id);
        Task UpdateAsync(UpdateBlogDto updateBlogDto);
    }
}