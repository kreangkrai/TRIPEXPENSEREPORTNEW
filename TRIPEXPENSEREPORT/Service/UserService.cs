using Microsoft.Data.SqlClient;
using TRIPEXPENSEREPORT.Models;
using TRIPEXPENSEREPORT.Interface;
namespace TRIPEXPENSEREPORT.Service
{
    public class UserService : IUser
    {
        public List<UserManagementModel> GetUsers()
        {
            try
            {
                List<UserManagementModel> users = new List<UserManagementModel>();
                SqlCommand cmd = new SqlCommand("select emp_id,name,department,role from [Employees] order by name", ConnectSQL.OpenConnect());
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
                if (ConnectSQL.con.State == System.Data.ConnectionState.Open)
                {
                    ConnectSQL.CloseConnect();
                }
            }
        }
    }
}
