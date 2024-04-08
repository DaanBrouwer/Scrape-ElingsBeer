﻿using System.Reflection;
using System.Text;
using Microsoft.Extensions.Options;
using ReadHTML.Interfaces;

namespace ReadHTML
{
    public class ExportCSV : IExportCSV
    {
        private readonly Appsettings _options;

        public ExportCSV(IOptions<Appsettings> options)
        {
            _options = options.Value;
        }

        public void ConvertToCsv<T>(IEnumerable<T> data)
        {
            if (data == null || !data.Any())
            {
                throw new ArgumentException("Data cannot be null or empty");
            }

            if (string.IsNullOrEmpty(_options.OutputPath))
            {
                throw new ArgumentException("File path cannot be null or empty");
            }

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var headers = string.Join(";", properties.Select(p => p.Name));

            using (var writer = new StreamWriter(_options.OutputPath, false, new UTF8Encoding(true)))
            {
                writer.WriteLine(headers);

                foreach (var item in data)
                {
                    var values = string.Join(";", properties.Select(p => p.GetValue(item)));
                    writer.WriteLine(values);
                }
                Console.WriteLine($"Exported csv to {_options.OutputPath}");
            }
        }
    }
}
