namespace TRIPEXPENSEREPORT.Models
{
    public class AllowanceViewModel
    {
        public int id { get; set; }
        public string code { get; set; }
        public string emp_id { get; set; }
        public string emp_name { get; set; }
        public string zipcode { get; set; }
        public DateTime date { get; set; }
        public string customer { get; set; }
        public string job { get; set; }
        public double allowance_province { get; set; }
        public double allowance_1_4 { get; set; }
        public double allowance_4_8 { get; set; }
        public double allowance_8 { get; set; }
        public double allowance_other { get; set; }
        public double allowance_hostel { get; set; }
        public string list { get; set; }
        public double amount { get; set; }
        public string description { get; set; }
        public string remark { get; set; }
        public string status { get; set; }
        public string approver { get; set; }
        public string approver_name { get; set; }
        public DateTime last_date { get; set; }
    }
}
