using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Business.Services.Interfaces;
using PublishingHouse.Enums;
using PublishingHouse.Models.Dtos.ProductAuthor;
using PublishingHouse.Models.ResponseModel;

namespace PublishingHouse.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Operator, SeniorOperator, Manager")]
public class ProductAuthorController : ControllerBase
{
	private readonly IProductAuthorService _productAuthorService;
	private readonly Response _response;

	public ProductAuthorController(IProductAuthorService productAuthorService)
	{
		_productAuthorService = productAuthorService;
		_response = new Response(Status.Success, "Successful request");
	}

	/// <summary>
	/// Retrieves a list of all product-author associations.
	/// </summary>
	/// <returns>An IActionResult containing the response with the list of product-author associations or an error message.</returns>
	[HttpGet]
	public async Task<ActionResult<Response>> Get()
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			var productAuthors = await _productAuthorService.GetAll();

			_response.Status = Status.Success;

			if (productAuthors.Count() == 0)
			{
				_response.Message = "There are no product authors";
			}
			else
			{
				_response.Message = "Product authors retrieved successfully";
			}

			_response.Result = productAuthors;
			return Ok(_response);
		}
	}

	/// <summary>
	/// Retrieves a single product-author association by its unique identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the product-author association.</param>
	/// <returns>An IActionResult containing the response with the product-author details or an error message.</returns>
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
				var productAuthor = await _productAuthorService.Get(id);

				_response.Status = Status.Success;
				_response.Message = "Product author retrieved successfully";
				_response.Result = productAuthor;

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
	/// Creates a new product-author association with the provided details.
	/// </summary>
	/// <param name="createProductAuthorDto">The details of the product-author association to create.</param>
	/// <returns>An IActionResult containing the response with the created product-author association or an error message.</returns>
	[HttpPost]
	public async Task<ActionResult<Response>> Post([FromBody] CreateProductAuthorDto createProductAuthorDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			try
			{
				var productAuthor = await _productAuthorService.Create(createProductAuthorDto);

				_response.Status = Status.Success;
				_response.Message = "Product author created successfully";
				_response.Result = productAuthor;

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
	/// Updates an existing product-author association with the provided details.
	/// </summary>
	/// <param name="id">The unique identifier of the product-author association to update.</param>
	/// <param name="updateProductAuthorDto">The updated details of the product-author association.</param>
	/// <returns>An IActionResult containing the response with the updated product-author association or an error message.</returns>
	[HttpPut("{id:int}")]
	public async Task<ActionResult<Response>> Update(int id, [FromBody] UpdateProductAuthorDto updateProductAuthorDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			try
			{
				var productAuthor = await _productAuthorService.Edit(id, updateProductAuthorDto);

				_response.Status = Status.Success;
				_response.Message = "Product author updated successfully";
				_response.Result = productAuthor;

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
	/// Deletes an existing product-author association by its unique identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the product-author association to delete.</param>
	/// <returns>An IActionResult containing the response with a success message or an error message.</returns>
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
				var result = await _productAuthorService.Remove(id);

				_response.Status = Status.Success;
				_response.Message = "Product author deleted successfully";
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
