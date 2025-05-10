using App.Application.Extensions;
using App.Bus;
using App.Persistence.Extensions;
using App.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddContollersWithFiltersExt().AddSwaggerGenExt().AddExceptionHandlerExt().AddCacheExt();
builder.Services.AddPersistence(builder.Configuration).AddApplications(builder.Configuration).AddBusExt(builder.Configuration);

var app = builder.Build();

app.UseConfigurePipelineExt();

app.MapControllers();

app.Run();
