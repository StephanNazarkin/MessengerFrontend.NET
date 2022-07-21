using MessengerFrontend.Services;
using MessengerFrontend.Services.Interfaces;
using System.Net.Http.Headers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("log.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IAccountServiceAPI, AccountServiceAPI>();
builder.Services.AddTransient<IChatServiceAPI, ChatServiceAPI>();
builder.Services.AddTransient<IMessageServiceAPI, MessageServiceAPI>();
builder.Services.AddTransient<IAccountServiceAPI, AccountServiceAPI>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession();

builder.Services.AddHttpClient("Messenger", httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration["Api"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
