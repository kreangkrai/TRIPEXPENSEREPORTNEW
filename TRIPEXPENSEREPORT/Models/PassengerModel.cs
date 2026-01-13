namespace TRIPEXPENSEREPORT.Models
{
    public class PassengerModel
    {
        public int id { get; set; }
        public string code { get; set; }
        public DateTime date { get; set; }
        public string zipcode { get; set; }
        public string passenger { get; set; }
        public TimeSpan time_start { get; set; }
        public TimeSpan time_stop { get; set; }
        public string location { get; set; }
        public string job { get; set; }      
    }
}
