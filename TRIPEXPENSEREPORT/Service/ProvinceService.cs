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
            SqlConnection connection = ConnectSQL.OpenConnect();
            try
            {
                string strCmd = string.Format($@"SELECT zipcode,province FROM Province");
                SqlCommand command = new SqlCommand(strCmd, connection);
                connection.Open();
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
                connection.Close();
            }
            return provinces;
        }
    }
}
