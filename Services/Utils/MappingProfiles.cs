using AutoMapper;
using Common.Request;
using Common.Response;
using Core.Models;

namespace Services.Utils
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
