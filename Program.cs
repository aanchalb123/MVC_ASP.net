using ControllerDemo.Controllers;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddTransient<HomeController>();
builder.Services.AddControllers();  //Add all the controllers classes as service

var app = builder.Build();
app.UseStaticFiles();

//use Controller for endpoints
app.UseRouting();
app.MapControllers();

app.Run();
