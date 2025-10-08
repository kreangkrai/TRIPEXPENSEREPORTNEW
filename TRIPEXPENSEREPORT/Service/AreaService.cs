using Microsoft.Data.SqlClient;
using System.Data;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Service
{
    public class AreaService : IArea
    {
        ConnectSQL connect = null;
        SqlConnection con = null;
        public AreaService()
        {
            connect = new ConnectSQL();
            con = connect.OpenReportConnect();
        }
        public List<AreaModel> GetAreas()
        {
            List<AreaModel> areas = new List<AreaModel>();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string strCmd = string.Format($@"SELECT code,hq,rbo,kbo FROM Area" );
                SqlCommand command = new SqlCommand(strCmd, con);
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        AreaModel area = new AreaModel()
                        {
                            code = dr["code"].ToString(),
                            hq = dr["hq"] != DBNull.Value ? Convert.ToBoolean(dr["hq"].ToString()) :false,
                            rbo = dr["rbo"] != DBNull.Value ? Convert.ToBoolean(dr["rbo"].ToString()) : false,
                            kbo = dr["kbo"] != DBNull.Value ? Convert.ToBoolean(dr["kbo"].ToString()) : false,
                        };
                        areas.Add(area);
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
            return areas;
        }
    }
}
