using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using securitydataprotection.Filters;
using securitydataprotection.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());   
});


builder.Services.AddScoped<CheckWhiteList>();


builder.Services.AddCors(configuration =>
{
    configuration.AddPolicy("deneme", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// security data protection
//var sp = builder.Services.BuildServiceProvider();
builder.Services.AddDataProtection();



builder.Services.Configure<IpList>(builder.Configuration.GetSection("IpList"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("deneme");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseMiddleware<IPSafetyMiddleWare>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
