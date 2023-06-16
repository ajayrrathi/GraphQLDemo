using System;
using GraphQLHotChoclateServer.Shared;
using MasterProject.Infrastructure.Data;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GraphQLHotChoclateServer;
using MasterProject.SharedKernel.Interface;
using MasterProject.Infrastructure;
using MasterProject.SharedKernel.Repository;
using GraphQLHotChoclateServer.DataService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<AppDBContext>(option => option.UseInMemoryDatabase("InMemoryDatabase"), ServiceLifetime.Scoped);
builder.Services.AddPooledDbContextFactory<AppDBContext>(option => option.UseInMemoryDatabase("TestDatabase"));
builder.Services.AddScoped<EfRepository>();

builder.Services
    .AddMemoryCache()
    .AddGraphQLServer()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .AddQueryType<GraphQLQuery>()
    .AddTypeExtension<GraphQLClientQuery>()
    .AddTypeExtension<GraphQLProductQuery>()
    .AddMutationType<GraphQLMutation>()
    .AddTypeExtension<GraphQLClientMutation>()
    .AddTypeExtension<GraphQLProductMutation>()
    .AddSubscriptionType<GraphQLSubcription>()
    .AddTypeExtension<GraphQLClientSubcription>()
    .AddTypeExtension<GraphQLProductSubscription>()
    .AddMutationConventions();

builder.Services.AddInMemorySubscriptions();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
    
    var serviceScopeFactory = (IServiceScopeFactory)app.Services.GetService(typeof(IServiceScopeFactory));

    using (var scope = serviceScopeFactory.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<IDbContextFactory<AppDBContext>>();
        GraphQLHotChoclateServer.DataService.AppDBContectDataService.SeedData(dbContext.CreateDbContext());
    }
    
    
}
app.UseRouting();
app.UseWebSockets();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.UseEndpoints(endpoint => endpoint.MapGraphQL()).UseHttpLogging();

app.Run();
