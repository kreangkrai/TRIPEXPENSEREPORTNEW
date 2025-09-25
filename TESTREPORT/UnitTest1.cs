using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Service;
using TRIPEXPENSEREPORT.Models;
using Xunit;
using Moq;
using Microsoft.Extensions.DependencyInjection;
namespace TESTREPORT
{
 
    public class UnitTest1
    {
        private readonly IAllowance Allowance;

        public UnitTest1()
        {
            Allowance = new AllowanceService();
        }

        // 1	25/09/2025 8:30	25/09/2025 9:29	0:59	11	ต่ำกว่า 1 ชม.	ในเขต	0	0	0	0
        [Fact]
        public void TestInZone1()
        {
            DateTime start = new DateTime(2025, 9, 1);
            DateTime stop = new DateTime(2025, 9, 30);
            List<DataTripModel> trips = new List<DataTripModel>();

            DataTripModel trip1 = new DataTripModel()
            {
                date = new DateTime(2025,9,25,8,30,0),
                emp_id = "059197",
                job_id = "J25-9999",
                location = "CTL(HQ)",
                location_mode = "OTHER",
                mode = "PERSONAL",
                status = "START",
                trip = "1",
                zipcode = "11520"
            };
            DataTripModel trip2 = new DataTripModel()
            {
                date = new DateTime(2025, 9, 25, 9, 29, 0),
                emp_id = "059197",
                job_id = "J25-9999",
                location = "HOME",
                location_mode = "OTHER",
                mode = "PERSONAL",
                status = "STOP",
                trip = "1",
                zipcode = "11520"
            };
            trips.Add(trip1);
            trips.Add(trip2);

            AllowanceModel expect = new AllowanceModel()
            {
                allowance_1_4 = 0,
                allowance_4_8 = 0,
                allowance_8 = 0,
                allowance_province = 0
            };
            List<AllowanceModel> allowances = Allowance.CalculateAllowanceNew("059197", trips, start, stop);
            AllowanceModel allowance = allowances.Where(a => a.date.Date == new DateTime(2025, 9, 25)).First();

            Assert.NotEmpty(allowances);
            Assert.Equal(expect.allowance_1_4, allowance.allowance_1_4);
            Assert.Equal(expect.allowance_4_8, allowance.allowance_4_8);
            Assert.Equal(expect.allowance_8, allowance.allowance_8);
            Assert.Equal(expect.allowance_province, allowance.allowance_province);

        }


        // 16	25/09/2025 8:30	25/09/2025 9:29	0:59	22	ต่ำกว่า 1 ชม.	นอกเขต (ในระบบ)	100	0	0	0
        [Fact]
        public void TestOutZone1()
        {
            DateTime start = new DateTime(2025, 9, 1);
            DateTime stop = new DateTime(2025, 9, 30);
            List<DataTripModel> trips = new List<DataTripModel>();

            DataTripModel trip1 = new DataTripModel()
            {
                date = new DateTime(2025, 9, 25, 8, 30, 0),
                emp_id = "059197",
                job_id = "J25-9999",
                location = "CTL(HQ)",
                location_mode = "OTHER",
                mode = "PERSONAL",
                status = "START",
                trip = "1",
                zipcode = "22520"
            };
            DataTripModel trip2 = new DataTripModel()
            {
                date = new DateTime(2025, 9, 25, 9, 29, 0),
                emp_id = "059197",
                job_id = "J25-9999",
                location = "HOME",
                location_mode = "OTHER",
                mode = "PERSONAL",
                status = "STOP",
                trip = "1",
                zipcode = "22520"
            };
            trips.Add(trip1);
            trips.Add(trip2);

            AllowanceModel expect = new AllowanceModel()
            {
                allowance_1_4 = 0,
                allowance_4_8 = 0,
                allowance_8 = 0,
                allowance_province = 100
            };
            List<AllowanceModel> allowances = Allowance.CalculateAllowanceNew("059197", trips, start, stop);
            AllowanceModel allowance = allowances.Where(a => a.date.Date == new DateTime(2025, 9, 25)).First();

            Assert.NotEmpty(allowances);
            Assert.Equal(expect.allowance_1_4, allowance.allowance_1_4);
            Assert.Equal(expect.allowance_4_8, allowance.allowance_4_8);
            Assert.Equal(expect.allowance_8, allowance.allowance_8);
            Assert.Equal(expect.allowance_province, allowance.allowance_province);

        }

        //31	25/09/2025 8:30	25/09/2025 9:29	0:59	55	ต่ำกว่า 1 ชม.นอกเขต(นอกระบบ)	100	0	0	0

        [Fact]
        public void TestOutZone_1()
        {
            DateTime start = new DateTime(2025, 9, 1);
            DateTime stop = new DateTime(2025, 9, 30);
            List<DataTripModel> trips = new List<DataTripModel>();

            DataTripModel trip1 = new DataTripModel()
            {
                date = new DateTime(2025, 9, 25, 8, 30, 0),
                emp_id = "059197",
                job_id = "J25-9999",
                location = "CTL(HQ)",
                location_mode = "OTHER",
                mode = "PERSONAL",
                status = "START",
                trip = "1",
                zipcode = "55520"
            };
            DataTripModel trip2 = new DataTripModel()
            {
                date = new DateTime(2025, 9, 25, 9, 29, 0),
                emp_id = "059197",
                job_id = "J25-9999",
                location = "HOME",
                location_mode = "OTHER",
                mode = "PERSONAL",
                status = "STOP",
                trip = "1",
                zipcode = "55520"
            };
            trips.Add(trip1);
            trips.Add(trip2);

            AllowanceModel expect = new AllowanceModel()
            {
                allowance_1_4 = 0,
                allowance_4_8 = 0,
                allowance_8 = 0,
                allowance_province = 100
            };
            List<AllowanceModel> allowances = Allowance.CalculateAllowanceNew("059197", trips, start, stop);
            AllowanceModel allowance = allowances.Where(a => a.date.Date == new DateTime(2025, 9, 25)).First();

            Assert.NotEmpty(allowances);
            Assert.Equal(expect.allowance_1_4, allowance.allowance_1_4);
            Assert.Equal(expect.allowance_4_8, allowance.allowance_4_8);
            Assert.Equal(expect.allowance_8, allowance.allowance_8);
            Assert.Equal(expect.allowance_province, allowance.allowance_province);

        }


        // 1	25/09/2025 8:30	25/09/2025 9:29	0:59	11	ต่ำกว่า 1 ชม.	ในเขต	0	0	0	0
        [Fact]
        public void TestInZone1_1()
        {
            DateTime start = new DateTime(2025, 9, 1);
            DateTime stop = new DateTime(2025, 9, 30);
            List<DataTripModel> trips = new List<DataTripModel>();

            DataTripModel trip1 = new DataTripModel()
            {
                date = new DateTime(2025, 9, 25, 8, 30, 0),
                emp_id = "060226",
                job_id = "J25-9999",
                location = "CTL(HQ)",
                location_mode = "OTHER",
                mode = "PERSONAL",
                status = "START",
                trip = "1",
                zipcode = "21520"
            };
            DataTripModel trip2 = new DataTripModel()
            {
                date = new DateTime(2025, 9, 25, 9, 29, 0),
                emp_id = "060226",
                job_id = "J25-9999",
                location = "HOME",
                location_mode = "OTHER",
                mode = "PERSONAL",
                status = "STOP",
                trip = "1",
                zipcode = "21520"
            };
            trips.Add(trip1);
            trips.Add(trip2);

            AllowanceModel expect = new AllowanceModel()
            {
                allowance_1_4 = 50,
                allowance_4_8 = 70,
                allowance_8 = 80,
                allowance_province = 0
            };
            List<AllowanceModel> allowances = Allowance.CalculateAllowanceNew("060226", trips, start, stop);
            AllowanceModel allowance = allowances.Where(a => a.date.Date == new DateTime(2025, 9, 25)).First();

            Assert.NotEmpty(allowances);
            Assert.Equal(expect.allowance_1_4, allowance.allowance_1_4);
            Assert.Equal(expect.allowance_4_8, allowance.allowance_4_8);
            Assert.Equal(expect.allowance_8, allowance.allowance_8);
            Assert.Equal(expect.allowance_province, allowance.allowance_province);

        }
    }
}