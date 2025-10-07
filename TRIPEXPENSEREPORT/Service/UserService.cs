using Microsoft.Data.SqlClient;
using TRIPEXPENSEREPORT.Models;
using TRIPEXPENSEREPORT.Interface;
using System.Data;
namespace TRIPEXPENSEREPORT.Service
{
    public class UserService : IUser
    {
        ConnectSQL connect = null;
        SqlConnection con = null;
        public UserService()
        {
            connect = new ConnectSQL();
            con = connect.OpenReportConnect();
        }
        public List<UserManagementModel> GetUsers()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                List<UserManagementModel> users = new List<UserManagementModel>();
                SqlCommand cmd = new SqlCommand("select emp_id,name,department,location,role from [Employees] order by name", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        UserManagementModel u = new UserManagementModel()
                        {
                            emp_id = dr["emp_id"].ToString(),
                            name = dr["name"].ToString(),
                            department = dr["department"].ToString(),
                            location = dr["location"].ToString(),
                            role = dr["role"].ToString(),
                        };
                        users.Add(u);
                    }
                    dr.Close();
                }
                return users;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
    }
}
