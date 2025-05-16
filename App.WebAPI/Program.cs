using App.Application.Extensions;
using App.Bus;
using App.Caching;
using App.Persistence.Extensions;
using App.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddContollersWithFiltersExt().AddSwaggerGenExt().AddExceptionHandlerExt();
builder.Services.AddPersistence(builder.Configuration).AddApplications(builder.Configuration).AddCacheExt(builder.Configuration).AddBusExt(builder.Configuration);

var app = builder.Build();

app.UseConfigurePipelineExt();

app.MapControllers();

app.Run();
