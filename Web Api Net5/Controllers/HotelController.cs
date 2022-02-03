using AutoMapper;
using Common.Response;
using Core.Models;
using DataEF.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web_Api_Net5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelCOntroller : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public HotelCOntroller(IUnitOfWork uow, ILogger<CountryController> logger, IMapper mapper)
        {
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<HotelDTO>), 200)]
        public async Task<ActionResult<List<HotelDTO>>> GetHotels()
        {
            var hotels = await _uow.Hotels.GetAllAsync();
            var response = _mapper.Map<IList<HotelDTO>>(hotels);
            
            return Ok(response);
        }
        
        [HttpGet("{id:int}", Name = "GetHotel")]
        [ProducesResponseType(typeof(HotelDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {
            var hotel = await _uow.Hotels.GetAsync(h => h.Id == id);
                return hotel is not null
                    ? Ok(_mapper.Map<HotelDTO>(hotel)) 
                    : NotFound();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDTO hotelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _uow.Countries.Exists(c => c.Id == hotelDto.CountryId))
                throw new WrongForeignKeyBadRequestException("The Hotel with Id passed doesn't exist", 400002);
            
            var hotel = _mapper.Map<Hotel>(hotelDto);
            await _uow.Hotels.InsertAsync(hotel);
            await _uow.CommitAsync();

            return CreatedAtRoute("GetHotel", new { Id = hotel.Id } , _mapper.Map<HotelDTO>(hotel));
        }
    }
}
