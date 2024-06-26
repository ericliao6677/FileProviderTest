using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
//var path = Directory.GetCurrentDirectory();
//var path2 = builder.Environment.ContentRootPath;
//var path3 = Path.GetTempFileName();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles")));

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(@"D:\FileTest"));

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
