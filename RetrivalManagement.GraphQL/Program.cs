using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RetrivalManagement.GraphQL.Bootstrap;
using RetrivalManagement.Infrastructure.Singleton;
using RxWeb.Core.AspNetCore.Extensions;
using System.Reflection.PortableExecutable;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddConfigurationOptions(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddPerformance();
builder.Services.AddSecurity(builder.Configuration);
builder.Services.AddSingleton(typeof(UserAccessConfigInfo));
builder.Services.AddSingletonService();
builder.Services.AddScopedService();
builder.Services.AddDbContextService();
builder.Services.AddRxWebLocalization();
builder.Services.AddGraphQLService();

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapControllers();
app.MapGraphQL();

app.Run();
