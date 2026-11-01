using SignalR.Hub.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("ChatClient", policy =>
    {
        policy.WithOrigins("http://localhost:5000", "https://localhost:5001")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddSignalR();

var app = builder.Build();

app.UseRouting();
app.UseCors("ChatClient");
app.MapHub<ChatHub>("/chatHub");

app.Run();