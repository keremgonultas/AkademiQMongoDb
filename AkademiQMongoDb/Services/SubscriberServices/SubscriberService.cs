using AkademiQMongoDb.DTOs.SubscriberDtos;
using AkademiQMongoDb.Entities;
using AkademiQMongoDb.Settings;
using MongoDB.Driver;

namespace AkademiQMongoDb.Services.SubscriberServices
{
    public class SubscriberService : ISubscriberService
    {
        private readonly IMongoCollection<Subscriber> _subscriberCollection;

        public SubscriberService(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _subscriberCollection = database.GetCollection<Subscriber>("Subscribers");
        }

        public async Task CreateAsync(CreateSubscriberDto createSubscriberDto)
        {
            var subscriber = new Subscriber { Email = createSubscriberDto.Email };
            await _subscriberCollection.InsertOneAsync(subscriber);
        }

        public async Task DeleteAsync(string id)
        {
            await _subscriberCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<ResultSubscriberDto>> GetAllAsync()
        {
            var values = await _subscriberCollection.Find(x => true).ToListAsync();
            return values.Select(x => new ResultSubscriberDto
            {
                Id = x.Id,
                Email = x.Email
            }).ToList();
        }
    }
}