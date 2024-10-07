using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Business.Services.Interfaces;
using PublishingHouse.Enums;
using PublishingHouse.Models.Dtos.Author;
using PublishingHouse.Models.QueryParameterModel;
using PublishingHouse.Models.ResponseModel;

namespace PublishingHouse.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Operator, SeniorOperator")]
public class AuthorController : ControllerBase
{
	private readonly IAuthorService _authorService;
	private readonly Response _response;

	public AuthorController(IAuthorService authorService)
	{
		_authorService = authorService;
		_response = new Response(Status.Success, "Successful request");
	}

	/// <summary>
	/// Retrieves a list of authors based on query parameters.
	/// </summary>
	/// <param name="query">The query parameters for filtering and pagination.</param>
	/// <returns>An IActionResult containing the response with the list of authors or an error message.</returns>
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
			else
			{
				_response.Message = "Authors retrieved successfully";
			}

			_response.Result = authors;
			return Ok(_response);
		}
	}

	/// <summary>
	/// Retrieves a single author by their unique identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the author.</param>
	/// <returns>An IActionResult containing the response with the author details or an error message.</returns>
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

	/// <summary>
	/// Creates a new author with the provided details.
	/// </summary>
	/// <param name="createAuthorDto">The details of the author to create.</param>
	/// <returns>An IActionResult containing the response with the created author or an error message.</returns>
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

	/// <summary>
	/// Updates an existing author with the provided details.
	/// </summary>
	/// <param name="id">The unique identifier of the author to update.</param>
	/// <param name="updateAuthorDto">The updated details of the author.</param>
	/// <returns>An IActionResult containing the response with the updated author or an error message.</returns>
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

	/// <summary>
	/// Deletes an existing author by their unique identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the author to delete.</param>
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
