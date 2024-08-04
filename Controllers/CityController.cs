using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Enums;
using PublishingHouse.Models.Dtos.City;
using PublishingHouse.Models.ResponseModel;
using PublishingHouse.Services.IServices;

namespace PublishingHouse.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Operator, Senior Operator")]
public class CityController : ControllerBase
{
	private readonly ICityService _cityService;
	private readonly Response _response;

	public CityController(ICityService cityService)
	{
		_cityService = cityService;
		_response = new Response(Status.Success, "Successfull request");
	}

	[HttpGet]
	public async Task<ActionResult<Response>> Get()
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			var cities = await _cityService.GetAllCities();

			_response.Status = Status.Success;

			if (cities.Count() == 0)
			{
				_response.Message = "There are no cities";
			}

			if (cities.Count() > 0)
			{
				_response.Message = "Cities retrieved successfully";
			}

			_response.Result = cities;
			return Ok(_response);
		}
	}

	[HttpGet("{id:int}")]
	public async Task<ActionResult<Response>> Get(int id)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			try
			{
				var city = await _cityService.GetCity(id);

				_response.Status = Status.Success;
				_response.Message = "City retrieved successfully";
				_response.Result = city;

				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.Status = Status.Error;
				_response.Message = ex.Message;

				return BadRequest(_response);
			}
		}
	}

	[HttpPost]
	public async Task<ActionResult<Response>> Post([FromBody] CreateCityDto createCityDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			try
			{
				var city = await _cityService.CreateCity(createCityDto);

				_response.Status = Status.Success;
				_response.Message = "City created successfully";
				_response.Result = city;

				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.Status = Status.Error;
				_response.Message = ex.Message;

				return BadRequest(_response);
			}
		}
	}

	[HttpPut("{id:int}")]
	public async Task<ActionResult<Response>> Update(int id, [FromBody] UpdateCityDto updateCityDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			try
			{
				var city = await _cityService.EditCity(id, updateCityDto);

				_response.Status = Status.Success;
				_response.Message = "City updated successfully";
				_response.Result = city;

				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.Status = Status.Error;
				_response.Message = ex.Message;

				return BadRequest(_response);
			}
		}
	}

	[HttpDelete("{id:int}")]
	public async Task<ActionResult<Response>> Delete(int id)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			try
			{
				var result = await _cityService.RemoveCity(id);

				_response.Status = Status.Success;
				_response.Message = "City deleted successfully";
				_response.Result = result;

				return Ok(_response);
			}
			catch (KeyNotFoundException ex)
			{
				_response.Status = Status.Error;
				_response.Message = ex.Message;

				return BadRequest(_response);
			}
		}
	}
}
