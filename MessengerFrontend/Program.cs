using MessengerFrontend.Services;
using MessengerFrontend.Services.Interfaces;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IAccountServiceAPI, AccountServiceAPI>();
builder.Services.AddTransient<IChatServiceAPI, ChatServiceAPI>();
builder.Services.AddTransient<IMessageServiceAPI, MessageServiceAPI>();
builder.Services.AddTransient<IAccountServiceAPI, AccountServiceAPI>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession();


builder.Services.AddHttpClient("Messenger", httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration["Api"]);
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InRlc3QiLCJuYW1laWQiOiI4OTY2ZjYyZS01ZDZlLTRmYWYtOWM4MS0xZTkyODA4ZGU1OTEiLCJuYmYiOjE2NTc2MjcwMzEsImV4cCI6MTY1ODIzMTgzMSwiaWF0IjoxNjU3NjI3MDMxfQ.Ze9Jc7gJqpvZRm8tjv8zSqYG36Kh6inln24JWld_9Mw");
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
