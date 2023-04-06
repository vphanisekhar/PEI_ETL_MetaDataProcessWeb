using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PEI_ETL.Core.Interfaces;
using PEI_ETL.Infrastrucure.UnitOfWork;
using PEI_ETL.Infrastrucure;
using PEI_ETL.Services.Service;
using PEI_MetaData_API.Authentication;
using AutoMapper;
using PEI_ETL.Services.AutoMapperProfile;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient(typeof(ProjectService), typeof(ProjectService));
builder.Services.AddTransient(typeof(ProductService), typeof(ProductService));

// Add services to the container.
builder.Services.AddDbContext<ETLDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// For Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ETLDbContext>()
    .AddDefaultTokenProviders();



builder.Services.AddSwaggerGen(c =>
{
c.SwaggerDoc("v1", new OpenApiInfo { Title = "PEIAPI", Version = "v1" }

);

//First we define the security scheme
c.AddSecurityDefinition("Bearer", //Name the security scheme
    new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
        Scheme = JwtBearerDefaults.AuthenticationScheme //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
    });

c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = JwtBearerDefaults.AuthenticationScheme, //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });

    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    // Tweak to the Schema generator
    c.SchemaGeneratorOptions = new Swashbuckle.AspNetCore.SwaggerGen.SchemaGeneratorOptions
    {
        SchemaIdSelector = type =>
   type.FullName
    };

});


//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//});
    
//    .AddJwtBearer(o =>
//{
//    o.Authority = builder.Configuration["Jwt:Authority"];
//    o.Audience = builder.Configuration["Jwt:Audience"];
//    o.RequireHttpsMetadata = false;
//    o.Events = new JwtBearerEvents()
//    {
//        OnAuthenticationFailed = c =>
//        {
//            c.NoResult();

//            c.Response.StatusCode = 500;
//            c.Response.ContentType = "text/plain";
//            //if (Environment.IsDevelopment())
//            //{
//            //    return c.Response.WriteAsync(c.Exception.ToString());
//            //}
//            return c.Response.WriteAsync("An error occured processing your authentication.");
//        }
//    };
//});

builder.Services.ConfigureJWT(false,
    "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA5QoHuDWepQvbNB4MU7kWjnEiICgAE05NfmQ4m8VKzgpBiXNbBFm9ZNT6gZ6khstlNLvN5DMtLDPG3GkfYSv6CHe9Td0L8sr9SGU2k6T48q6Wdea7t0CLbAv5jgNhT5cmCU2D0+ntIAUt6MSinzF41HiUzvmFatLbJsLZ9CSalI7Q4lMnz4+T70oa4EVoHR5IIyfOrEoEmx7mBMZ5V0+oBsuGweO0cjQYlN7oPP4aioAGZcZoWmqCeyWkqILRpStf5nTIPtRqxtxwioPE4h+dIcoqU1KT9hs8ga8ahaVhB0gSBeCuaYZEaPRQYWp8jrgz/nfV9giyTRmueZIFfFW8XwIDAQAB");


var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfile());
});

var mapper = config.CreateMapper();

builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();



app.MapControllers();

app.Run();
