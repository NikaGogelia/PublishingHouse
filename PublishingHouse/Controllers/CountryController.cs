using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Business.Services.Interfaces;
using PublishingHouse.Enums;
using PublishingHouse.Models.Dtos.Country;
using PublishingHouse.Models.ResponseModel;

namespace PublishingHouse.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Operator, SeniorOperator")]
public class CountryController : ControllerBase
{
	private readonly ICountryService _countryService;
	private readonly Response _response;

	public CountryController(ICountryService countryService)
	{
		_countryService = countryService;
		_response = new Response(Status.Success, "Successful request");
	}

	/// <summary>
	/// Retrieves a list of all countries.
	/// </summary>
	/// <returns>An IActionResult containing the response with the list of countries or an error message.</returns>
	[HttpGet]
	public async Task<ActionResult<Response>> Get()
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			var countries = await _countryService.GetAllCountries();

			_response.Status = Status.Success;

			if (countries.Count() == 0)
			{
				_response.Message = "There are no countries";
			}
			else
			{
				_response.Message = "Countries retrieved successfully";
			}

			_response.Result = countries;
			return Ok(_response);
		}
	}

	/// <summary>
	/// Retrieves a single country by its unique identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the country.</param>
	/// <returns>An IActionResult containing the response with the country details or an error message.</returns>
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
				var country = await _countryService.GetCountry(id);

				_response.Status = Status.Success;
				_response.Message = "Country retrieved successfully";
				_response.Result = country;

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
	/// Creates a new country with the provided details.
	/// </summary>
	/// <param name="createCountryDto">The details of the country to create.</param>
	/// <returns>An IActionResult containing the response with the created country or an error message.</returns>
	[HttpPost]
	public async Task<ActionResult<Response>> Post([FromBody] CreateCountryDto createCountryDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			try
			{
				var country = await _countryService.CreateCountry(createCountryDto);

				_response.Status = Status.Success;
				_response.Message = "Country created successfully";
				_response.Result = country;

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
	/// Updates an existing country with the provided details.
	/// </summary>
	/// <param name="id">The unique identifier of the country to update.</param>
	/// <param name="updateCountryDto">The updated details of the country.</param>
	/// <returns>An IActionResult containing the response with the updated country or an error message.</returns>
	[HttpPut("{id:int}")]
	public async Task<ActionResult<Response>> Update(int id, [FromBody] UpdateCountryDto updateCountryDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			try
			{
				var country = await _countryService.EditCountry(id, updateCountryDto);

				_response.Status = Status.Success;
				_response.Message = "Country updated successfully";
				_response.Result = country;

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
	/// Deletes an existing country by its unique identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the country to delete.</param>
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
				var result = await _countryService.RemoveCountry(id);

				_response.Status = Status.Success;
				_response.Message = "Country deleted successfully";
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
