using Productos.Api.Extensions;
using Productos.Common.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.  

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBusinessServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfiles>());
builder.Services.AddSwaggerDocumentation();
builder.Services.AddJWTBearerConfiguration(builder.Configuration);
builder.Services.AddCors();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration["AllowedHosts"]!);
    });
});

var app = builder.Build();
  
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerGen();
app.UseHttpsRedirection();

app.UseAuthentication();  
app.UseAuthorization();

app.MapControllers();
app.UseCors("CorsPolicy");
app.Run();
