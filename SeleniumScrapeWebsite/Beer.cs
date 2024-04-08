using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ReadHTML
{
    public class Beer
    {
        public string Brewery { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
        public string Volume { get; set; }
        public double Price { get; set; }
        public double PricePerPiece { get; set; }
        public string Stock { get; set; }
        public string AlcoholPercentage { get; set; }
        public double UntappedRating { get; set; }
        public bool Sale { get; set; }
        public double StandardPrice { get; set; }
    }
}
