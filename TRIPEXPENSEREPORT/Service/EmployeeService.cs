using Microsoft.Data.SqlClient;
using System.Data;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Service
{
    public class EmployeeService : IEmployee
    {
        ConnectSQL connect = null;
        SqlConnection con = null;
        public EmployeeService()
        {
            connect = new ConnectSQL();
            con = connect.OpenReportConnect();
        }
        public List<EmployeeModel> GetEmployees()
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string strCmd = string.Format($@"SELECT emp_id,name,department,location ,role FROM Employees");
                SqlCommand command = new SqlCommand(strCmd, con);
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        EmployeeModel employee = new EmployeeModel()
                        {
                            emp_id = dr["emp_id"].ToString(),
                            name = dr["name"].ToString(),
                            department = dr["department"].ToString(),
                            role = dr["role"].ToString(),
                            location = dr["location"].ToString(),
                        };
                        employees.Add(employee);
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
            return employees;
        }

        public string Inserts(List<EmployeeModel> employees)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string string_command = string.Format($@"
                    INSERT INTO 
                        OriginalPersonal(emp_id,
                            name,
                            department,
                            location,
                            role
                        )
                        VALUES(@emp_id,
                            @name,
                            @department,
                            @location,
                            @role
                        )");
                using (SqlCommand cmd = new SqlCommand(string_command, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@emp_id", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@department", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@location", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@role", SqlDbType.NVarChar);

                    for (int i = 0; i < employees.Count; i++)
                    {
                        cmd.Parameters[0].Value = employees[i].emp_id;
                        cmd.Parameters[1].Value = employees[i].name;
                        cmd.Parameters[2].Value = employees[i].department;
                        cmd.Parameters[3].Value = employees[i].location;
                        cmd.Parameters[4].Value = employees[i].role;
                        cmd.ExecuteNonQuery();
                    }
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

        public string Update(EmployeeModel employee)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string string_command = string.Format($@"
                    UPDATE 
                        Employees SET
  	                    name = @name,
                        department = @department,
                        location = @location,
                        role = @role
                        WHERE emp_id = @emp_id");
                using (SqlCommand cmd = new SqlCommand(string_command, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@emp_id", employee.emp_id);
                    cmd.Parameters.AddWithValue("@name", employee.name);
                    cmd.Parameters.AddWithValue("@department", employee.department);
                    cmd.Parameters.AddWithValue("@location", employee.location);
                    cmd.Parameters.AddWithValue("@role", employee.role);
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
