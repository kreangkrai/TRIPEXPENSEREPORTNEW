using Microsoft.Data.SqlClient;
using NPOI.SS.Formula.Functions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Data;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;
using static NPOI.HSSF.Util.HSSFColor;

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
        public List<GasolineModel> GetGasoline()
        {
            List<GasolineModel> gasolines = new List<GasolineModel>();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string strCmd = string.Format($@"SELECT month,sohol,diesel FROM Gasoline");
                SqlCommand command = new SqlCommand(strCmd, con);
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        GasolineModel gasoline = new GasolineModel()
                        {
                            month = dr["month"].ToString(),
                            sohol = dr["sohol"] != DBNull.Value ? Convert.ToDouble(dr["sohol"].ToString()) : 0,
                            diesel = dr["diesel"] != DBNull.Value ? Convert.ToDouble(dr["diesel"].ToString()) : 0,
                        };
                        gasolines.Add(gasoline);
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
            return gasolines;
        }
        public string Delete(GasolineModel model)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                if (model.month != "")
                {
                    string sql_user = "DELETE FROM Gasoline WHERE Month='" + model.month + "'";
                    SqlCommand com = new SqlCommand(sql_user, con);
                    com.ExecuteNonQuery();
                    return "Delete Success";
                }
                else
                {
                    return "Delete Failed";
                }
            }
            catch
            {
                return "Delete Failed";
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public double GetDiesel(string month)
        {
            double value = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT Diesel FROM Gasoline WHERE Month='" + month + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        value = Convert.ToDouble(dr["Diesel"].ToString());
                    }
                    dr.Close();
                }
            }
            catch
            {
                value = 0.0;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return value;
        }


        public double GetSohol(string month)
        {
            double value = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT Sohol FROM Gasoline WHERE Month='" + month + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        value = Convert.ToDouble(dr["Sohol"].ToString());
                    }
                    dr.Close();
                }
            }
            catch
            {
                value = 0.0;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return value;
        }

        public double GetValueGasByMonth(string gas, string month)
        {
            double gasValue = 0.0;

            if (gas == "Diesel")
                gasValue = GetDiesel(month);
            if (gas == "Bensin")
                gasValue = GetSohol(month);
            return gasValue;
        }

        public string Insert(GasolineModel model)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                using (SqlCommand cmd = new SqlCommand($@"IF NOT EXISTS (
                                                            SELECT 1
                                                            FROM Gasoline
                                                            WHERE month = @month
                                                        )
                                                        BEGIN
                                                            INSERT INTO Gasoline(month, sohol, diesel)
                                                            VALUES(@month, @sohol, @diesel)
                                                        END", con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@month", model.month);
                    cmd.Parameters.AddWithValue("@sohol", model.sohol);
                    cmd.Parameters.AddWithValue("@diesel", model.diesel);
                    cmd.ExecuteNonQuery();
                    return "Insert Success";
                }

            }
            catch
            {
                return "Insert Failed";
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public string Update(GasolineModel model)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                if (model.month != "")
                {
                    SqlDataReader reader;
                    SqlCommand cmd = new SqlCommand("UPDATE Gasoline SET Sohol='" + model.sohol + "',Diesel='" + model.diesel + "' WHERE Month='" + model.month + "'");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    reader = cmd.ExecuteReader();
                    reader.Close();

                    return "Update Success";
                }
                else
                {
                    return "Update Failed";
                }
            }
            catch
            {
                return "Update Failed";
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
    }
}
