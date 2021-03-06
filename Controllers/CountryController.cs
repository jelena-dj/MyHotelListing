using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyHotelListing.Data;
using MyHotelListing.IRepository;
using MyHotelListing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHotelListing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CountryController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger<CountryController> _logger;
		private readonly IMapper _mapper;

		public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetCountries([FromQuery] RequestParams requestParams)
		{
			//try
			//{
				var countries = await _unitOfWork.Contries.GetPagedList(requestParams);
				var results = _mapper.Map<IList<CountryDTO>>(countries);
				return Ok(results);
			//}
			//catch (Exception ex)
			//{
			//	_logger.LogError(ex, $"Issue in the {nameof(GetCountries)}");
			//	return StatusCode(500, "Internal server error. Please try again later.");
			//}
		}

		[HttpGet("{id:int}", Name = "GetCountry")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetCountry(int id)
		{
			//try
			//{
				var country = await _unitOfWork.Contries.Get(q => q.Id == id, new List<string> {"Hotels"});
				var result = _mapper.Map<CountryDTO>(country);
				return Ok(result);
			//}
			//catch (Exception ex)
			//{
			//	_logger.LogError(ex, $"Issue in the {nameof(GetCountry)}");
			//	return StatusCode(500, "Internal server error. Please try again later.");
			//}
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDTO countryDTO)
		{
			if (!ModelState.IsValid)
			{
				_logger.LogError($"Invalid POST attempt in {nameof(CreateCountry)}");
				return BadRequest(ModelState);
			}
			try
			{
				var country = _mapper.Map<Country>(countryDTO);
				await _unitOfWork.Contries.Insert(country);
				await _unitOfWork.Save();

				return CreatedAtRoute("GetCountry", new { id = country.Id }, country);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Issue in the {nameof(CreateCountry)}");
				return StatusCode(500, "Internal server error. Please try again later.");
			}
		}

		[HttpPut("{id:int}")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDTO updateCountryDTO)
		{
			if (!ModelState.IsValid || id < 1)
			{
				_logger.LogError($"Invalid PUT attempt in {nameof(UpdateCountry)}");
				return BadRequest(ModelState);
			}

			try
			{
				var country = await _unitOfWork.Contries.Get(q => q.Id == id);
				if (country == null)
				{
					_logger.LogError($"Invalid PUT attempt in {nameof(UpdateCountry)}");
					return BadRequest("Submitted data is invalid");
				}

				_mapper.Map(updateCountryDTO, country);
				_unitOfWork.Contries.Update(country);
				await _unitOfWork.Save();

				return NoContent();
			}
			catch (Exception ex)
			{

				_logger.LogError(ex, $"Issue in the {nameof(UpdateCountry)}");
				return StatusCode(500, "Internal server error. Please try again later.");
			}
		}

		
		[HttpDelete("{id:int}")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> DeleteCountry(int id)
		{
			if (id < 1)
			{
				_logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCountry)}");
				return BadRequest();
			}

			try
			{
				var country = await _unitOfWork.Contries.Get(q => q.Id == id);
				if (country == null)
				{
					_logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCountry)}");
					return BadRequest("Submitted data is invalid");
				}

				await _unitOfWork.Contries.Delete(id);
				await _unitOfWork.Save();

				return NoContent();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Issue in the {nameof(DeleteCountry)}");
				return StatusCode(500, "Internal Server Error. Please Try Again Later.");
			}
		}

	}
}
