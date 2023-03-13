namespace WeatherRecordWebsite.Models
{
    public class RecordView
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public Temperature Temperature { get; set; }
        public WindSpeed WindSpeed { get; set; }
        public Weather Weather { get; set; }
        public City City { get; set; }
    }
}
