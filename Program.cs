using Task_Mangement.Models;
using Task_Mangement.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUser, UserRepo>();
builder.Services.AddScoped<ITask, TaskRepo>();
builder.Services.AddCors(org =>
{
    org.AddPolicy("Allow_All", py =>
    {
        py.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Allow_All");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
