using FluentValidation;
using LibrarySystem.Application.Books.Commands;
using LibrarySystem.Application.Validators;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Infrastructure.DomainEvents;
using LibrarySystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

// MediatR 
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateBookCommand).Assembly);
});

// Validators
builder.Services.AddValidatorsFromAssemblyContaining<CreateBookCommandValidator>();

// Controllers and API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.SetIsOriginAllowed(_ => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngular");
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();