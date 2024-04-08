using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using ReadHTML;
using ReadHTML.Interfaces;

//TODO
// Als biernaam al in lijst staat voeg kolom toe met ook beschikbaar in
// dubbel check brouwerij untappd of deze echt goed is vergelijk met bekende brouwerij van elings
// if rating > 3.5 ofzo check checkins
// fix error edgedriver
// ILogger implementation
var builder = Host.CreateApplicationBuilder();

builder.Services
    .Configure<Appsettings>(builder.Configuration.GetSection(nameof(Appsettings)))
    .AddSingleton<IWebDriver, EdgeDriver>()
    .AddSingleton<IProcessElings, ProcessElings>()
    .AddSingleton<IProcessUntappd, ProcessUntappd>()
    .AddSingleton<IExportCSV, ExportCSV>()
    .AddSingleton<IProcessBeers, ProcessBeers>();

var host = builder.Build();
var app = host.Services.GetRequiredService<IProcessBeers>();
app.ProcessAllBeer();










