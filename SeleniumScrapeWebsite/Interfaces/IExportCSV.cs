namespace ReadHTML.Interfaces
{
    public interface IExportCSV
    {
        void ConvertToCsv<T>(IEnumerable<T> data, string filePath);
    }
}