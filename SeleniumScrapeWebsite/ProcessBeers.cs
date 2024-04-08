using Microsoft.Extensions.Options;
using ReadHTML.Interfaces;

namespace ReadHTML
{
    public class ProcessBeers(IExportCSV exportCSV, IProcessElings processElings, IProcessUntappd processUntappd) : IProcessBeers
    {
        public void ProcessAllBeer()
        {
            var bieren = processElings.GetElingsBeers();
            processUntappd.GetUntappedRatingAsync(bieren);
            exportCSV.ConvertToCsv(bieren);
            Console.WriteLine("All Beers Read!");
        }
    }
}
