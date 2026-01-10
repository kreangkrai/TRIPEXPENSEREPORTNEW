using Microsoft.Data.SqlClient;

namespace TRIPEXPENSEREPORT.Service
{
    public class ConnectSQL
    {
        public SqlConnection con;
        public SqlConnection OpenConnect()
        {
            con = new SqlConnection("Data Source = 192.168.15.12, 1433; Initial Catalog = TRIP_EXPENSE; User Id = sa; Password = p@ssw0rd;TrustServerCertificate=True; Timeout = 120");
            return con;
        }

        public SqlConnection OpenReportConnect()
        {
            con = new SqlConnection("Data Source = 192.168.15.12, 1433; Initial Catalog = TRIP_EXPENSE_REPORT; User Id = sa; Password = p@ssw0rd;TrustServerCertificate=True; Timeout = 120");
            return con;
        }

        public SqlConnection OpenCTLConnect()
        {
            con = new SqlConnection("Data Source = 192.168.15.12, 1433; Initial Catalog = CTL; User Id = sa; Password = p@ssw0rd;TrustServerCertificate=True; Timeout = 120");
            return con;
        }
    }
}
