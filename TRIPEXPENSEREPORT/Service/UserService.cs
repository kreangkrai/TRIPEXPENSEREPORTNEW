using Microsoft.Data.SqlClient;
using TRIPEXPENSEREPORT.Models;
using TRIPEXPENSEREPORT.Interface;
using System.Data;
using static NPOI.HSSF.Util.HSSFColor;
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

        public string update(string emp_id, string role)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string string_command = string.Format($@"
                    UPDATE [Employees] SET
                                    role = @role
                                    WHERE emp_id = @emp_id");
                using (SqlCommand cmd = new SqlCommand(string_command, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@role", role);
                    cmd.Parameters.AddWithValue("@emp_id", emp_id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return "Success";
        }
        public string insert(UserManagementModel users)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string string_command = string.Format($@"
                    INSERT INTO [Employees] (emp_id,name,department,location,role) VALUES (@emp_id,@name,@department,@location,@role) ");
                using (SqlCommand cmd = new SqlCommand(string_command, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@emp_id", users.emp_id);
                    cmd.Parameters.AddWithValue("@name", users.name);
                    cmd.Parameters.AddWithValue("@department", users.department);
                    cmd.Parameters.AddWithValue("@location", users.location);
                    cmd.Parameters.AddWithValue("@role", users.role);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return "Success";
        }
    }
}
