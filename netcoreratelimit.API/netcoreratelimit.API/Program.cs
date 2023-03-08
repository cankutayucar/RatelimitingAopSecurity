using AspNetCoreRateLimit;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);





// ratelimit için service ler
// needed to load configuration from appsettings.json
builder.Services.AddOptions();

// needed to store rate limit counters and ip rules
builder.Services.AddMemoryCache();

//load general configuration from appsettings.json
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));

//load ip rules from appsettings.json
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));

// inject counter and rules stores
builder.Services.AddInMemoryRateLimiting();
//builder.Services.AddDistributedRateLimiting<AsyncKeyLockProcessingStrategy>();
//builder.Services.AddDistributedRateLimiting<RedisProcessingStrategy>();
//services.AddRedisRateLimiting();




builder.Services.AddControllers();

// configuration (resolvers, counter key builders)
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var ipPolicyStore = app.Services.GetRequiredService<IIpPolicyStore>(); 
ipPolicyStore.SeedAsync().GetAwaiter().GetResult(); 
//var clientPolicyStore = app.Services.GetRequiredService<IClientPolicyStore>(); 
//clientPolicyStore.SeedAsync().GetAwaiter().GetResult();
app.UseIpRateLimiting(); // ip adresi üzerinden bir kısıtlama ekleyecek

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
