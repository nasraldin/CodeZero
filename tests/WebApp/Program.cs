#region Default
//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllers();
//var app = builder.Build();

//// Configure the HTTP request pipeline.
//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.MapControllers();
//app.Run();
#endregion

#region CodeZero
//var builder = WebApplication.CreateBuilder(args);
//builder.AddCodeZero();
//var app = builder.Build();
//app.UseCodeZero(builder.Configuration);
//app.Run();

CustomHostBuilder.CreateAsync(WebApplication.CreateBuilder(args), args);

#endregion