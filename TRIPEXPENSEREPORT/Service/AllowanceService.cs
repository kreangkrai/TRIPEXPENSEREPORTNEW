using Microsoft.Data.SqlClient;
using System.Data;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Service
{
    public class AllowanceService : IAllowance
    {
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
    }
}
