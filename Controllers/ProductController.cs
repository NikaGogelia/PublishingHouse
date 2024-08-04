using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Enums;
using PublishingHouse.Models.Dtos.Product;
using PublishingHouse.Models.QueryParameterModel;
using PublishingHouse.Models.ResponseModel;
using PublishingHouse.Services.IServices;

namespace PublishingHouse.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Manager")]
public class ProductController : ControllerBase
{
	private readonly IProductService _productService;
	private readonly Response _response;

	public ProductController(IProductService productService)
	{
		_productService = productService;
		_response = new Response(Status.Success, "Successfull request");
	}

	[HttpGet]
	public async Task<ActionResult<Response>> Get([FromQuery] ProductQueryParameters query)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			var products = await _productService.GetAllProducts(query);

			_response.Status = Status.Success;

			if (products.Count() == 0)
			{
				_response.Message = "There are no products";
			}

			if (products.Count() > 0)
			{
				_response.Message = "Products retrieved successfully";
			}

			_response.Result = products;
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
				var product = await _productService.GetProduct(id);

				_response.Status = Status.Success;
				_response.Message = "Product retrieved successfully";
				_response.Result = product;

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
	public async Task<ActionResult<Response>> Post([FromBody] CreateProductDto createProductDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			try
			{
				var product = await _productService.CreateProduct(createProductDto);

				_response.Status = Status.Success;
				_response.Message = "Product created successfully";
				_response.Result = product;

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

	[HttpPatch("Archive/{id:int}")]
	public async Task<ActionResult<Response>> Archive(int id, [FromBody] UpdateProductDto updateProductDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			try
			{
				var product = await _productService.ArchiveProduct(id, updateProductDto);

				_response.Status = Status.Success;
				_response.Message = "Product archived successfully";
				_response.Result = product;

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
