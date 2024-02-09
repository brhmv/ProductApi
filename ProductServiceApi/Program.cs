using Microsoft.EntityFrameworkCore;
using ProductServiceApi.Data;
using ProductServiceApi.Helpers;
using ProductServiceApi.Services;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

///////////////////
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});
///////////////////

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ProductConStr")));
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITagService, TagService>();


/////////////////////////////////////
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
//////////////////////////////////////


builder.Services.AddLogging(builder => { builder.AddConsole(); });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();