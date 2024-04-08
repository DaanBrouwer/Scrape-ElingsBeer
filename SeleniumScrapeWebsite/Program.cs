using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenQA.Selenium;
using ReadHTML;
using ReadHTML.Interfaces;

//TODO
// Als biernaam al in lijst staat voeg kolom toe met ook beschikbaar in
// dubbel check brouwerij untappd of deze echt goed is vergelijk met bekende brouwerij van elings
// if rating > 3.5 ofzo check checkins
// fix error edgedriver
//var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
//{
//    var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build(); ;
//    services.Configure<Appsettings>(configuration.GetSection("AppSettings"));
//    services.AddSingleton<IWebDriver, WebDriver>();
//    services.AddSingleton<IWebElement, WebElement>();
//    services.AddSingleton<IProcessElings, ProcessElings>();
//    services.AddSingleton<IProcessUntappd, ProcessUntappd>();
//    services.AddSingleton<IExportCSV, ExportCSV>();
//    services.AddSingleton<IProcessBeers, ProcessBeers>();
//}).Build();

var builder = Host.CreateApplicationBuilder();

builder.Services
    .Configure<Appsettings>(builder.Configuration.GetSection(nameof(Appsettings)))
    .AddSingleton<IWebDriver, WebDriver>()
    .AddSingleton<IWebElement, WebElement>()
    .AddSingleton<IProcessElings, ProcessElings>()
    .AddSingleton<IProcessUntappd, ProcessUntappd>()
    .AddSingleton<IExportCSV, ExportCSV>()
    .AddSingleton<IProcessBeers, ProcessBeers>();

var host = builder.Build();
var app = host.Services.GetRequiredService<IProcessBeers>();
app.ProcessAllBeer();










