using AutoMapper;
using Common.Response;
using DataEF.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            try
            {
                var hotels = await _uow.Hotels.GetAllAsync();
                var response = _mapper.Map<IList<HotelDTO>>(hotels);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong on {nameof(GetHotels)}.");
                return StatusCode(500, "Internal Servel Error, please try again later");
            }
            
            
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(HotelDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {
            try
            {
                var hotel = await _uow.Hotels.GetAsync(id);
                return hotel is not null
                    ? Ok(_mapper.Map<HotelDTO>(hotel)) 
                    : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong on {nameof(GetHotel)}.");
                return StatusCode(500, "Internal Servel Error, please try again later");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDTO hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(hotel);
        }
    }
}
