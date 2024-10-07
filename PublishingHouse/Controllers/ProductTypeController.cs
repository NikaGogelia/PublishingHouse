using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Business.Services.Interfaces;
using PublishingHouse.Enums;
using PublishingHouse.Models.Dtos.ProductType;
using PublishingHouse.Models.ResponseModel;

namespace PublishingHouse.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Operator, SeniorOperator")]
public class ProductTypeController : ControllerBase
{
	private readonly IProductTypeService _productTypeService;
	private readonly Response _response;

	public ProductTypeController(IProductTypeService productTypeService)
	{
		_productTypeService = productTypeService;
		_response = new Response(Status.Success, "Successful request");
	}

	/// <summary>
	/// Retrieves a list of all product types.
	/// </summary>
	/// <returns>An IActionResult containing the response with the list of product types or an error message.</returns>
	[HttpGet]
	public async Task<ActionResult<Response>> Get()
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			var productTypes = await _productTypeService.GetAllProductType();

			_response.Status = Status.Success;

			if (productTypes.Count() == 0)
			{
				_response.Message = "There are no product types";
			}
			else
			{
				_response.Message = "Product types retrieved successfully";
			}

			_response.Result = productTypes;
			return Ok(_response);
		}
	}

	/// <summary>
	/// Retrieves a single product type by its unique identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the product type.</param>
	/// <returns>An IActionResult containing the response with the product type details or an error message.</returns>
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
				var productType = await _productTypeService.GetProductType(id);

				_response.Status = Status.Success;
				_response.Message = "Product type retrieved successfully";
				_response.Result = productType;

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
	/// Creates a new product type with the provided details.
	/// </summary>
	/// <param name="createProductTypeDto">The details of the product type to create.</param>
	/// <returns>An IActionResult containing the response with the created product type or an error message.</returns>
	[HttpPost]
	public async Task<ActionResult<Response>> Post([FromBody] CreateProductTypeDto createProductTypeDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			try
			{
				var productType = await _productTypeService.CreateProductType(createProductTypeDto);

				_response.Status = Status.Success;
				_response.Message = "Product type created successfully";
				_response.Result = productType;

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
