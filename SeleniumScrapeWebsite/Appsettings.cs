namespace ReadHTML
{
    public record Appsettings
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string OutputPath { get; set; }
    }
}
