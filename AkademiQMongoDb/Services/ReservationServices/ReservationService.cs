using AkademiQMongoDb.DTOs.ReservationDtos;
using AkademiQMongoDb.Entities;
using AkademiQMongoDb.Settings;
using MongoDB.Driver;

namespace AkademiQMongoDb.Services.ReservationServices
{
    public class ReservationService : IReservationService
    {
        private readonly IMongoCollection<Reservation> _reservationCollection;

        public ReservationService(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _reservationCollection = database.GetCollection<Reservation>("Reservations");
        }

        public async Task CreateAsync(CreateReservationDto createReservationDto)
        {
            var reservation = new Reservation
            {
                Name = createReservationDto.Name,
                Email = createReservationDto.Email,
                Phone = createReservationDto.Phone,
                ReservationDate = createReservationDto.ReservationDate,
                ReservationTime = createReservationDto.ReservationTime,
                PersonCount = createReservationDto.PersonCount,
                SpecialRequest = createReservationDto.SpecialRequest,
                // Yeni gelen rezervasyon otomatik olarak bu durumu alır
                Status = "Onay Bekliyor"
            };
            await _reservationCollection.InsertOneAsync(reservation);
        }

        public async Task DeleteAsync(string id)
        {
            await _reservationCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<ResultReservationDto>> GetAllAsync()
        {
            var values = await _reservationCollection.Find(x => true).ToListAsync();
            // En yeni rezervasyonlar en üstte görünsün diye Id'ye göre tersten sıralıyoruz
            return values.OrderByDescending(x => x.Id).Select(x => new ResultReservationDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                ReservationDate = x.ReservationDate,
                ReservationTime = x.ReservationTime,
                PersonCount = x.PersonCount,
                SpecialRequest = x.SpecialRequest,
                Status = x.Status
            }).ToList();
        }

        public async Task<UpdateReservationDto> GetByIdAsync(string id)
        {
            var value = await _reservationCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (value == null) return null;

            return new UpdateReservationDto
            {
                Id = value.Id,
                Name = value.Name,
                Email = value.Email,
                Phone = value.Phone,
                ReservationDate = value.ReservationDate,
                ReservationTime = value.ReservationTime,
                PersonCount = value.PersonCount,
                SpecialRequest = value.SpecialRequest,
                Status = value.Status
            };
        }

        public async Task UpdateAsync(UpdateReservationDto updateReservationDto)
        {
            var reservation = new Reservation
            {
                Id = updateReservationDto.Id,
                Name = updateReservationDto.Name,
                Email = updateReservationDto.Email,
                Phone = updateReservationDto.Phone,
                ReservationDate = updateReservationDto.ReservationDate,
                ReservationTime = updateReservationDto.ReservationTime,
                PersonCount = updateReservationDto.PersonCount,
                SpecialRequest = updateReservationDto.SpecialRequest,
                Status = updateReservationDto.Status
            };
            await _reservationCollection.FindOneAndReplaceAsync(x => x.Id == updateReservationDto.Id, reservation);
        }

        // Hızlı Onaylama Metodu
        public async Task ApproveReservationAsync(string id)
        {
            var value = await _reservationCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (value != null)
            {
                value.Status = "Onaylandı";
                await _reservationCollection.FindOneAndReplaceAsync(x => x.Id == id, value);
            }
        }

        // Hızlı İptal Etme Metodu
        public async Task CancelReservationAsync(string id)
        {
            var value = await _reservationCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (value != null)
            {
                value.Status = "İptal Edildi";
                await _reservationCollection.FindOneAndReplaceAsync(x => x.Id == id, value);
            }
        }
    }
}