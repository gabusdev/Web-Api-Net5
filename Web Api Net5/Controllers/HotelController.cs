using AutoMapper;
using BasicResponses;
using Common.DTO.Request;
using Common.Response;
using Core.Entities.Enums;
using Core.Models;
using DataEF.UnitOfWork;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace Web_Api_Net5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateHotelDTO> _hotelValidator;

        public HotelController(IUnitOfWork uow, ILogger<CountryController> logger, IMapper mapper, IValidator<CreateHotelDTO> hotelValidator)
        {
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
            _hotelValidator = hotelValidator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<HotelDTO>), 200)]
        public async Task<ActionResult<List<HotelDTO>>> GetHotels([FromQuery] RequestParams reqParams)
        {
            var hotels = await _uow.Hotels.GetAllAsync();
            var response = _mapper.Map<IList<HotelDTO>>(hotels);

            return Ok(new ApiOkResponse(response));
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
            var result = _hotelValidator.Validate(hotelDto);
            if (!result.IsValid)
                return BadRequest(new ApiBadRequestResponse(result));

            if (!await _uow.Countries.Exists(c => c.Id == hotelDto.CountryId))
                throw new WrongForeignKeyBadRequestException($"The Country with Id {hotelDto.CountryId} doesn't exist", (int)CustomCodeEnum.NoCountryId);

            var hotel = _mapper.Map<Hotel>(hotelDto);
            await _uow.Hotels.InsertAsync(hotel);
            await _uow.CommitAsync();

            return CreatedAtRoute("GetHotel", new { id = hotel.Id }, _mapper.Map<HotelDTO>(hotel));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] CreateHotelDTO hotelDto)
        {
            var result = _hotelValidator.Validate(hotelDto);
            if (!result.IsValid)
                return BadRequest(new ApiBadRequestResponse(result));

            var hotel = await _uow.Hotels.GetAsync(h => h.Id == id);
            if (hotel == null)
                return NotFound(new ApiNotFoundResponse($"There is not Hotel with id {id}"));

            _mapper.Map(hotelDto, hotel);
            _uow.Hotels.Update(hotel);
            await _uow.CommitAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _uow.Hotels.GetAsync(h => h.Id == id);
            if (hotel == null)
                return NotFound(new ApiNotFoundResponse($"There is not Hotel with id {id}"));

            _uow.Hotels.DeleteAsync(hotel.Id);
            await _uow.CommitAsync();

            return NoContent();
        }
    }
}
