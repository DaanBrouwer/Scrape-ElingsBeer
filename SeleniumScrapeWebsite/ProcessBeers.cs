using Microsoft.Extensions.Logging;
using ReadHTML.Interfaces;

namespace ReadHTML
{
    public class ProcessBeers(IExportCSV exportCSV, IProcessElings processElings, IProcessUntappd processUntappd, ILogger logger) : IProcessBeers
    {
        public void ProcessAllBeer()
        {
            var bieren = processElings.GetElingsBeers();
            processUntappd.GetUntappedRatingAsync(bieren);
            exportCSV.ConvertToCsv(bieren);
            logger.LogInformation("All Beers Read!");
        }
    }
}
