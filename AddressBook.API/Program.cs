using AddressBook.API.Data;
using AddressBook.API.Services;
using Microsoft.EntityFrameworkCore;
using AddressBook.API.Filters;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.AspNetCore;
using AddressBook.API.Validators;
using AddressBook.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ValidateModelStateFilter>();
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true)
    .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ContactDtoValidator>());

builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<AddressBookContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();