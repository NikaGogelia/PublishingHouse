using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Enums;
using PublishingHouse.Models;
using PublishingHouse.Models.Dtos.Author;
using PublishingHouse.Models.ResponseModel;
using PublishingHouse.Services.IServices;

namespace PublishingHouse.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
	private readonly IAuthorService _authorService;
	private readonly Response _response;

	public AuthorController(IAuthorService authorService)
	{
		_authorService = authorService;
		_response = new Response(Status.Success, "Successfull request");
	}

	[HttpGet]
	public async Task<ActionResult<Response>> Get([FromQuery] AuthorQueryParameters query)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			var authors = await _authorService.GetAllAuthors(query);

			_response.Status = Status.Success;

			if (authors.Count() == 0)
			{
				_response.Message = "There are no authors";
			}

			if (authors.Count() > 0)
			{
				_response.Message = "Authors retrieved successfully";
			}

			_response.Result = authors;
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
				var author = await _authorService.GetAuthor(id);

				_response.Status = Status.Success;
				_response.Message = "Author retrieved successfully";
				_response.Result = author;

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
	public async Task<ActionResult<Response>> Post([FromBody] CreateAuthorDto createAuthorDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			try
			{
				var author = await _authorService.CreateAuthor(createAuthorDto);

				_response.Status = Status.Success;
				_response.Message = "Author created successfully";
				_response.Result = author;

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
	public async Task<ActionResult<Response>> Update(int id, [FromBody] UpdateAuthorDto updateAuthorDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			try
			{
				var author = await _authorService.EditAuthor(id, updateAuthorDto);

				_response.Status = Status.Success;
				_response.Message = "Author updated successfully";
				_response.Result = author;

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
				var result = await _authorService.RemoveAuthor(id);

				_response.Status = Status.Success;
				_response.Message = "Author deleted successfully";
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
