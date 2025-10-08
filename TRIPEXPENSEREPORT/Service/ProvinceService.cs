using Microsoft.Data.SqlClient;
using System.Data;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Service
{
    public class ProvinceService : IProvince
    {
        ConnectSQL connect = null;
        SqlConnection con = null;
        public ProvinceService()
        {
            connect = new ConnectSQL();
            con = connect.OpenReportConnect();
        }
        public List<ProvinceModel> GetProvinces()
        {
            List<ProvinceModel> provinces = new List<ProvinceModel>();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string strCmd = string.Format($@"SELECT zipcode,province FROM Province WHERE zipcode <> ''");
                SqlCommand command = new SqlCommand(strCmd, con);
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
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return provinces;
        }
    }
}
