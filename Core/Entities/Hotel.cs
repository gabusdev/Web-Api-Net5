namespace Core.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }

        public virtual Country Country { get; set; }
    }
}
