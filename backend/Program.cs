using backend;
using graph_oo;

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
app.UseStaticFiles(new StaticFileOptions
{
    // RequestPath = "wwwroot"
}); 

app.MapControllers();


// var g = ReadAutobahndaten.Read("graph_bab.xml"); 

// var started = DateTime.Now; 

// var wilnsdorf = g.FindNode("WILNSDORF (A 45)"); 
// var herrenberg = g.FindNode("HERRENBERG (A 81)"); 
// var rottenburg = g.FindNode("KREUZ ROTTENBURG"); 

// // Console.WriteLine("Dist: "+wilnsdorf.GetDistanceTo(herrenberg)); 
// // Console.WriteLine("Dist: "+wilnsdorf.GetDistanceTo(rottenburg)); 

// var p = Dijkstra.GetShortestPath(
//     g, 
//     wilnsdorf, 
//     herrenberg); 

// Console.WriteLine( "Time Dijkstra: "+(DateTime.Now - started).TotalMilliseconds); 

// foreach(var stop in p)
// {
//     Console.WriteLine(stop.Name); 
// }

// started = DateTime.Now; 
// var p2 = AStar.FindShortestPath(g, wilnsdorf, herrenberg); 
// _ = p2 ?? throw new NullReferenceException(); 

// Console.WriteLine("Time A*: " + (DateTime.Now - started).TotalMilliseconds); 

// foreach(var stop in p2)
// {
//     Console.WriteLine(stop.Name); 
// }

app.Run();
