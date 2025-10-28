using AutoMapper;
using beSQLSugar.Application.Services;
using beSQLSugar.Application.Services.Admin;
using beSQLSugar.Application.Services.AnalyzerImageServices;
using beSQLSugar.Application.Services.Auth;
using beSQLSugar.Application.Services.CategoryServices;
using beSQLSugar.Application.Services.ContactServices;
using beSQLSugar.Application.Services.Helper;
using beSQLSugar.Application.Services.HeroSectionProductServices;
using beSQLSugar.Application.Services.HeroSectionServices;
using beSQLSugar.Application.Services.JWT;
using beSQLSugar.Application.Services.Partners;
using beSQLSugar.Application.Services.ProductColorServices;
using beSQLSugar.Application.Services.ProductMediaServices;
using beSQLSugar.Application.Services.ProductServices;
using beSQLSugar.Application.Services.ProductSpecServivces;
using beSQLSugar.Application.Services.ProductStorageServices;
using beSQLSugar.Infrastructure;
using beSQLSugar.Infrastructure.Database;
using beSQLSugar.Infrastructure.Repositories;
using beSQLSugar.Infrastructure.Repositories.AdminRepository;
using beSQLSugar.Infrastructure.Repositories.CategoryRepository;
using beSQLSugar.Infrastructure.Repositories.Contacts;
using beSQLSugar.Infrastructure.Repositories.HeroSectionProducts;
using beSQLSugar.Infrastructure.Repositories.HeroSections;
using beSQLSugar.Infrastructure.Repositories.Images;
using beSQLSugar.Infrastructure.Repositories.Partners;
using beSQLSugar.Infrastructure.Repositories.ProductColors;
using beSQLSugar.Infrastructure.Repositories.ProductMedias;
using beSQLSugar.Infrastructure.Repositories.Products;
using beSQLSugar.Infrastructure.Repositories.ProductSpecs;
using beSQLSugar.Infrastructure.Repositories.ProductStorages;
using beSQLSugar.Infrastructure.Repository.CategoryRepository;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "beSQLSugar API", Version = "v1" });

    // Cấu hình JWT Bearer
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Nhập JWT token theo format: Bearer {token}"
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
            Array.Empty<string>()
        }
    });
});


// Auto Mapper Configurations
builder.Services.AddAutoMapper(typeof(Program));


// Add SQLSugar
builder.Services.AddScoped<SqlSugarDbContext>();

// Add Repositories and Services
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IAdminUserRepository, AdminUserRepository>();
builder.Services.AddScoped<IAdminUserService, AdminUserService>();

builder.Services.AddScoped<IHeroSectionRepository, HeroSectionRepository>();
builder.Services.AddScoped<IHeroSectionService, HeroSectionService>();

builder.Services.AddScoped<IHeroSectionProductRepository, HeroSectionProductRepository>();
builder.Services.AddScoped<IHeroSectionProductService, HeroSectionProductService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IProductSpecRepository, ProductSpecRepository>();
builder.Services.AddScoped<IProductSpecService, ProductSpecService>();

builder.Services.AddScoped<IProductMediaRepository, ProductMediaRepository>();
builder.Services.AddScoped<IProductMediaService, ProductMediaService>();

builder.Services.AddScoped<IProductColorRepository, ProductColorRepository>();
builder.Services.AddScoped<IProductColorService, ProductColorService>();

builder.Services.AddScoped<IProductStorageRepository, ProductStorageRepository>();
builder.Services.AddScoped<IProductStorageService, ProductStorageService>();

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();

builder.Services.AddScoped<IPartnerRepository, PartnerRepository>();
builder.Services.AddScoped<IPartnerService, PartnerService>();

builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IAnalyzerImageSerivce, AnalyzerImageService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IImageToGridService, ImageToGridService>();
builder.Services.AddScoped<IPathFindingService, PathFindingService>();


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJWTService, JWTService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContextService, UserContextService>();

// Đăng ký mediator
builder.Services.AddMediatR(cfg =>
{
    //cfg.RegisterServicesFromAssembly(typeof(CreateAdminUserCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

// Jwt auth
var key = builder.Configuration["Jwt:Key"] ?? throw new Exception("Jwt:Key missing");
var issuer = builder.Configuration["Jwt:Issuer"];
var audience = builder.Configuration["Jwt:Audience"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});

// CORS
builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Cho phép truy cập file tĩnh (wwwroot)
app.UseStaticFiles();

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
