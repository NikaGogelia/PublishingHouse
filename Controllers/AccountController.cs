using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PublishingHouse.Identity.Models;
using PublishingHouse.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PublishingHouse.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
	private readonly UserManager<IdentityUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly IConfiguration _configuration;

	public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_configuration = configuration;
	}

	[HttpPost("Register")]
	public async Task<IActionResult> Register([FromBody] Register model)
	{
		var user = new IdentityUser { UserName = model.UserName, };
		var register = await _userManager.CreateAsync(user, model.Password);

		if (register.Succeeded)
		{

			return Ok(new { message = "User registered Successfully" });
		}
		return BadRequest(register.Errors);
	}

	[HttpPost("Login")]
	public async Task<IActionResult> Login([FromBody] Login model)
	{
		var user = await _userManager.FindByNameAsync(model.UserName);
		if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
		{
			var userRoles = await _userManager.GetRolesAsync(user);

			var authClaims = new List<Claim>
		{
			new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
		};

			authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

			double expiryMinutes;
			if (!double.TryParse(_configuration["Jwt:ExpiryMinutes"], out expiryMinutes))
			{
				expiryMinutes = 60;
			}

			var token = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"],
				expires: DateTime.Now.AddMinutes(expiryMinutes),
				claims: authClaims,
				signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
				SecurityAlgorithms.HmacSha256
			));

			return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
		}

		return Unauthorized();
	}

	[HttpPost("AddRole")]
	public async Task<IActionResult> AddRole([FromBody] RoleModel model)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		if (!await _roleManager.RoleExistsAsync(model.Role))
		{
			var result = await _roleManager.CreateAsync(new IdentityRole(model.Role));

			if (result.Succeeded)
			{
				return Ok(new { message = "Role added successfully" });
			}

			return BadRequest(result.Errors);
		}

		return BadRequest("Role already exists");

	}

	[HttpPost("AssignRole")]
	public async Task<IActionResult> AssignRole([FromBody] UserRole model)
	{
		var user = await _userManager.FindByNameAsync(model.UserName);

		if (user == null)
		{
			return BadRequest("User not found");
		}

		var result = await _userManager.AddToRoleAsync(user, model.Role);

		if (result.Succeeded)
		{
			return Ok(new { message = "Role assigned successfully" });
		}

		return BadRequest(result.Errors);
	}
}
