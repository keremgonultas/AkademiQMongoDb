using AkademiQMongoDb.DTOs.ContactDtos;
using AkademiQMongoDb.Entities;
using AkademiQMongoDb.Settings;
using MongoDB.Driver;
using System;

namespace AkademiQMongoDb.Services.ContactServices
{
    public class ContactService : IContactService
    {
        private readonly IMongoCollection<Contact> _contactCollection;

        public ContactService(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            // Veritabanında "Contacts" adında bir tablo (collection) oluşturacak
            _contactCollection = database.GetCollection<Contact>("Contacts");
        }

        public async Task CreateAsync(CreateContactDto createContactDto)
        {
            var contact = new Contact
            {
                Name = createContactDto.Name,
                Email = createContactDto.Email,
                Phone = createContactDto.Phone,
                Message = createContactDto.Message,

                // İŞTE BURASI ÖNEMLİ: Müşteri formdan tarih seçmez, biz o anki zamanı otomatik atıyoruz!
                SendDate = DateTime.Now,
                // Yeni gelen mesaj varsayılan olarak okunmamıştır
                IsRead = false
            };
            await _contactCollection.InsertOneAsync(contact);
        }

        public async Task DeleteAsync(string id)
        {
            await _contactCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<ResultContactDto>> GetAllAsync()
        {
            var values = await _contactCollection.Find(x => true).ToListAsync();
            // Mesajları en yeniden en eskiye doğru (Tarihe göre) sıralayarak getiriyoruz
            return values.OrderByDescending(x => x.SendDate).Select(x => new ResultContactDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                Message = x.Message,
                SendDate = x.SendDate,
                IsRead = x.IsRead
            }).ToList();
        }

        public async Task<ResultContactDto> GetByIdAsync(string id)
        {
            var value = await _contactCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (value == null) return null;

            // Admin mesajın detayına girdiğinde, o mesajı "Okundu" (true) olarak güncelliyoruz
            value.IsRead = true;
            await _contactCollection.FindOneAndReplaceAsync(x => x.Id == id, value);

            return new ResultContactDto
            {
                Id = value.Id,
                Name = value.Name,
                Email = value.Email,
                Phone = value.Phone,
                Message = value.Message,
                SendDate = value.SendDate,
                IsRead = value.IsRead
            };
        }
    }
}