using System.ComponentModel.DataAnnotations;

namespace WeatherRecordWebsite.Models
{
    public class Record
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public int TemperatureId { get; set; }
        public int WindSpeedId { get; set; }
        public int WeatherId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
