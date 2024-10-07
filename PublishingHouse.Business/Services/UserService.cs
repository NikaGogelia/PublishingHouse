using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PublishingHouse.Business.Services.Interfaces;
using PublishingHouse.Data.Entities;
using PublishingHouse.Repository.IRepository;
using PublishingHouse.Shared.Dtos.Auth;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PublishingHouse.Business.Services;

public class UserService : IUserService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	private readonly IConfiguration _configuration;

	public UserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_configuration = configuration;
	}

	public async Task<string> RegisterAsync(RegisterDto model)
	{
		// Validate the DTO
		var validationContext = new ValidationContext(model);
		var validationResults = new List<ValidationResult>();
		bool isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

		if (!isValid)
		{
			var errors = validationResults.Select(v => v.ErrorMessage).ToList();
			throw new ValidationException(string.Join(", ", errors));
		}

		var user = _mapper.Map<User>(model);
		user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

		_unitOfWork.User.AddUserAsync(user);
		await _unitOfWork.Save();
		return "User registered successfully.";
	}


	public async Task<AuthResponseDto> LoginAsync(LoginDto model)
	{
		// Validate the DTO
		var validationContext = new ValidationContext(model);
		var validationResults = new List<ValidationResult>();
		bool isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);
		if (!isValid)
		{
			var errors = validationResults.Select(v => v.ErrorMessage).ToList();
			throw new ValidationException(string.Join(", ", errors));
		}

		var user = await _unitOfWork.User.GetUserByUserNameAsync(model.Username);
		if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
		{
			return null;
		}

		// Generate JWT Token
		var tokenHandler = new JwtSecurityTokenHandler();
		var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]);
		var expiresInMinutes = double.Parse(_configuration["JwtSettings:ExpiresInMinutes"]);

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new[]
			{
			new Claim(ClaimTypes.Name, user.Username),
			new Claim(ClaimTypes.Role, user.Role.ToString())
		}),
			Expires = DateTime.UtcNow.AddMinutes(expiresInMinutes),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);
		var tokenString = tokenHandler.WriteToken(token);

		// Create AuthResponseDto and populate it
		var authResponse = new AuthResponseDto
		{
			Token = tokenString,
			ExpiresAt = DateTime.UtcNow.AddMinutes(expiresInMinutes)
		};

		return authResponse;
	}
}
