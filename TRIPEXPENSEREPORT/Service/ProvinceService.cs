using Microsoft.Data.SqlClient;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Service
{
    public class ProvinceService : IProvince
    {
        public List<ProvinceModel> GetProvinces()
        {
            List<ProvinceModel> provinces = new List<ProvinceModel>();
            try
            {
                string strCmd = string.Format($@"SELECT zipcode,province FROM Province WHERE zipcode <> ''");
                SqlCommand command = new SqlCommand(strCmd, ConnectSQL.OpenReportConnect());
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ProvinceModel province = new ProvinceModel()
                        {
                            zipcode = dr["zipcode"].ToString(),
                            province = dr["province"].ToString()
                        };
                        provinces.Add(province);
                    }
                    dr.Close();
                }
            }
            finally
            {
                if (ConnectSQL.con_report.State == System.Data.ConnectionState.Open)
                {
                    ConnectSQL.CloseReportConnect();
                }
            }
            return provinces;
        }
    }
}
