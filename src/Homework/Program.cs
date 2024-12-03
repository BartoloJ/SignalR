var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<ChatHub>("/chathub");
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

app.Run();

