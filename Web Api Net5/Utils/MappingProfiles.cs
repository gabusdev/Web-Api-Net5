using AutoMapper;
using Core.Models;
using Web_Api_Net5.Models;

namespace Web_Api_Net5.Utils
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Country, CountryDTO>();
            CreateMap<Hotel, HotelDTO>();
            CreateMap<RegisterDTO, User>();
        }
    }
}
