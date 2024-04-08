using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadHTML
{
    public record Appsettings
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string OutputPath { get; set; }
    }
}
