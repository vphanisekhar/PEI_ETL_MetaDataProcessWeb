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
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

//Logging added
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

//PHANI added
builder.Services.AddCors();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient(typeof(ProjectService), typeof(ProjectService));
builder.Services.AddTransient(typeof(ProductService), typeof(ProductService));
builder.Services.AddTransient(typeof(ETLBatchSrcService), typeof(ETLBatchSrcService));
builder.Services.AddTransient(typeof(ETLBatchService), typeof(ETLBatchService));
builder.Services.AddTransient(typeof(ETLBatchSrcStepService), typeof(ETLBatchSrcStepService));
builder.Services.AddTransient(typeof(ETLBatchStepCfgService), typeof(ETLBatchStepCfgService));
builder.Services.AddTransient(typeof(ETLJobsService), typeof(ETLJobsService));

builder.Services.AddTransient<IClaimsTransformation, ClaimsTransformer>();

var clientId= builder.Configuration.GetValue<string>("JWT:Audience");
var keyCloakRealm = builder.Configuration.GetValue<string>("JWT:Authority");
var publicKeyRealm = builder.Configuration.GetValue<string>("JWT:PublicKeyRS256");
var isDevelopment = builder.Configuration.GetValue<bool>("IsDevelopment");

builder.Services.ConfigureJWT(isDevelopment, publicKeyRealm, keyCloakRealm);

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


var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfile());
});

var mapper = config.CreateMapper();

builder.Services.AddSingleton(mapper);

var app = builder.Build();

//Logging into a file
var loggerFactory = app.Services.GetService<ILoggerFactory>();
loggerFactory.AddFile(builder.Configuration["Logging:LogFilePath"].ToString());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//PHANI added
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
