using Microsoft.EntityFrameworkCore;
using AuthService.DbContexts;
using AuthService.IFactory;
using AuthService.TenantFactory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//builder.Services.AddDbContext<ChatApplicationDbContext>(options =>
//{
//    options.UseSqlServer(
//        builder.Configuration.GetConnectionString("DefaultConnection"));
//});

builder.Services.AddDbContextFactory<ChatApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("UserServiceDb"));
});

builder.Services.AddScoped<IDbChatApplicationContextFactory, ChatApplicationDbContextFactory>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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