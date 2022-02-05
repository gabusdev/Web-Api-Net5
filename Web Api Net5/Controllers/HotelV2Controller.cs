using AutoMapper;
using BasicResponses;
using Common.DTO.Request;
using Common.Response;
using DataEF.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace Web_Api_Net5.Controllers
{
    [ApiVersion("2.0", Deprecated = true)] // because of this, with Deprecated as example
    [Route("api/{v:apiversion}/hotel")] // For using Versioning in Routes
    [Route("api/hotel")] // This uses api-version header after cunfigured otherwise api-version query paramether
    [ApiController]
    public class HotelV2Controller : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public HotelV2Controller(IUnitOfWork uow, ILogger<CountryController> logger, IMapper mapper)
        {
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ResponseCache(Duration = 60)]
        [ProducesResponseType(typeof(List<HotelDTO>), 200)]
        public async Task<ActionResult<List<HotelDTO>>> GetHotels([FromQuery] RequestParams reqParams)
        {
            var hotels = await _uow.Hotels.GetAllQuery(null, null, false)
                .ToPagedListAsync(reqParams.PageNumber, reqParams.PageSize);
            var response = _mapper.Map<IList<HotelDTO>>(hotels);

            return Ok(new ApiOkResponse(response));
        }
    }
}
