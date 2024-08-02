using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Enums;
using PublishingHouse.Models.Dtos.Country;
using PublishingHouse.Models.ResponseModel;
using PublishingHouse.Services.IServices;

namespace PublishingHouse.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController : ControllerBase
{
	private readonly ICountryService _countryService;
	private readonly Response _response;

	public CountryController(ICountryService countryService)
	{
		_countryService = countryService;
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
			var countries = await _countryService.GetAllCountries();

			_response.Status = Status.Success;

			if (countries.Count() == 0)
			{
				_response.Message = "There are no countries";
			}

			if (countries.Count() > 0)
			{
				_response.Message = "Countries retrieved successfully";
			}

			_response.Result = countries;
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
