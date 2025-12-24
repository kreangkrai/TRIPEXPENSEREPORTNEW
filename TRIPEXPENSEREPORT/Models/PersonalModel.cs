namespace TRIPEXPENSEREPORT.Models
{
    public class PersonalModel
    {
        public int id { get; set; }
        public string trip { get; set; }
        public DateTime date { get; set; }
        public string driver { get; set; }
        public TimeSpan time_start { get; set; }
        public TimeSpan time_stop { get; set; }
        public string location { get; set; }
        public string job { get; set; }
        public double cash { get; set; }
        public double ctbo { get; set; }
        public double exp { get; set; }
        public double pt { get; set; }
        public int mileage_start { get; set; }
        public int mileage_stop { get; set; }
        public int km { get; set; }
        public int program_km { get; set; }
        public int auto_km { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string gasoline { get; set; }
        public string approver { get; set; }
        public DateTime last_date { get; set; }
    }
}
