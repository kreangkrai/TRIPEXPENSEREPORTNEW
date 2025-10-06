using Microsoft.Data.SqlClient;
using System.Data;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Service
{
    public class EmployeeService : IEmployee
    {
        public List<EmployeeModel> GetEmployees()
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();
            try
            {
                string strCmd = string.Format($@"SELECT emp_id,name,department,role FROM Employees");
                SqlCommand command = new SqlCommand(strCmd, ConnectSQL.OpenConnect());
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
                            role = dr["role"].ToString()
                        };
                        employees.Add(employee);
                    }
                    dr.Close();
                }
            }
            finally
            {
                if (ConnectSQL.con.State == ConnectionState.Open)
                {
                    ConnectSQL.CloseConnect();
                }
            }
            return employees;
        }

        public string Inserts(List<EmployeeModel> employees)
        {
            SqlConnection conn = ConnectSQL.OpenConnect();
            try
            {
                string string_command = string.Format($@"
                    INSERT INTO 
                        OriginalPersonal(emp_id,
                            name,
                            department,
                            role
                        )
                        VALUES(@emp_id,
                            @name,
                            @department,
                            @role
                        )");
                using (SqlCommand cmd = new SqlCommand(string_command, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@emp_id", SqlDbType.Text);
                    cmd.Parameters.Add("@name", SqlDbType.Text);
                    cmd.Parameters.Add("@department", SqlDbType.Text);
                    cmd.Parameters.Add("@role", SqlDbType.Text);

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Open();
                    }
                    for (int i = 0; i < employees.Count; i++)
                    {
                        cmd.Parameters[0].Value = employees[i].emp_id;
                        cmd.Parameters[1].Value = employees[i].name;
                        cmd.Parameters[2].Value = employees[i].department;
                        cmd.Parameters[3].Value = employees[i].role;
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
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return "Success";
        }

        public string Update(EmployeeModel employee)
        {
            SqlConnection conn = ConnectSQL.OpenConnect();
            try
            {
                string string_command = string.Format($@"
                    UPDATE 
                        Employees SET
  	                    name = @name,
                        department = @department,
                        role = @role
                        WHERE emp_id = @emp_id");
                using (SqlCommand cmd = new SqlCommand(string_command, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@emp_id", employee.emp_id);
                    cmd.Parameters.AddWithValue("@name", employee.name);
                    cmd.Parameters.AddWithValue("@department", employee.department);
                    cmd.Parameters.AddWithValue("@role", employee.role);
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Open();
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return "Success";
        }
    }
}
