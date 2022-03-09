using BeardAndBoobsFinances;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
//using Microsoft.Extensions.Configuration.UserSecrets;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
.ReadFrom.Configuration(configuration)
.CreateLogger();

builder.Host.UseSerilog();

//YNAB_Configuration.Token = configuration.GetValue<string>("YNAB_INFO:API_TOKEN");
//YNAB_Configuration.BudgetId = configuration.GetValue<string>("YNAB_INFO:Budget_ID");

try
{
    Log.Information("Application Starting Up");
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
        //app.AddUserSecrets();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseSerilogRequestLogging();

    app.UseRouting();

    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "The application failed to start correctly... Fucking A-a-ron or Phil!");
}
finally 
{ 
    Log.CloseAndFlush();
}


