using Hangfire;
using HangfireBasicAuthenticationFilter;
using System.Reflection;
using ERPPlugin;
using ERPPlugin.Dto;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<PluginManager>();

builder.Services.AddHangfire(opt =>
{
    opt.UseSqlServerStorage(builder.Configuration.GetConnectionString("DbConnectionString"))
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[]
    {
        new HangfireCustomBasicAuthenticationFilter
        {
            User = app.Configuration.GetSection("HangfireOptions:User").Value,
            Pass = app.Configuration.GetSection("HangfireOptions:Pass").Value
        }
    }
});
app.UseAuthorization();

app.MapControllers();


app.Run();
