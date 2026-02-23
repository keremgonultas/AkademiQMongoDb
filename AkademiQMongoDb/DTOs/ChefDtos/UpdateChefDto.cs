namespace AkademiQMongoDb.DTOs.ChefDTOs
{
    public class UpdateChefDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
    }
}