using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;
using TRIPEXPENSEREPORT.Service;

namespace TESTREPORT
{
    public class RBO
    {
        private IAllowance Allowance;
        List<DataModel> DataModels = new List<DataModel>();
        public RBO()
        {
            Allowance = new AllowanceService();
        }

        //HQ

        DataModel DataModel = ReadExcel.Read()[1];

        [Fact]
        public void TestInZone1()
        {
            DateTime start = new DateTime(2025, 9, 1);
            DateTime stop = new DateTime(2025, 9, 30);
            List<DataTripModel> trips = new List<DataTripModel>();

            for (int i = 0; i < DataModel.datas.Count; i++)
            {
                DataTripModel trip1 = new DataTripModel()
                {
                    date = DataModel.datas[i].start,
                    emp_id = DataModel.emp_id,
                    job_id = "J25-9999",
                    location = "CTL(HQ)",
                    location_mode = "OTHER",
                    mode = "PERSONAL",
                    status = "START",
                    trip = DataModel.trip,
                    zipcode = DataModel.datas[i].zipcode
                };
                DataTripModel trip2 = new DataTripModel()
                {
                    date = DataModel.datas[i].end,
                    emp_id = DataModel.emp_id,
                    job_id = "J25-9999",
                    location = "HOME",
                    location_mode = "OTHER",
                    mode = "PERSONAL",
                    status = "STOP",
                    trip = DataModel.trip,
                    zipcode = DataModel.datas[i].zipcode
                };
                trips.Add(trip1);
                trips.Add(trip2);

                AllowanceModel expect = new AllowanceModel()
                {
                    allowance_1_4 = DataModel.datas[i].a_1_4,
                    allowance_4_8 = DataModel.datas[i].a_4_8,
                    allowance_8 = DataModel.datas[i].a_8,
                    allowance_province = DataModel.datas[i].province
                };
                List<AllowanceModel> allowances = Allowance.CalculateAllowanceNew(DataModel.emp_id, trips, start, stop);
                AllowanceModel allowance = allowances.Where(a => a.date.Date == new DateTime(2025, 9, 25)).First();

                Assert.NotEmpty(allowances);
                Assert.Equal(expect.allowance_1_4, allowance.allowance_1_4);
                Assert.Equal(expect.allowance_4_8, allowance.allowance_4_8);
                Assert.Equal(expect.allowance_8, allowance.allowance_8);
                Assert.Equal(expect.allowance_province, allowance.allowance_province);
            }
        }
    }
}
