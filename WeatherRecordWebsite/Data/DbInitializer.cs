using Microsoft.CodeAnalysis.CSharp.Syntax;
using WeatherRecordWebsite.Models;

namespace WeatherRecordWebsite.Data
{
    public class DbInitializer
    {
        public static void Initialize(WeatherContext context)
        {
            // Look for any students.
            if (context.Temperature.Any())
            {
                return;   // DB has been seeded
            }
            
        }
    }
}