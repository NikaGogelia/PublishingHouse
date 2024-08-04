using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Enums;
using PublishingHouse.Models.Dtos.ProductType;
using PublishingHouse.Models.ResponseModel;
using PublishingHouse.Services.IServices;

namespace PublishingHouse.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Operator, Senior Operator")]
public class ProductTypeController : ControllerBase
{
	private readonly IProductTypeService _productTypeService;
	private readonly Response _response;

	public ProductTypeController(IProductTypeService productTypeService)
	{
		_productTypeService = productTypeService;
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
			var productTypes = await _productTypeService.GetAllProductType();

			_response.Status = Status.Success;

			if (productTypes.Count() == 0)
			{
				_response.Message = "There are no product types";
			}

			if (productTypes.Count() > 0)
			{
				_response.Message = "Product types retrieved successfully";
			}

			_response.Result = productTypes;
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
