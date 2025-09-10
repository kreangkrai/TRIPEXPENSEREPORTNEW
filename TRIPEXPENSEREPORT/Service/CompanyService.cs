using Microsoft.Data.SqlClient;
using System.Data;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Service
{
    public class CompanyService : ICompany
    {
        public string EditInserts(List<CompanyModel> companies)
        {
            SqlConnection conn = ConnectSQL.OpenConnect();
            try
            {
                string string_command = string.Format($@"
                    INSERT INTO 
                        EditCompany(code,
                            driver,
                            date,
                            car_id,
                            time_start,
                            time_stop,
                            location,
                            zipcode,
                            job,
                            fleet,
                            cash,
                            ctbo,
                            exp,
                            pt,
                            mileage_start,
                            mileage_stop,
                            km,
                            program_km,
                            auto_km,
                            description,
                            status,
                            approver,
                            last_date
                        )
                        VALUES(@code,
                            @driver,
                            @date,
                            @car_id,
                            @time_start,
                            @time_stop,
                            @location,
                            @zipcode,
                            @job,
                            @fleet,
                            @cash,
                            @ctbo,
                            @exp,
                            @pt,
                            @mileage_start,
                            @mileage_stop,
                            @km,
                            @program_km,
                            @auto_km,
                            @description,
                            @status,
                            @approver,
                            @last_date
                        )");
                using (SqlCommand cmd = new SqlCommand(string_command, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@code", SqlDbType.Text);
                    cmd.Parameters.Add("@driver", SqlDbType.Text);
                    cmd.Parameters.Add("@date", SqlDbType.Date);
                    cmd.Parameters.Add("@car_id", SqlDbType.Text);
                    cmd.Parameters.Add("@time_start", SqlDbType.Time);
                    cmd.Parameters.Add("@time_stop", SqlDbType.Time);
                    cmd.Parameters.Add("@location", SqlDbType.Text);
                    cmd.Parameters.Add("@zipcode", SqlDbType.Text);
                    cmd.Parameters.Add("@job", SqlDbType.Text);
                    cmd.Parameters.Add("@fleet", SqlDbType.Float);
                    cmd.Parameters.Add("@cash", SqlDbType.Float);
                    cmd.Parameters.Add("@ctbo", SqlDbType.Float);
                    cmd.Parameters.Add("@exp", SqlDbType.Float);
                    cmd.Parameters.Add("@pt", SqlDbType.Float);
                    cmd.Parameters.Add("@mileage_start", SqlDbType.Int);
                    cmd.Parameters.Add("@mileage_stop", SqlDbType.Int);
                    cmd.Parameters.Add("@km", SqlDbType.Int);
                    cmd.Parameters.Add("@program_km", SqlDbType.Int);
                    cmd.Parameters.Add("@auto_km", SqlDbType.Int);
                    cmd.Parameters.Add("@description", SqlDbType.Text);
                    cmd.Parameters.Add("@status", SqlDbType.Text);
                    cmd.Parameters.Add("@approver", SqlDbType.Text);
                    cmd.Parameters.Add("@last_date", SqlDbType.DateTime);

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Open();
                    }
                    for (int i = 0; i < companies.Count; i++)
                    {
                        cmd.Parameters[0].Value = companies[i].code;
                        cmd.Parameters[1].Value = companies[i].driver;
                        cmd.Parameters[2].Value = companies[i].date;
                        cmd.Parameters[3].Value = companies[i].car_id;
                        cmd.Parameters[4].Value = companies[i].time_start;
                        cmd.Parameters[5].Value = companies[i].time_stop;
                        cmd.Parameters[6].Value = companies[i].location;
                        cmd.Parameters[7].Value = companies[i].zipcode;
                        cmd.Parameters[8].Value = companies[i].job;
                        cmd.Parameters[9].Value = companies[i].fleet;
                        cmd.Parameters[10].Value = companies[i].cash;
                        cmd.Parameters[11].Value = companies[i].ctbo;
                        cmd.Parameters[12].Value = companies[i].exp;
                        cmd.Parameters[13].Value = companies[i].pt;
                        cmd.Parameters[14].Value = companies[i].mileage_start;
                        cmd.Parameters[15].Value = companies[i].mileage_stop;
                        cmd.Parameters[16].Value = companies[i].km;
                        cmd.Parameters[17].Value = companies[i].program_km;
                        cmd.Parameters[18].Value = companies[i].auto_km;
                        cmd.Parameters[19].Value = companies[i].description;
                        cmd.Parameters[20].Value = companies[i].status;
                        cmd.Parameters[21].Value = companies[i].approver;
                        cmd.Parameters[22].Value = companies[i].last_date;
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

        public string OriginalInserts(List<CompanyModel> companies)
        {
            SqlConnection conn = ConnectSQL.OpenConnect();
            try
            {
                string string_command = string.Format($@"
                    INSERT INTO 
                        OriginalCompany(code,
                            driver,
                            date,
                            car_id,
                            time_start,
                            time_stop,
                            location,
                            zipcode,
                            job,
                            fleet,
                            cash,
                            ctbo,
                            exp,
                            pt,
                            mileage_start,
                            mileage_stop,
                            km,
                            program_km,
                            auto_km,
                            description,
                            status,
                            approver,
                            last_date
                        )
                        VALUES(@code,
                            @driver,
                            @date,
                            @car_id,
                            @time_start,
                            @time_stop,
                            @location,
                            @zipcode,
                            @job,
                            @fleet,
                            @cash,
                            @ctbo,
                            @exp,
                            @pt,
                            @mileage_start,
                            @mileage_stop,
                            @km,
                            @program_km,
                            @auto_km,
                            @description,
                            @status,
                            @approver,
                            @last_date
                        )");
                using (SqlCommand cmd = new SqlCommand(string_command, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@code", SqlDbType.Text);
                    cmd.Parameters.Add("@driver", SqlDbType.Text);
                    cmd.Parameters.Add("@date", SqlDbType.Date);
                    cmd.Parameters.Add("@car_id", SqlDbType.Text);
                    cmd.Parameters.Add("@time_start", SqlDbType.Time);
                    cmd.Parameters.Add("@time_stop", SqlDbType.Time);
                    cmd.Parameters.Add("@location", SqlDbType.Text);
                    cmd.Parameters.Add("@zipcode", SqlDbType.Text);
                    cmd.Parameters.Add("@job", SqlDbType.Text);
                    cmd.Parameters.Add("@fleet", SqlDbType.Float);
                    cmd.Parameters.Add("@cash", SqlDbType.Float);
                    cmd.Parameters.Add("@ctbo", SqlDbType.Float);
                    cmd.Parameters.Add("@exp", SqlDbType.Float);
                    cmd.Parameters.Add("@pt", SqlDbType.Float);
                    cmd.Parameters.Add("@mileage_start", SqlDbType.Int);
                    cmd.Parameters.Add("@mileage_stop", SqlDbType.Int);
                    cmd.Parameters.Add("@km", SqlDbType.Int);
                    cmd.Parameters.Add("@program_km", SqlDbType.Int);
                    cmd.Parameters.Add("@auto_km", SqlDbType.Int);
                    cmd.Parameters.Add("@description", SqlDbType.Text);
                    cmd.Parameters.Add("@status", SqlDbType.Text);
                    cmd.Parameters.Add("@approver", SqlDbType.Text);
                    cmd.Parameters.Add("@last_date", SqlDbType.DateTime);

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Open();
                    }
                    for (int i = 0; i < companies.Count; i++)
                    {
                        cmd.Parameters[0].Value = companies[i].code;
                        cmd.Parameters[1].Value = companies[i].driver;
                        cmd.Parameters[2].Value = companies[i].date;
                        cmd.Parameters[3].Value = companies[i].car_id;
                        cmd.Parameters[4].Value = companies[i].time_start;
                        cmd.Parameters[5].Value = companies[i].time_stop;
                        cmd.Parameters[6].Value = companies[i].location;
                        cmd.Parameters[7].Value = companies[i].zipcode;
                        cmd.Parameters[8].Value = companies[i].job;
                        cmd.Parameters[9].Value = companies[i].fleet;
                        cmd.Parameters[10].Value = companies[i].cash;
                        cmd.Parameters[11].Value = companies[i].ctbo;
                        cmd.Parameters[12].Value = companies[i].exp;
                        cmd.Parameters[13].Value = companies[i].pt;
                        cmd.Parameters[14].Value = companies[i].mileage_start;
                        cmd.Parameters[15].Value = companies[i].mileage_stop;
                        cmd.Parameters[16].Value = companies[i].km;
                        cmd.Parameters[17].Value = companies[i].program_km;
                        cmd.Parameters[18].Value = companies[i].auto_km;
                        cmd.Parameters[19].Value = companies[i].description;
                        cmd.Parameters[20].Value = companies[i].status;
                        cmd.Parameters[21].Value = companies[i].approver;
                        cmd.Parameters[22].Value = companies[i].last_date;
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
