using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublishingHouse.Business.Services.Interfaces;
using PublishingHouse.Enums;
using PublishingHouse.Models.ResponseModel;
using PublishingHouse.Shared.Dtos.Auth;
using System.Security.Claims;

namespace PublishingHouse.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	private readonly IUserService _userService;
	private readonly Response _response;

	public UserController(IUserService userService)
	{
		_userService = userService;
		_response = new Response(Status.Success, "Successful request");
	}

	/// <summary>
	/// Registers a new user with the provided details.
	/// </summary>
	/// <param name="userRegisterDto">The details of the user to register.</param>
	/// <returns>An IActionResult containing the response with a success message or an error message.</returns>
	[HttpPost("register")]
	public async Task<ActionResult<Response>> Register([FromBody] RegisterDto userRegisterDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			try
			{
				var result = await _userService.RegisterAsync(userRegisterDto);

				_response.Status = Status.Success;
				_response.Message = result;

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
	/// Logs in an existing user and returns a token.
	/// </summary>
	/// <param name="userLoginDto">The login details of the user.</param>
	/// <returns>An IActionResult containing the response with the token or an error message.</returns>
	[HttpPost("login")]
	public async Task<ActionResult<Response>> Login([FromBody] LoginDto userLoginDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		else
		{
			try
			{
				var authResponse = await _userService.LoginAsync(userLoginDto);

				if (authResponse.Token == null || authResponse.ExpiresAt == null)
				{
					_response.Status = Status.Error;
					_response.Message = "Invalid username or password.";
					return BadRequest(_response);
				}

				_response.Status = Status.Success;
				_response.Message = "Login successful";
				_response.Result = authResponse;

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
