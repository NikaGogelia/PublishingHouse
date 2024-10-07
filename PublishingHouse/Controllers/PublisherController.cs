using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Business.Services.Interfaces;
using PublishingHouse.Enums;
using PublishingHouse.Models.Dtos.Publisher;
using PublishingHouse.Models.ResponseModel;

namespace PublishingHouse.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Operator, SeniorOperator")]
public class PublisherController : ControllerBase
{
	private readonly IPublisherService _publisherService;
	private readonly Response _response;

	public PublisherController(IPublisherService publisherService)
	{
		_publisherService = publisherService;
		_response = new Response(Status.Success, "Successful request");
	}

	/// <summary>
	/// Retrieves a list of all publishers.
	/// </summary>
	/// <returns>An IActionResult containing the response with the list of publishers or an error message.</returns>
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
			else
			{
				_response.Message = "Publishers retrieved successfully";
			}

			_response.Result = publishers;
			return Ok(_response);
		}
	}

	/// <summary>
	/// Retrieves a single publisher by its unique identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the publisher.</param>
	/// <returns>An IActionResult containing the response with the publisher details or an error message.</returns>
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
				var publisher = await _publisherService.GetPublisher(id);

				_response.Status = Status.Success;
				_response.Message = "Publisher retrieved successfully";
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

	/// <summary>
	/// Creates a new publisher with the provided details.
	/// </summary>
	/// <param name="createPublisherDto">The details of the publisher to create.</param>
	/// <returns>An IActionResult containing the response with the created publisher or an error message.</returns>
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
				_response.Message = "Publisher created successfully";
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

	/// <summary>
	/// Updates an existing publisher with the provided details.
	/// </summary>
	/// <param name="id">The unique identifier of the publisher to update.</param>
	/// <param name="updatePublisherDto">The updated details of the publisher.</param>
	/// <returns>An IActionResult containing the response with the updated publisher or an error message.</returns>
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

	/// <summary>
	/// Deletes a publisher by its unique identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the publisher to delete.</param>
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
