using Microsoft.Extensions.Options;
using ReadHTML.Interfaces;

namespace ReadHTML
{
    public class ProcessBeers : IProcessBeers
    {
        private readonly IExportCSV _exportCSV;
        private readonly IProcessElings _processElings;
        private readonly IProcessUntappd _processUntappd;
        private readonly Appsettings _options;

        public ProcessBeers(IExportCSV exportCSV, IProcessElings processElings, IProcessUntappd processUntappd, IOptions<Appsettings> options)
        {
            _exportCSV = exportCSV;
            _processElings = processElings;
            _processUntappd = processUntappd;
            _options = options.Value;
        }

        public void ProcessAllBeer()
        {
            var bieren = _processElings.GetElingsBeers();
            _processUntappd.GetUntappedRatingAsync(bieren);
            _exportCSV.ConvertToCsv(bieren, _options.OutputPath);
            Console.WriteLine("All Beers Read!");

        }
    }
}
