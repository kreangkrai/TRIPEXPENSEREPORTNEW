using Microsoft.Data.SqlClient;

namespace TRIPEXPENSEREPORT.Service
{
    public class ConnectSQL
    {
        public static SqlConnection con;
        public static SqlConnection con_ctl;
        public static SqlConnection OpenConnect()
        {
            con = new SqlConnection("Data Source = 192.168.15.12, 1433; Initial Catalog = TRIP_EXPENSE; User Id = sa; Password = p@ssw0rd;TrustServerCertificate=True; Timeout = 120");
            con.Open();
            return con;
        }
        public static SqlConnection CloseConnect()
        {
            con.Close();
            return con;
        }

        public static SqlConnection Open_CTL_Connect()
        {
            con_ctl = new SqlConnection("Data Source = 192.168.15.12, 1433; Initial Catalog = CTL; User Id = sa; Password = p@ssw0rd;TrustServerCertificate=True; Timeout = 120");
            con_ctl.Open();
            return con_ctl;
        }
        public static SqlConnection Close_CTL_Connect()
        {
            con_ctl.Close();
            return con_ctl;
        }
    }
}
