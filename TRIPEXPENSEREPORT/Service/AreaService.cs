using Microsoft.Data.SqlClient;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Service
{
    public class AreaService : IArea
    {
        public List<AreaModel> GetAreas()
        {
            List<AreaModel> areas = new List<AreaModel>();
            try
            {
                string strCmd = string.Format($@"SELECT code,hq,rbo,kbo FROM Area" );
                SqlCommand command = new SqlCommand(strCmd, ConnectSQL.OpenReportConnect());
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
                if (ConnectSQL.con_report.State == System.Data.ConnectionState.Open)
                {
                    ConnectSQL.CloseReportConnect();
                }
            }
            return areas;
        }
    }
}
