using Microsoft.Data.SqlClient;
using System.Data;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Service
{
    public class GasolineService : IGasoline
    {
        ConnectSQL connect = null;
        SqlConnection con = null;
        public GasolineService()
        {
            connect = new ConnectSQL();
            con = connect.OpenReportConnect();
        }
        public GasolineModel GetGasolineByMonth(string month)
        {
            GasolineModel gasoline = new GasolineModel();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string strCmd = string.Format($@"SELECT month,sohol,diesel FROM Gasoline WHERE month = @month");
                SqlCommand command = new SqlCommand(strCmd, con);
                command.Parameters.AddWithValue("@month", month);
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        gasoline = new GasolineModel()
                        {
                            month = dr["month"].ToString(),
                            sohol = dr["sohol"] != DBNull.Value ? Convert.ToDouble(dr["sohol"].ToString()) : 0,
                            diesel = dr["diesel"] != DBNull.Value ? Convert.ToDouble(dr["diesel"].ToString()) : 0,
                        };
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
            return gasoline;
        }
    }
}
