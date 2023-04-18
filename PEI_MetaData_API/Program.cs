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

builder.Services.AddTransient<IClaimsTransformation, ClaimsTransformer>();

builder.Services.ConfigureJWT(false,
 "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArfrqpk1IzAv7BLvXr3ox9zFrrsM0jfKe4RzbjpgG2xvFaRtkY+F0THPNiLIASorGWy+ErrA9kLkLvMmWiV1Um2uTSEe3hMZ6W/OK25B+P+AwrXgPNAXt3R0QJNKk2zlNIN9ZrD0tV8E6h3oDi31pvXPi2NuPzW6C5B5r6sTDzHd5dkTLO+dnkJG3O92M1QuvstJfDERy+03IKRRFtdgEQTit+nJ7Dy8sv5TFZpUnzQ85SNQCLqWedApvont67r+oAJtgd4CXh12nQD0Zm33CPojJfdo2fqborqhuhezg1Gq4VKALhbtmCyz/u7SkRN/upY6bzHp/q+i+Jx3cQaTCyQIDAQAB");

// Add services to the container.
builder.Services.AddDbContext<ETLDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// For Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ETLDbContext>()
    .AddDefaultTokenProviders();

//PHANI - added
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("MyAllowedOrigins",
//        policy =>
//        {
//            policy.WithOrigins("https://localhost:8081") // note the port is included 
//                .AllowAnyHeader()
//                .AllowAnyMethod();
//        });
//});


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



var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfile());
});

var mapper = config.CreateMapper();

builder.Services.AddSingleton(mapper);



var app = builder.Build();

//app.UseCors("MyAllowedOrigins");

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
