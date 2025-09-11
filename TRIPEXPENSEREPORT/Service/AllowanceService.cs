using Microsoft.Data.SqlClient;
using System.Data;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Service
{
    public class AllowanceService : IAllowance
    {     
        public List<AllowanceViewModel> GetEditAllowancesByDate(DateTime start_date, DateTime stop_date)
        {
            List<AllowanceViewModel> allowances = new List<AllowanceViewModel>();
            SqlConnection connection = ConnectSQL.OpenConnect();
            try
            {
                string strCmd = string.Format($@"SELECT code,
	                                           EditAllowance.emp_id,
                                               emp1.name as emp_name,
	                                           EditAllowance.zipcode,
	                                           p.province
	                                           date,
	                                           customer,
	                                           job,
	                                           allowance_province,
	                                           allowance_1_4,
	                                           allowance_4_8,
	                                           allowance_8,
	                                           allowance_other,
	                                           allowance_hostel,
	                                           list,
	                                           amount,
	                                           description,
	                                           remark,
	                                           status,
												EditAllowance.approver,
												emp2.name as approver_name,
												last_date
                                                FROM EditAllowance
												LEFT JOIN Province p ON EditAllowance.zipcode = p.zipcode
                                                LEFT JOIN Employees emp1 ON EditAllowance.emp_id = emp1.emp_id
												LEFT JOIN Employees emp2 ON EditAllowance.approver = emp2.emp_id
                                                WHERE date BETWEEN @start_date AND @stop_date");
                SqlCommand command = new SqlCommand(strCmd, connection);
                command.Parameters.AddWithValue("@start_date", start_date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@stop_date", stop_date.ToString("yyyy-MM-dd"));
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        AllowanceViewModel allowance = new AllowanceViewModel()
                        {
                            code = dr["code"].ToString(),
                            emp_id = dr["emp_id"].ToString(),
                            emp_name = dr["emp_name"].ToString(),
                            zipcode = dr["zipcode"].ToString(),
                            province = dr["province"].ToString(),
                            date = dr["date"] != DBNull.Value ? Convert.ToDateTime(dr["date"].ToString()) : DateTime.MinValue,
                            customer = dr["customer"].ToString(),
                            job = dr["job"].ToString(),
                            allowance_province = dr["allowance_province"] != DBNull.Value ? Convert.ToDouble(dr["allowance_province"].ToString()) : 0,
                            allowance_1_4 = dr["allowance_1_4"] != DBNull.Value ? Convert.ToDouble(dr["allowance_1_4"].ToString()) : 0,
                            allowance_4_8 = dr["allowance_4_8"] != DBNull.Value ? Convert.ToDouble(dr["allowance_4_8"].ToString()) : 0,
                            allowance_8 = dr["allowance_8"] != DBNull.Value ? Convert.ToDouble(dr["allowance_8"].ToString()) : 0,
                            allowance_other = dr["allowance_other"] != DBNull.Value ? Convert.ToDouble(dr["allowance_other"].ToString()) : 0,
                            allowance_hostel = dr["allowance_hostel"] != DBNull.Value ? Convert.ToDouble(dr["allowance_hostel"].ToString()) : 0,
                            list = dr["list"].ToString(),
                            amount = dr["amount"] != DBNull.Value ? Convert.ToDouble(dr["amount"].ToString()) : 0,
                            description = dr["description"].ToString(),
                            remark = dr["remark"].ToString(),
                            status = dr["status"].ToString(),
                            approver = dr["approver"].ToString(),
                            approver_name = dr["approver_name"].ToString(),
                            last_date = dr["last_date"] != DBNull.Value ? Convert.ToDateTime(dr["last_date"].ToString()) : DateTime.MinValue,
                        };
                        allowances.Add(allowance);
                    }
                    dr.Close();
                }
            }
            finally
            {
                connection.Close();
            }
            return allowances;
        }

        public List<AllowanceViewModel> GetOriginalAllowancesByDate(DateTime start_date, DateTime stop_date)
        {
            List<AllowanceViewModel> allowances = new List<AllowanceViewModel>();
            SqlConnection connection = ConnectSQL.OpenConnect();
            try
            {
                string strCmd = string.Format($@"SELECT code,
	                                           OriginalAllowance.emp_id,
                                               emp1.name as emp_name,
	                                           OriginalAllowance.zipcode,
	                                           p.province
	                                           date,
	                                           customer,
	                                           job,
	                                           allowance_province,
	                                           allowance_1_4,
	                                           allowance_4_8,
	                                           allowance_8,
	                                           allowance_other,
	                                           allowance_hostel,
	                                           list,
	                                           amount,
	                                           description,
	                                           remark,
	                                           status,
												OriginalAllowance.approver,
												emp2.name as approver_name,
												last_date
                                                FROM OriginalAllowance
												LEFT JOIN Province p ON OriginalAllowance.zipcode = p.zipcode
                                                LEFT JOIN Employees emp1 ON OriginalAllowance.emp_id = emp1.emp_id
												LEFT JOIN Employees emp2 ON OriginalAllowance.approver = emp2.emp_id
                                                WHERE date BETWEEN @start_date AND @stop_date");
                SqlCommand command = new SqlCommand(strCmd, connection);
                command.Parameters.AddWithValue("@start_date", start_date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@stop_date", stop_date.ToString("yyyy-MM-dd"));
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        AllowanceViewModel allowance = new AllowanceViewModel()
                        {
                            code = dr["code"].ToString(),
                            emp_id = dr["emp_id"].ToString(),
                            emp_name = dr["emp_name"].ToString(),                          
                            zipcode = dr["zipcode"].ToString(),
                            province = dr["province"].ToString(),
                            date = dr["date"] != DBNull.Value ? Convert.ToDateTime(dr["date"].ToString()) : DateTime.MinValue,
                            customer = dr["customer"].ToString(),
                            job = dr["job"].ToString(),
                            allowance_province = dr["allowance_province"] != DBNull.Value ? Convert.ToDouble(dr["allowance_province"].ToString()) : 0,
                            allowance_1_4 = dr["allowance_1_4"] != DBNull.Value ? Convert.ToDouble(dr["allowance_1_4"].ToString()) : 0,
                            allowance_4_8 = dr["allowance_4_8"] != DBNull.Value ? Convert.ToDouble(dr["allowance_4_8"].ToString()) : 0,
                            allowance_8 = dr["allowance_8"] != DBNull.Value ? Convert.ToDouble(dr["allowance_8"].ToString()) : 0,
                            allowance_other = dr["allowance_other"] != DBNull.Value ? Convert.ToDouble(dr["allowance_other"].ToString()) : 0,
                            allowance_hostel = dr["allowance_hostel"] != DBNull.Value ? Convert.ToDouble(dr["allowance_hostel"].ToString()) : 0,
                            list = dr["list"].ToString(),
                            amount = dr["amount"] != DBNull.Value ? Convert.ToDouble(dr["amount"].ToString()) : 0,
                            description = dr["description"].ToString(),
                            remark = dr["remark"].ToString(),                          
                            status = dr["status"].ToString(),
                            approver = dr["approver"].ToString(),
                            approver_name = dr["approver_name"].ToString(),
                            last_date = dr["last_date"] != DBNull.Value ? Convert.ToDateTime(dr["last_date"].ToString()) : DateTime.MinValue,
                        };
                        allowances.Add(allowance);
                    }
                    dr.Close();
                }
            }
            finally
            {
                connection.Close();
            }
            return allowances;
        }
        public string EditInserts(List<AllowanceModel> allowances)
        {
            SqlConnection conn = ConnectSQL.OpenConnect();
            try
            {
                string string_command = string.Format($@"
                    INSERT INTO 
                        EditAllowance(code,
                            emp_id,
                            zipcode,
                            date,
                            customer,
                            job,
                            allowance_province,
                            allowance_1_4,
                            allowance_4_8,
                            allowance_8,
                            allowance_other,
                            allowance_hostel,
                            list,amount,
                            description,
                            remark,
                            status,
                            approver,
                            last_date
                        )
                        VALUES(@code,
                            @emp_id,
                            @zipcode,
                            @date,
                            @customer,
                            @job,
                            @allowance_province,
                            @allowance_1_4,
                            @allowance_4_8,
                            @allowance_8,
                            @allowance_other,
                            @allowance_hostel,
                            @list,amount,
                            @description,
                            @remark,
                            @status,
                            @approver,
                            @last_date
                        )");
                using (SqlCommand cmd = new SqlCommand(string_command, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@code", SqlDbType.Text);
                    cmd.Parameters.Add("@emp_id", SqlDbType.Text);
                    cmd.Parameters.Add("@zipcode", SqlDbType.Text);
                    cmd.Parameters.Add("@date", SqlDbType.Date);
                    cmd.Parameters.Add("@customer", SqlDbType.Text);
                    cmd.Parameters.Add("@job", SqlDbType.Text);
                    cmd.Parameters.Add("@allowance_province", SqlDbType.Float);
                    cmd.Parameters.Add("@allowance_1_4", SqlDbType.Float);
                    cmd.Parameters.Add("@allowance_4_8", SqlDbType.Float);
                    cmd.Parameters.Add("@allowance_8", SqlDbType.Float);
                    cmd.Parameters.Add("@allowance_other", SqlDbType.Float);
                    cmd.Parameters.Add("@allowance_hostel", SqlDbType.Float);
                    cmd.Parameters.Add("@list", SqlDbType.Text);
                    cmd.Parameters.Add("@amount", SqlDbType.Float);
                    cmd.Parameters.Add("@description", SqlDbType.Text);
                    cmd.Parameters.Add("@remark", SqlDbType.Text);
                    cmd.Parameters.Add("@status", SqlDbType.Text);
                    cmd.Parameters.Add("@approver", SqlDbType.Text);
                    cmd.Parameters.Add("@last_date", SqlDbType.Text);

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Open();
                    }
                    for (int i = 0; i < allowances.Count; i++)
                    {
                        cmd.Parameters[0].Value = allowances[i].code;
                        cmd.Parameters[1].Value = allowances[i].emp_id;
                        cmd.Parameters[2].Value = allowances[i].zipcode;
                        cmd.Parameters[3].Value = allowances[i].date;
                        cmd.Parameters[4].Value = allowances[i].customer;
                        cmd.Parameters[5].Value = allowances[i].job;
                        cmd.Parameters[6].Value = allowances[i].allowance_province;
                        cmd.Parameters[7].Value = allowances[i].allowance_1_4;
                        cmd.Parameters[8].Value = allowances[i].allowance_4_8;
                        cmd.Parameters[9].Value = allowances[i].allowance_8;
                        cmd.Parameters[10].Value = allowances[i].allowance_other;
                        cmd.Parameters[11].Value = allowances[i].allowance_hostel;
                        cmd.Parameters[12].Value = allowances[i].list;
                        cmd.Parameters[13].Value = allowances[i].amount;
                        cmd.Parameters[14].Value = allowances[i].description;
                        cmd.Parameters[15].Value = allowances[i].remark;
                        cmd.Parameters[16].Value = allowances[i].status;
                        cmd.Parameters[17].Value = allowances[i].approver;
                        cmd.Parameters[18].Value = allowances[i].last_date;
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
        public string OriginalInserts(List<AllowanceModel> allowances)
        {
            SqlConnection conn = ConnectSQL.OpenConnect();
            try
            {
                string string_command = string.Format($@"
                    INSERT INTO 
                        OriginalAllowance(code,
                            emp_id,
                            zipcode,
                            date,
                            customer,
                            job,
                            allowance_province,
                            allowance_1_4,
                            allowance_4_8,
                            allowance_8,
                            allowance_other,
                            allowance_hostel,
                            list,amount,
                            description,
                            remark,
                            status,
                            approver,
                            last_date
                        )
                        VALUES(@code,
                            @emp_id,
                            @zipcode,
                            @date,
                            @customer,
                            @job,
                            @allowance_province,
                            @allowance_1_4,
                            @allowance_4_8,
                            @allowance_8,
                            @allowance_other,
                            @allowance_hostel,
                            @list,amount,
                            @description,
                            @remark,
                            @status,
                            @approver,
                            @last_date
                        )");
                using (SqlCommand cmd = new SqlCommand(string_command, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@code", SqlDbType.Text);
                    cmd.Parameters.Add("@emp_id", SqlDbType.Text);
                    cmd.Parameters.Add("@zipcode", SqlDbType.Text);
                    cmd.Parameters.Add("@date", SqlDbType.Date);
                    cmd.Parameters.Add("@customer", SqlDbType.Text);
                    cmd.Parameters.Add("@job", SqlDbType.Text);
                    cmd.Parameters.Add("@allowance_province", SqlDbType.Float);
                    cmd.Parameters.Add("@allowance_1_4", SqlDbType.Float);
                    cmd.Parameters.Add("@allowance_4_8", SqlDbType.Float);
                    cmd.Parameters.Add("@allowance_8", SqlDbType.Float);
                    cmd.Parameters.Add("@allowance_other", SqlDbType.Float);
                    cmd.Parameters.Add("@allowance_hostel", SqlDbType.Float);
                    cmd.Parameters.Add("@list", SqlDbType.Text);
                    cmd.Parameters.Add("@amount", SqlDbType.Float);
                    cmd.Parameters.Add("@description", SqlDbType.Text);
                    cmd.Parameters.Add("@remark", SqlDbType.Text);
                    cmd.Parameters.Add("@status", SqlDbType.Text);
                    cmd.Parameters.Add("@approver", SqlDbType.Text);
                    cmd.Parameters.Add("@last_date", SqlDbType.Text);

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Open();
                    }
                    for (int i = 0; i < allowances.Count; i++)
                    {
                        cmd.Parameters[0].Value = allowances[i].code;
                        cmd.Parameters[1].Value = allowances[i].emp_id;
                        cmd.Parameters[2].Value = allowances[i].zipcode;
                        cmd.Parameters[3].Value = allowances[i].date;
                        cmd.Parameters[4].Value = allowances[i].customer;
                        cmd.Parameters[5].Value = allowances[i].job;
                        cmd.Parameters[6].Value = allowances[i].allowance_province;
                        cmd.Parameters[7].Value = allowances[i].allowance_1_4;
                        cmd.Parameters[8].Value = allowances[i].allowance_4_8;
                        cmd.Parameters[9].Value = allowances[i].allowance_8;
                        cmd.Parameters[10].Value = allowances[i].allowance_other;
                        cmd.Parameters[11].Value = allowances[i].allowance_hostel;
                        cmd.Parameters[12].Value = allowances[i].list;
                        cmd.Parameters[13].Value = allowances[i].amount;
                        cmd.Parameters[14].Value = allowances[i].description;
                        cmd.Parameters[15].Value = allowances[i].remark;
                        cmd.Parameters[16].Value = allowances[i].status;
                        cmd.Parameters[17].Value = allowances[i].approver;
                        cmd.Parameters[18].Value = allowances[i].last_date;
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

        public string UpdateByCode(AllowanceModel allowance)
        {
            SqlConnection conn = ConnectSQL.OpenConnect();
            try
            {
                string string_command = string.Format($@"
                    UPDATE 
                        EditAllowance SET
                        emp_id = @emp_id,
                        zipcode = @zipcode,
	                    date = @date,
	                    customer = @customer,
	                    job = @job,
	                    allowance_province = @allowance_province,
	                    allowance_1_4 = @allowance_1_4,
	                    allowance_4_8 = @allowance_4_8,
	                    allowance_8 = @allowance_8,
	                    allowance_other = @allowance_other,
	                    allowance_hostel = @allowance_hostel,
	                    list = @list,
	                    amount = @amount,
	                    description = @description,
	                    remark = @remark,
	                    status = @status,
						approver = @approver,
						last_date = @last_date                                                     
                        WHERE code = @code");
                using (SqlCommand cmd = new SqlCommand(string_command, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@code", allowance.code);
                    cmd.Parameters.AddWithValue("@emp_id", allowance.emp_id);
                    cmd.Parameters.AddWithValue("@zipcode", allowance.zipcode);
                    cmd.Parameters.AddWithValue("@date", allowance.date);
                    cmd.Parameters.AddWithValue("@customer", allowance.customer);
                    cmd.Parameters.AddWithValue("@job", allowance.job);
                    cmd.Parameters.AddWithValue("@allowance_province", allowance.allowance_province);
                    cmd.Parameters.AddWithValue("@allowance_1_4", allowance.allowance_1_4);
                    cmd.Parameters.AddWithValue("@allowance_4_8", allowance.allowance_4_8);
                    cmd.Parameters.AddWithValue("@allowance_8", allowance.allowance_8);
                    cmd.Parameters.AddWithValue("@allowance_other", allowance.allowance_other);
                    cmd.Parameters.AddWithValue("@allowance_hostel", allowance.allowance_hostel);
                    cmd.Parameters.AddWithValue("@list", allowance.list);
                    cmd.Parameters.AddWithValue("@amount", allowance.amount);
                    cmd.Parameters.AddWithValue("@description", allowance.description);
                    cmd.Parameters.AddWithValue("@remark", allowance.remark);
                    cmd.Parameters.AddWithValue("@status", allowance.status);
                    cmd.Parameters.AddWithValue("@approver", allowance.approver);
                    cmd.Parameters.AddWithValue("@last_date", allowance.last_date);
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
