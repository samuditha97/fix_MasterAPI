using System.Text;
using FixMaster;
using FixMaster.Interfaces;
using FixMaster.Models;
using FixMaster.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

// Configure CORS to allow any origin.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", builder =>
    {
        builder
            .AllowAnyOrigin()  // Allow requests from any origin (CORS wildcard)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Configure MongoDB settings and register MongoDB client and database.
builder.Services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});
builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBookingInterface, BookingService>();
builder.Services.AddScoped<ICustomerInterface, CustomerService>();
builder.Services.AddScoped<IServiceInterface, ServicesService>();
builder.Services.AddScoped<ITechnicianInterface, ITechnicianService>();


var app = builder.Build();

var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
ConventionRegistry.Register("CustomConventions", conventionPack, type => true);

BsonClassMap.RegisterClassMap<BookingClass>(cm =>
{
    cm.AutoMap();
    cm.SetIdMember(cm.GetMemberMap(c => c.id));
    cm.MapIdMember(c => c.id).SetIdGenerator(new StringObjectIdGenerator());
});
BsonClassMap.RegisterClassMap<CustomerClass>(cm =>
{
    cm.AutoMap();
    cm.SetIdMember(cm.GetMemberMap(c => c.id));
    cm.MapIdMember(c => c.id).SetIdGenerator(new StringObjectIdGenerator());
});

BsonClassMap.RegisterClassMap<ServiceClass>(cm =>
{
    cm.AutoMap();
    cm.SetIdMember(cm.GetMemberMap(c => c.id));
    cm.MapIdMember(c => c.id).SetIdGenerator(new StringObjectIdGenerator());
});

BsonClassMap.RegisterClassMap<TechnicianClass>(cm =>
{
    cm.AutoMap();
    cm.SetIdMember(cm.GetMemberMap(c => c.id));
    cm.MapIdMember(c => c.id).SetIdGenerator(new StringObjectIdGenerator());
});
// Use CORS middleware to allow any origin.
app.UseCors("AllowAnyOrigin");
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

