using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Business.Services.Interfaces;
using PublishingHouse.Enums;
using PublishingHouse.Models.Dtos.Product;
using PublishingHouse.Models.QueryParameterModel;
using PublishingHouse.Models.ResponseModel;

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
		_response = new Response(Status.Success, "Successful request");
	}

	/// <summary>
	/// Retrieves a list of products with optional filtering and pagination.
	/// </summary>
	/// <param name="query">The query parameters for filtering and pagination.</param>
	/// <returns>An IActionResult containing the response with the list of products or an error message.</returns>
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
			else
			{
				_response.Message = "Products retrieved successfully";
			}

			_response.Result = products;
			return Ok(_response);
		}
	}

	/// <summary>
	/// Retrieves a single product by its unique identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the product.</param>
	/// <returns>An IActionResult containing the response with the product details or an error message.</returns>
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

	/// <summary>
	/// Creates a new product with the provided details.
	/// </summary>
	/// <param name="createProductDto">The details of the product to create.</param>
	/// <returns>An IActionResult containing the response with the created product or an error message.</returns>
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

	/// <summary>
	/// Archives an existing product by its unique identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the product to archive.</param>
	/// <param name="updateProductDto">The updated details of the product for archiving.</param>
	/// <returns>An IActionResult containing the response with the updated product or an error message.</returns>
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
