namespace TRIPEXPENSEREPORT.Models
{
    public class DataModel
    {
        public string car_id { get; set; }
        public string driver { get; set; }
        public string passenger { get; set; }
        public string job_id { get; set; }
        public string trip { get; set; }
        public DateTime trip_date { get; set; }
        public DateTime date { get; set; }
        public string status { get; set; }
        public double distance { get; set; }
        public double speed { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double accuracy { get; set; }
        public string location_mode { get; set; }
        public string location { get; set; }
        public string zipcode { get; set; }
        public int mileage { get; set; }
        public double cash { get; set; }
        public double fleetcard { get; set; }
        public string borrower { get; set; }      
        public string mode { get; set; }
    }

    public class TripModel
    {
        public string car_id { get; set; }
        public string driver { get; set; }
        public string passenger { get; set; }
        public string job_id { get; set; }
        public string trip { get; set; }
        public DateTime date { get; set; }
        public DateTime date_start { get; set; }
        public DateTime date_stop { get; set; }
        public string status { get; set; }
        public double distance { get; set; }
        public double speed { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double accuracy { get; set; }
        public string location_mode { get; set; }
        public string location { get; set; }
        public string zipcode { get; set; }
        public int mileage { get; set; }
        public double cash { get; set; }
        public double fleetcard { get; set; }
        public string borrower { get; set; }
        public string mode { get; set; }
    }
    public class LocationModel
    {

        public string car { get; set; }
        public string driver { get; set; }
        public string passenger { get; set; }
        public string trip { get; set; }
        public DateTime date { get; set; }
        public string location { get; set; }
        public TimeSpan time { get; set; }
        public string status { get; set; }
        public int duration { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string mode { get; set; }
    }
}