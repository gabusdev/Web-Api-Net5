namespace Web_Api_Net5.Models
{
    public class CreateHotelDTO
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }
    }
    public class HotelDTO: CreateHotelDTO
    {
        public int Id { get; set; }
    }
}
