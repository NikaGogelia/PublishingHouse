using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Business.Services.Interfaces;
using PublishingHouse.Enums;
using PublishingHouse.Models.Dtos.City;
using PublishingHouse.Models.ResponseModel;

namespace PublishingHouse.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Operator, SeniorOperator")]
public class CityController : ControllerBase
{
	private readonly ICityService _cityService;
	private readonly Response _response;

	public CityController(ICityService cityService)
	{
		_cityService = cityService;
		_response = new Response(Status.Success, "Successful request");
	}

	/// <summary>
	/// Retrieves a list of all cities.
	/// </summary>
	/// <returns>An IActionResult containing the response with the list of cities or an error message.</returns>
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
			else
			{
				_response.Message = "Cities retrieved successfully";
			}

			_response.Result = cities;
			return Ok(_response);
		}
	}

	/// <summary>
	/// Retrieves a single city by its unique identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the city.</param>
	/// <returns>An IActionResult containing the response with the city details or an error message.</returns>
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

	/// <summary>
	/// Creates a new city with the provided details.
	/// </summary>
	/// <param name="createCityDto">The details of the city to create.</param>
	/// <returns>An IActionResult containing the response with the created city or an error message.</returns>
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

	/// <summary>
	/// Updates an existing city with the provided details.
	/// </summary>
	/// <param name="id">The unique identifier of the city to update.</param>
	/// <param name="updateCityDto">The updated details of the city.</param>
	/// <returns>An IActionResult containing the response with the updated city or an error message.</returns>
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

	/// <summary>
	/// Deletes an existing city by its unique identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the city to delete.</param>
	/// <returns>An IActionResult containing the response with the result of the deletion or an error message.</returns>
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
