using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
	public class HotelController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger<CountryController> _logger;
		private readonly IMapper _mapper;

		public HotelController(IUnitOfWork unitOfWork, ILogger<CountryController> logger, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetHotels()
		{
			try
			{
				var hotels = await _unitOfWork.Hotels.GetAll();
				var results = _mapper.Map<IList<HotelDTO>>(hotels);
				return Ok(results);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Issue in the {nameof(GetHotels)}");
				return StatusCode(500, "Internal server error. Please try again later.");
			}
		}

		[HttpGet("{id:int}", Name = "GetHotel")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		//[Authorize]
		public async Task<IActionResult> GetHotel(int id)
		{
			try
			{
				var hotel = await _unitOfWork.Hotels.Get(q => q.Id == id, new List<string> { "Country" });
				var result = _mapper.Map<HotelDTO>(hotel);
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Issue in the {nameof(GetHotel)}");
				return StatusCode(500, "Internal server error. Please try again later.");
			}
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult> CreateHotel([FromBody] CreateHotelDTO hotelDTO)
		{
			if (!ModelState.IsValid)
			{
				_logger.LogError($"Invalid POST attempt in {nameof(CreateHotel)}");
				return BadRequest(ModelState);
			}

			try
			{
				var hotel = _mapper.Map<Hotel>(hotelDTO);
				await _unitOfWork.Hotels.Insert(hotel);
				await _unitOfWork.Save();

				return CreatedAtRoute("GetHotel", new { id = hotel.Id }, hotel);
			}
			catch (Exception ex)
			{

				_logger.LogError(ex, $"Issue in the {nameof(CreateHotel)}");
				return StatusCode(500, "Internal server error. Please try again later.");
			}
		}

		[HttpPut("{id:int}")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult> UpdateHotel(int id, [FromBody] UpdateHotelDTO updateHotelDTO)
		{
			if (!ModelState.IsValid || id < 1)
			{
				_logger.LogError($"Invalid PUT attempt in {nameof(UpdateHotel)}");
				return BadRequest(ModelState);
			}

			try
			{
				var hotel = await _unitOfWork.Hotels.Get(q => q.Id == id);
				if (hotel == null)
				{
					_logger.LogError($"Invalid PUT attempt in {nameof(UpdateHotel)}");
					return BadRequest("Submitted data is invalid");
				}

				_mapper.Map(updateHotelDTO, hotel);
				_unitOfWork.Hotels.Update(hotel);
				await _unitOfWork.Save();

				return NoContent();
			}
			catch (Exception ex)
			{

				_logger.LogError(ex, $"Issue in the {nameof(UpdateHotel)}");
				return StatusCode(500, "Internal server error. Please try again later.");
			}
		}

		[HttpDelete("{id:int}")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> DeleteHotel(int id)
		{
			if (id < 1)
			{
				_logger.LogError($"Invalid DELETE attempt in {nameof(DeleteHotel)}");
				return BadRequest();
			}

			try
			{
				var hotel = await _unitOfWork.Hotels.Get(q => q.Id == id);
				if (hotel == null)
				{
					_logger.LogError($"Invalid DELETE attempt in {nameof(DeleteHotel)}");
					return BadRequest("Submitted data is invalid");
				}

				await _unitOfWork.Hotels.Delete(id);
				await _unitOfWork.Save();

				return NoContent();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Issue in the {nameof(DeleteHotel)}");
				return StatusCode(500, "Internal Server Error. Please Try Again Later.");
			}
		}

	}
}
