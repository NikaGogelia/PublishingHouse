using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Enums;
using PublishingHouse.Models.Dtos.Publisher;
using PublishingHouse.Models.ResponseModel;
using PublishingHouse.Services.IServices;

namespace PublishingHouse.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Operator, Senior Operator")]
public class PublisherController : ControllerBase
{
	private readonly IPublisherService _publisherService;
	private readonly Response _response;

	public PublisherController(IPublisherService publisherService)
	{
		_publisherService = publisherService;
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
			var publishers = await _publisherService.GetAllPublishers();

			_response.Status = Status.Success;

			if (publishers.Count() == 0)
			{
				_response.Message = "There are no publishers";
			}

			if (publishers.Count() > 0)
			{
				_response.Message = "Publishers retrieved successfully";
			}

			_response.Result = publishers;
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
				var Publisher = await _publisherService.GetPublisher(id);

				_response.Status = Status.Success;
				_response.Message = "publisher retrieved successfully";
				_response.Result = Publisher;

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
	public async Task<ActionResult<Response>> Post([FromBody] CreatePublisherDto createPublisherDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			try
			{
				var publisher = await _publisherService.CreatePublisher(createPublisherDto);

				_response.Status = Status.Success;
				_response.Message = "publisher created successfully";
				_response.Result = publisher;

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
	public async Task<ActionResult<Response>> Update(int id, [FromBody] UpdatePublisherDto updatePublisherDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			try
			{
				var publisher = await _publisherService.EditPublisher(id, updatePublisherDto);

				_response.Status = Status.Success;
				_response.Message = "Publisher updated successfully";
				_response.Result = publisher;

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
				var result = await _publisherService.RemovePublisher(id);

				_response.Status = Status.Success;
				_response.Message = "Publisher deleted successfully";
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
