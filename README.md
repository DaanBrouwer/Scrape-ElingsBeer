# C# .NET Web Scraping Project

This project aims to scrape data from the Elings Craftbeer and Untappd website using C# .NET and Selenium.
You'll need to set up your environment, install necessary libraries, and create an `appsettings.json` file to store sensitive information.

## Getting Started

1. **Clone the Repository:**

   ```
   git clone https://github.com/DaanBrouwer/Scrape-ElingsBeer.git
   cd Scrape-ElingsBeer
   ```

2. **Install Dependencies:**
   Make sure you have the .NET SDK installed. Then, restore the NuGet packages:

   ```
   dotnet restore
   ```

3. **Create `appsettings.json`:**
   Create a file named `appsettings.json` in the root directory. Add the following content:

   ```json
   {
     "Appsettings": {
       "UserName": "",
       "Password": "",
       "OutputPath": "*.csv"
     }
   }
   ```

   Replace the values with your actual credentials and desired output path.

4. **Run the Scraper:**
   Build and run the project:
   ```
   dotnet build
   dotnet run
   ```

## Project Structure

- `Program.cs`: Main entry point of the project.
- `ProcessBeers.cs`: Main process class.
- `ProcessElings.cs`: Class responsible for retrieving the beers of each page from Elings Craft Beer.
- `ProcessUntappd.cs`: Class responsible for retrieving the untappd data of the beers.
- `ExportCSV.cs`: Class responsible for exporting beers to a CSV File.
- `appsettings.json`: Configuration file for user data and output path.

## Notes

- **Be cautious with sensitive information:** Never commit your actual credentials to version control. Add `appsettings.json` to your `.gitignore` file.

---
