using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using BlogAPI.Data.Repositories;
using FluentValidation;
using BlogAPI.Data;
using Serilog;
using Newtonsoft.Json;
using RiskFirst.Hateoas;
using BlogAPI.Models;
using BlogAPI.Controllers;

//Serilog setup
Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .Enrich.WithThreadId()
                    .WriteTo.Console()
                    .WriteTo.File("logs/serilogFile.txt",
                                    outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}",
                                    rollingInterval: RollingInterval.Day)
                    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient<ITagRepository, TagRepository>();

builder.Services.AddLinks(config =>
{
    config.AddPolicy<PostHateoasResponse>(policy =>
    {
        policy
            .RequireRoutedLink(nameof(PostsHateoasController.Get), nameof(PostsHateoasController.Get))
            .RequireRoutedLink(nameof(PostsHateoasController.GetById), nameof(PostsHateoasController.GetById), _ => new {id = _.Data.Id})
            .RequireRoutedLink(nameof(PostsHateoasController.Edit), nameof(PostsHateoasController.Edit), x => new {id = x.Data.Id})
            .RequireRoutedLink(nameof(PostsHateoasController.Delete), nameof(PostsHateoasController.Delete), x => new {id = x.Data.Id})
            .RequireRoutedLink(nameof(PostsHateoasController.Patch), nameof(PostsHateoasController.Patch), x => new {id = x.Data.Id});
    });
});

builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
