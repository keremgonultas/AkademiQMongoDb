using AkademiQMongoDb.DTOs.ReservationDtos;

namespace AkademiQMongoDb.Services.ReservationServices
{
    public interface IReservationService
    {
        Task CreateAsync(CreateReservationDto createReservationDto);
        Task DeleteAsync(string id);
        Task<List<ResultReservationDto>> GetAllAsync();
        Task<UpdateReservationDto> GetByIdAsync(string id);
        Task UpdateAsync(UpdateReservationDto updateReservationDto);

        // Adminin tek tıkla onaylaması veya iptal etmesi için 2 özel metot:
        Task ApproveReservationAsync(string id);
        Task CancelReservationAsync(string id);
    }
}