using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using src.Infra;
using src.Mappers;
using src.Persistence;
using src.Services;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddDbContext<DatabaseContext>(o => o.UseInMemoryDatabase("dbContracts"));
    
builder
    .Services
    .AddScoped<DatabaseContext, DatabaseContext>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region [AutoMapper]
    builder.Services.AddAutoMapper(typeof(EntityToModelMapping), typeof(ModelToEntityMapping));
#endregion

#region [Database]
    builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
    builder.Services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
#endregion

#region [DI]
    builder.Services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoRepository<>));
    builder.Services.AddSingleton<NewsServices>();
#endregion

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
