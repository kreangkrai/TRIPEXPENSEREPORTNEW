using Microsoft.Data.SqlClient;
using System.Data;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Service
{
    public class PersonalService : IPersonal
    {
        public string OriginalInserts(List<PersonalModel> personals)
        {
            SqlConnection conn = ConnectSQL.OpenConnect();
            try
            {
                string string_command = string.Format($@"
                    INSERT INTO 
                        OriginalPersonal(code,
                            driver,
                            date,
                            time_start,
                            time_stop,
                            location,
                            zipcode,
                            job,
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
                            gasoline,
                            approver,
                            last_date
                        )
                        VALUES(@code,
                            @driver,
                            @date,
                            @time_start,
                            @time_stop,
                            @location,
                            @zipcode,
                            @job,
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
                            @gasoline,
                            @approver,
                            @last_date
                        )");
                using (SqlCommand cmd = new SqlCommand(string_command, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@code", SqlDbType.Text);
                    cmd.Parameters.Add("@driver", SqlDbType.Text);
                    cmd.Parameters.Add("@date", SqlDbType.Date);
                    cmd.Parameters.Add("@time_start", SqlDbType.Time);
                    cmd.Parameters.Add("@time_stop", SqlDbType.Time);
                    cmd.Parameters.Add("@location", SqlDbType.Text);
                    cmd.Parameters.Add("@zipcode", SqlDbType.Text);
                    cmd.Parameters.Add("@job", SqlDbType.Text);
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
                    cmd.Parameters.Add("@gasoline", SqlDbType.Text);
                    cmd.Parameters.Add("@approver", SqlDbType.Text);
                    cmd.Parameters.Add("@last_date", SqlDbType.DateTime);

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Open();
                    }
                    for (int i = 0; i < personals.Count; i++)
                    {
                        cmd.Parameters[0].Value = personals[i].code;
                        cmd.Parameters[1].Value = personals[i].driver;
                        cmd.Parameters[2].Value = personals[i].date;
                        cmd.Parameters[3].Value = personals[i].time_start;
                        cmd.Parameters[4].Value = personals[i].time_stop;
                        cmd.Parameters[5].Value = personals[i].location;
                        cmd.Parameters[6].Value = personals[i].zipcode;
                        cmd.Parameters[7].Value = personals[i].job;
                        cmd.Parameters[8].Value = personals[i].cash;
                        cmd.Parameters[9].Value = personals[i].ctbo;
                        cmd.Parameters[10].Value = personals[i].exp;
                        cmd.Parameters[11].Value = personals[i].pt;
                        cmd.Parameters[12].Value = personals[i].mileage_start;
                        cmd.Parameters[13].Value = personals[i].mileage_stop;
                        cmd.Parameters[14].Value = personals[i].km;
                        cmd.Parameters[15].Value = personals[i].program_km;
                        cmd.Parameters[16].Value = personals[i].auto_km;
                        cmd.Parameters[17].Value = personals[i].description;
                        cmd.Parameters[18].Value = personals[i].status;
                        cmd.Parameters[19].Value = personals[i].gasoline;
                        cmd.Parameters[20].Value = personals[i].approver;
                        cmd.Parameters[21].Value = personals[i].last_date;
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

        public string EditInserts(List<PersonalModel> personals)
        {
            SqlConnection conn = ConnectSQL.OpenConnect();
            try
            {
                string string_command = string.Format($@"
                    INSERT INTO 
                        EditPersonal(code,
                            driver,
                            date,
                            time_start,
                            time_stop,
                            location,
                            zipcode,
                            job,
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
                            gasoline,
                            approver,
                            last_date
                        )
                        VALUES(@code,
                            @driver,
                            @date,
                            @time_start,
                            @time_stop,
                            @location,
                            @zipcode,
                            @job,
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
                            @gasoline,
                            @approver,
                            @last_date
                        )");
                using (SqlCommand cmd = new SqlCommand(string_command, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@code", SqlDbType.Text);
                    cmd.Parameters.Add("@driver", SqlDbType.Text);
                    cmd.Parameters.Add("@date", SqlDbType.Date);
                    cmd.Parameters.Add("@time_start", SqlDbType.Time);
                    cmd.Parameters.Add("@time_stop", SqlDbType.Time);
                    cmd.Parameters.Add("@location", SqlDbType.Text);
                    cmd.Parameters.Add("@zipcode", SqlDbType.Text);
                    cmd.Parameters.Add("@job", SqlDbType.Text);
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
                    cmd.Parameters.Add("@gasoline", SqlDbType.Text);
                    cmd.Parameters.Add("@approver", SqlDbType.Text);
                    cmd.Parameters.Add("@last_date", SqlDbType.DateTime);

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Open();
                    }
                    for (int i = 0; i < personals.Count; i++)
                    {
                        cmd.Parameters[0].Value = personals[i].code;
                        cmd.Parameters[1].Value = personals[i].driver;
                        cmd.Parameters[2].Value = personals[i].date;
                        cmd.Parameters[3].Value = personals[i].time_start;
                        cmd.Parameters[4].Value = personals[i].time_stop;
                        cmd.Parameters[5].Value = personals[i].location;
                        cmd.Parameters[6].Value = personals[i].zipcode;
                        cmd.Parameters[7].Value = personals[i].job;
                        cmd.Parameters[8].Value = personals[i].cash;
                        cmd.Parameters[9].Value = personals[i].ctbo;
                        cmd.Parameters[10].Value = personals[i].exp;
                        cmd.Parameters[11].Value = personals[i].pt;
                        cmd.Parameters[12].Value = personals[i].mileage_start;
                        cmd.Parameters[13].Value = personals[i].mileage_stop;
                        cmd.Parameters[14].Value = personals[i].km;
                        cmd.Parameters[15].Value = personals[i].program_km;
                        cmd.Parameters[16].Value = personals[i].auto_km;
                        cmd.Parameters[17].Value = personals[i].description;
                        cmd.Parameters[18].Value = personals[i].status;
                        cmd.Parameters[19].Value = personals[i].gasoline;
                        cmd.Parameters[20].Value = personals[i].approver;
                        cmd.Parameters[21].Value = personals[i].last_date;
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
