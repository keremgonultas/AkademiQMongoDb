namespace AkademiQMongoDb.DTOs.TestimonialDTOs
{
    public class UpdateTestimonialDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public string ImageUrl { get; set; }
        public string FoodImageUrl { get; set; }
        public bool Status { get; set; }
    }
}