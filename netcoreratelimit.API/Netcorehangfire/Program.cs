using Hangfire;
using Hangfire.AspNetCore;
using Netcorehangfire.Filters;
using Netcorehangfire.Services;

var builder = WebApplication.CreateBuilder(args);




builder.WebHost.ConfigureLogging(logging =>
{
    logging.ClearProviders();
});




builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("hangfireconnection"));
});

builder.Services.AddHangfireServer();


// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(CustomHandleExceptionFilterAttribute));
});


builder.Services.AddScoped<IEmailSender, EmailSender>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseStatusCodePages(async context =>
    {
        context.HttpContext.Response.ContentType = "text/plain";
        await context.HttpContext.Response.WriteAsync($"Error! => Status code: {context.HttpContext.Response.StatusCode}");
    });
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
    
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseHangfireDashboard("/hangfire");

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
