using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Enums;
using PublishingHouse.Models.Dtos.Gender;
using PublishingHouse.Models.ResponseModel;
using PublishingHouse.Services.IServices;

namespace PublishingHouse.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Operator, Senior Operator")]
public class GenderController : ControllerBase
{
	private readonly IGenderService _genderService;
	private readonly Response _response;

	public GenderController(IGenderService genderService)
	{
		_genderService = genderService;
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
			var genders = await _genderService.GetAllGenders();

			_response.Status = Status.Success;

			if (genders.Count() == 0)
			{
				_response.Message = "There are no genders";
			}

			if (genders.Count() > 0)
			{
				_response.Message = "Genders retrieved successfully";
			}

			_response.Result = genders;
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
				var gender = await _genderService.GetGender(id);

				_response.Status = Status.Success;
				_response.Message = "Gender retrieved successfully";
				_response.Result = gender;

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
	public async Task<ActionResult<Response>> Post([FromBody] CreateGenderDto createGenderDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			try
			{
				var gender = await _genderService.CreateGender(createGenderDto);

				_response.Status = Status.Success;
				_response.Message = "Gender created successfully";
				_response.Result = gender;

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
}
