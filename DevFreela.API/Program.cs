using DevFreela.Application.CQRS.Commands.SkillCommand;
using DevFreela.Application.Interfaces;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;




var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();

builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(typeof(CreateSkillCommand).Assembly); });

builder.Services.AddDbContext<DevFreelaDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DevFreelaDB")));

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
