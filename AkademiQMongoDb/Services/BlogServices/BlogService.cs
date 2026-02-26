using AkademiQMongoDb.DTOs.BlogDtos;
using AkademiQMongoDb.Entities;
using AkademiQMongoDb.Settings;
using MongoDB.Driver;

namespace AkademiQMongoDb.Services.BlogServices
{
    public class BlogService : IBlogService
    {
        private readonly IMongoCollection<Blog> _blogCollection;

        public BlogService(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            
            _blogCollection = database.GetCollection<Blog>("Blogs");
        }

        public async Task CreateAsync(CreateBlogDto createBlogDto)
        {
            var blog = new Blog
            {
                Title = createBlogDto.Title,
                Description = createBlogDto.Description,
                ImageUrl = createBlogDto.ImageUrl,
                Author = createBlogDto.Author,
                Date = createBlogDto.Date
            };
            await _blogCollection.InsertOneAsync(blog);
        }

        public async Task DeleteAsync(string id)
        {
            await _blogCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<ResultBlogDto>> GetAllAsync()
        {
            var values = await _blogCollection.Find(x => true).ToListAsync();
            return values.Select(x => new ResultBlogDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Author = x.Author,
                Date = x.Date
            }).ToList();
        }

        public async Task<UpdateBlogDto> GetByIdAsync(string id)
        {
            var value = await _blogCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return new UpdateBlogDto
            {
                Id = value.Id,
                Title = value.Title,
                Description = value.Description,
                ImageUrl = value.ImageUrl,
                Author = value.Author,
                Date = value.Date
            };
        }

        public async Task UpdateAsync(UpdateBlogDto updateBlogDto)
        {
            var blog = new Blog
            {
                Id = updateBlogDto.Id,
                Title = updateBlogDto.Title,
                Description = updateBlogDto.Description,
                ImageUrl = updateBlogDto.ImageUrl,
                Author = updateBlogDto.Author,
                Date = updateBlogDto.Date
            };
            await _blogCollection.FindOneAndReplaceAsync(x => x.Id == updateBlogDto.Id, blog);
        }
    }
}