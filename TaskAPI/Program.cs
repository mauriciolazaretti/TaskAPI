using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskAPI.DataAccess;
using TaskAPI.DataAccess.Entity;
using TaskAPI.Services.Implementations.Services;
using TaskAPI.Services.Implementations.UnitOfWork;
using TaskAPI.Services.Interfaces.Services;
using TaskAPI.Services.Interfaces.UnitOfWork;
using TaskAPI.Services.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TaskApiContext>(
        options => options.UseNpgsql("Host=postgres;Port=5432;Database=postgres;Username=postgres;Password=postgres;",
        x => x.MigrationsAssembly("TaskAPI.DataAccess"))
        );
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskUoW, TaskUoW>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IProjectUoW, ProjectUoW>();
builder.Services.AddScoped<IValidator<TaskEntity>, TaskValidator>();
builder.Services.AddScoped<IValidator<ProjectEntity>, ProjectValidator>();

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
