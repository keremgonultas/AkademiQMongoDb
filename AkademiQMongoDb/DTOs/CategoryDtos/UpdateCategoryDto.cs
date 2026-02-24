namespace AkademiQMongoDb.DTOs.CategoryDtos
{
    public class UpdateCategoryDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }
        public bool Status { get; set; }
    }
}
