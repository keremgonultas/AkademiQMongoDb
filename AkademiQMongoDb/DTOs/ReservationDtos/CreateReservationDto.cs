namespace AkademiQMongoDb.DTOs.ReservationDtos
{
    public class CreateReservationDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ReservationDate { get; set; }
        public string ReservationTime { get; set; }
        public string PersonCount { get; set; }
        public string SpecialRequest { get; set; }
    }
}