namespace TRIPEXPENSEREPORT.Models
{
    public class DataTripModel
    {
        public string emp_id { get; set; }
        public string job_id { get; set; }
        public DateTime date { get; set; }
        public TimeSpan time_start { get; set; }
        public TimeSpan time_stop { get; set; }
        public string zipcode  { get; set; }
        public string mode { get; set; }
        public string location {  get; set; }
        public string location_mode { get; set; }
    }
}
