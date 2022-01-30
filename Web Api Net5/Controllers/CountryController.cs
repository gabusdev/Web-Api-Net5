using AutoMapper;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web_Api_Net5.Models;
using Web_Api_Net5.Repository;

namespace Web_Api_Net5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public CountryController(IUnitOfWork uow, ILogger<CountryController> logger, IMapper mapper)
        {
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetCountries()
        {
            try
            {
                var countries = await _uow.Countries.GetAllAsync(c => c.Hotels);
                var response = _mapper.Map<IList<CountryDTO>>(countries);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong on {nameof(GetCountries)}.");
                return StatusCode(500, "Internal Servel Error, please try again later");
            }
            
            
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<Country>>> GetCountry(int id)
        {
            try
            {
                var country = await _uow.Countries.FindOnlyAsync(c => c.Id == id, c => c.Hotels);
                return country is not null
                    ? Ok(_mapper.Map<CountryDTO>(country)) 
                    : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong on {nameof(GetCountry)}.");
                return StatusCode(500, "Internal Servel Error, please try again later");
            }


        }
        [HttpGet("/cu")]
        public async Task<ActionResult<List<Country>>> Test()
        {
            try
            {
                var countries = await _uow.Countries.FindAllAsync(c => c.Name == "Cuba" && c.Id == 2);
                return countries is not null
                    ? Ok(_mapper.Map<IList<CountryDTO>>(countries))
                    : NotFound();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
