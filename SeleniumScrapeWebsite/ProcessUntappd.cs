using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using ReadHTML.Interfaces;

namespace ReadHTML
{
    public class ProcessUntappd(ILogger<ProcessUntappd> logger) : IProcessUntappd
    {
        public void GetUntappedRatingAsync(List<Beer> bieren)
        {
            IEnumerable<Beer[]> chunks = bieren.Chunk(bieren.Count / 2);
            Parallel.ForEach(chunks, beers =>
            {
                IWebDriver webDriver = new EdgeDriver();
                bool cookieClicked = false;
                foreach (var beer in beers)
                {
                    webDriver.Navigate().GoToUrl($"https://untappd.com/search?q={beer.Brewery}+{beer.Name}");

                    int retryCount = 5;
                    bool flag = false;
                    if (!cookieClicked)
                    {
                        while (retryCount > 0 && !flag)
                        {
                            try
                            {
                                var cookieButton = webDriver.FindElement(By.XPath("/html/body/div[5]/div[2]/div[1]/div[2]/div[2]/button[1]"));
                                cookieButton.Click();
                                cookieClicked = true;
                                flag = true;
                            }
                            catch (NoSuchElementException ex)
                            {
                                retryCount--;
                                if (retryCount == 0)
                                {
                                    logger.LogError("Operation Failed... {ex}", ex);
                                }
                            }
                        }
                    }
                    try
                    {
                        var getRatingElement = webDriver.FindElement(By.XPath("//*[@id=\"slide\"]/div[2]/div[1]/div/div/div[3]/div[2]/div/div[2]/div/div"));
                        var Rating = getRatingElement.GetAttribute("data-rating");
                        var alcoholPercentage = webDriver.FindElement(By.XPath("//*[@id=\"slide\"]/div[2]/div[1]/div/div/div[3]/div[2]/div/div[2]/p[1]")).Text;

                        var convertPercentage = alcoholPercentage.Substring(0, alcoholPercentage.Length - 4).Replace(".", ",");
                        beer.AlcoholPercentage = convertPercentage == "N/A" ? "0,0%" : convertPercentage;
                        beer.UntappedRating = Math.Round(double.Parse(Rating.Replace(".", ",")), 2);
                        logger.LogInformation("{BeerName} rating verwerkt", beer.Name);
                    }
                    catch (Exception ex)
                    {
                        beer.AlcoholPercentage = "Nothing found";
                        beer.UntappedRating = 9;
                        logger.LogError("{beerName} rating NIET verwerkt, {ex}", beer.Name, ex);
                    }
                };
                webDriver.Close();
            });

        }
    }
}
