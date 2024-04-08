using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using ReadHTML.Interfaces;

namespace ReadHTML
{
    public class ProcessElings : IProcessElings
    {
        private readonly Appsettings _options;
        private readonly IWebDriver _driver;

        public ProcessElings(IOptions<Appsettings> options, IWebDriver webDriver)
        {
            _options = options.Value;
            _driver = webDriver;
        }

        public List<Beer> GetElingsBeers()
        {
            _driver.Navigate().GoToUrl($"https://elingscraftbeer.shop/account/login");

            var emailinput = _driver.FindElement(By.XPath("//*[@id=\"customer[email]\"]"));
            var passwordInput = _driver.FindElement(By.XPath("//*[@id=\"customer[password]\"]"));
            var submitButton = _driver.FindElement(By.XPath("//*[@id=\"customer_login\"]/button"));

            emailinput.SendKeys(_options.UserName);
            passwordInput.SendKeys(_options.Password);
            submitButton.Click();
            _driver.Navigate().GoToUrl($"https://elingscraftbeer.shop/collections/alle-bieren?sort_by=created-descending");

            var totalPagesText = _driver.FindElement(By.XPath("//*[@id=\"shopify-section-template--16713055338752__main\"]/section/div[1]/div[2]/div[2]/div/div[2]/div/div[3]/div/div/a[3]"));
            int totalPages = Convert.ToInt32(totalPagesText.Text);

            List<Beer> bieren = new List<Beer>();
            for (int i = 1; i <= totalPages; i++)
            {
                _driver.Navigate().GoToUrl($"https://elingscraftbeer.shop/collections/alle-bieren?sort_by=created-descending&page={i}");
                bieren.AddRange(GetAllBeersFromPage());
                
                Console.WriteLine($"Pagina {i} verwerkt");
            }
            _driver.Dispose();
            return bieren;
        }

        private List<Beer> GetAllBeersFromPage()
        {

            var beers = _driver.FindElements(By.XPath("//*[@id=\"shopify-section-template--16713055338752__main\"]/section/div[1]/div[2]/div[2]/div/div[2]/div/div[2]/div/div/div"));
            var AllBeersFromPage = new List<Beer>();
            foreach (var beer in beers)
            {
                var price = beer.FindElements(By.XPath(".//span"))[0].Text;
                string[] parts = price.Split(new[] { "\r\n€" }, StringSplitOptions.None);
                var stock = beer.FindElements(By.XPath(".//span"))[2].Text.Trim();
                var sale = false;
                double standardPrice = 0;
                var splitAmount = beer.FindElement(By.XPath(".//div[2]")).Text.Trim().Split(new[] { "x" }, StringSplitOptions.None);


                if (stock.Contains("Standaard"))
                {
                    sale = true;
                    var stockSplit = stock.Split(new[] { "\r\n€" }, StringSplitOptions.None);
                    standardPrice = Math.Round(double.Parse(stockSplit[1]) * 1.21, 2);
                    stock = beer.FindElements(By.XPath(".//span"))[4].Text.Trim();

                }

                double priceExVat = double.Parse(parts[1]);
                double amount = double.Parse(splitAmount[0]);
                var volume = splitAmount[1];
                if (splitAmount.Count() >= 3)
                {
                    amount = double.Parse(splitAmount[0]) * double.Parse(splitAmount[1]);
                    volume = splitAmount[2];
                }

                var singleBeer = new Beer
                {

                    Name = beer.FindElement(By.XPath(".//a[2]")).Text.Trim(),
                    Brewery = beer.FindElement(By.XPath(".//a[1]")).Text.Trim(),
                    Amount = amount,
                    Volume = volume,
                    Price = Math.Round(priceExVat * 1.21, 2),
                    PricePerPiece = Math.Round(priceExVat * 1.21 / amount, 2),
                    Type = beer.FindElement(By.XPath(".//div[1]")).Text.Trim(),
                    Stock = stock,
                    Sale = sale,
                    StandardPrice = standardPrice
                };

                AllBeersFromPage.Add(singleBeer);

                Console.WriteLine($"{singleBeer.Name} verwerkt");
            }
            return AllBeersFromPage;
        }

    }
}
