using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PublishingHouse;
using PublishingHouse.Data;
using PublishingHouse.Identity.Data;
using PublishingHouse.Repository;
using PublishingHouse.Repository.IRepository;
using PublishingHouse.Services;
using PublishingHouse.Services.IServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configure the application database context with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure the authentication database context with SQL Server
builder.Services.AddDbContext<AuthDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Unit of Work and Repository services for dependency injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register application services for dependency injection
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IGenderService, GenderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductTypeService, ProductTypeService>();
builder.Services.AddScoped<IProductAuthorService, ProductAuthorService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();

// Configure Identity services for user authentication and authorization
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
	options.Password.RequiredLength = 6;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
})
	.AddEntityFrameworkStores<AuthDbContext>()
	.AddDefaultTokenProviders();

// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = false,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
		};

		options.Events = new JwtBearerEvents
		{
			OnChallenge = context =>
			{
				// Customize the response for unauthorized requests
				context.HandleResponse();
				context.Response.StatusCode = 401;
				context.Response.ContentType = "application/json";
				var result = JsonSerializer.Serialize(new { message = "You are not authorized to access this resource." });
				return context.Response.WriteAsync(result);
			},
			OnForbidden = context =>
			{
				// Customize the response for forbidden requests
				context.Response.StatusCode = 403;
				context.Response.ContentType = "application/json";
				var result = JsonSerializer.Serialize(new { message = "You don't have permission to access this resource." });
				return context.Response.WriteAsync(result);
			}
		};
	});

// Configure authorization policies
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("SeniorOperatorPolicy", policy => policy.RequireRole("Senior Operator"));
	options.AddPolicy("OperatorPolicy", policy => policy.RequireRole("Operator"));
	options.AddPolicy("ManagerPolicy", policy => policy.RequireRole("Manager"));
});

// Configure controllers and JSON serialization options
builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.PropertyNamingPolicy = null;
		options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
		options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	});

// Register AutoMapper for mapping between entities and DTOs
builder.Services.AddAutoMapper(typeof(MappingProfiles));

// Configure Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "PublishingHouse", Version = "v1" });

	// Add JWT Bearer authentication to Swagger
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] {}
		}
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
	// Enable Swagger in development environment
	app.UseSwagger();
	app.UseSwaggerUI();
}

// Redirect HTTP requests to HTTPS
app.UseHttpsRedirection();

// Enable authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Map controllers to routes
app.MapControllers();

// Run the application
app.Run();
