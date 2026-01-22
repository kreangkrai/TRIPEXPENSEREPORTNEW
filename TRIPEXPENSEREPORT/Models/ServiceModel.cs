namespace TRIPEXPENSEREPORT.Models
{
    public class ServiceModel
    {
        public int id { get; set; }
        public string car_id { get; set; }
        public string license_plate { get; set; }
        public string service_id { get; set; }
        public string service_name { get; set; }
        public int current_mileage { get; set; }
        public int mileage_at_service { get; set; }
        public DateTime date_at_service { get; set; }
        public int appointment_mileage { get; set; }
        public DateTime appointment_date { get; set; }
        public int next_appointment_mileage { get; set; }
        public DateTime next_appointment_date { get; set; }
        public string detail { get; set; }
        public string note { get; set; }
        public string location_service { get; set; }
        public string name { get; set; }
    }
}
