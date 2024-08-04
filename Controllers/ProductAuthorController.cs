using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Enums;
using PublishingHouse.Models.Dtos.ProductAuthor;
using PublishingHouse.Models.ResponseModel;
using PublishingHouse.Services.IServices;

namespace PublishingHouse.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Operator, Senior Operator")]
public class ProductAuthorController : ControllerBase
{
	private readonly IProductAuthorService _productAuthorService;
	private readonly Response _response;

	public ProductAuthorController(IProductAuthorService productAuthorService)
	{
		_productAuthorService = productAuthorService;
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
			var productAuthors = await _productAuthorService.GetAll();

			_response.Status = Status.Success;

			if (productAuthors.Count() == 0)
			{
				_response.Message = "There are no product authors";
			}

			if (productAuthors.Count() > 0)
			{
				_response.Message = "Product authors retrieved successfully";
			}

			_response.Result = productAuthors;
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
				var productAuthor = await _productAuthorService.Get(id);

				_response.Status = Status.Success;
				_response.Message = "Product authors retrieved successfully";
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
				_response.Message = "Product authors created successfully";
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
				_response.Message = "Product authors updated successfully";
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
				_response.Message = "Product authors deleted successfully";
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
