using System.ComponentModel.DataAnnotations;

namespace AkademiQMongoDb.DTOs.BannerDtos
{
    public class CreateBannerDto
    {
        [Required(ErrorMessage = "Banner başlığı boş bırakılamaz.")]
        [MinLength(5, ErrorMessage = "Banner başlığı en az 5 karakter olmalıdır.")]
        [MaxLength(50, ErrorMessage = "Banner başlığı en az 50 karakter olmalıdır.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Banner açıklaması boş bırakılamaz.")]
        [MinLength(10, ErrorMessage = "Banner açıklaması en az 10 karakter olmalıdır.")]
        [MaxLength(200, ErrorMessage = "Banner açıklaması 200 karakteri geçemez.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Görsel Url boş bırakılamaz.")]
        public string ImageUrl { get; set; }
    }
}