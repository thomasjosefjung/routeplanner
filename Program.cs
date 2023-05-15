using backend;
using graph_oo;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(builder =>
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());

app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions()); 
app.UseStaticFiles(new StaticFileOptions() {
        FileProvider =  new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
        RequestPath = new PathString("/node_modules")
});

app.MapControllers();

app.Run();
