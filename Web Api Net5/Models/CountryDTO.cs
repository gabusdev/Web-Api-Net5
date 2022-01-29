using System.Collections.Generic;

namespace Web_Api_Net5.Models
{
    public class CreateCountryDTO
    {   
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
    
    public class CountryDTO: CreateCountryDTO
    {
        public int Id { get; set; }
        public IList<HotelDTO> Hotels { get; set; }
    }
}
