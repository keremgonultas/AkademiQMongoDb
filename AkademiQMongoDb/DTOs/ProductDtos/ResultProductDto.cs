namespace AkademiQMongoDb.DTOs.ProductDtos
{
    public class ResultProductDto
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public string Imageurl { get; set; }

        public decimal Price { get; set; }

        public string CategoryName { get; set; }
    }
}
