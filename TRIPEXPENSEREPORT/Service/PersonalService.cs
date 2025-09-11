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

        public string UpdateByCode(PersonalModel personal)
        {
            SqlConnection conn = ConnectSQL.OpenConnect();
            try
            {
                string string_command = string.Format($@"
                    UPDATE 
                        EditPersonal SET
                        driver = @driver,
                        date = @date,
	                    time_start = @time_start,
	                    time_stop = @time_stop,
                        location = @location,
                        zipcode = @zipcode,
                        job = @job,
						cash = @cash,
                        ctbo = @ctbo,
						exp = @exp,
						pt = @pt,
						mileage_start = @mileage_start,
						mileage_stop = @mileage_stop,
						km = @km,
						program_km = @program_km,
						auto_km = @auto_km,
						description = @description,
						status = @status,
						gasoline = @gasoline,
						approver = @approver,
						last_date  = @last_date   	                                                          
                        WHERE code = @code");
                using (SqlCommand cmd = new SqlCommand(string_command, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@code", personal.code);
                    cmd.Parameters.AddWithValue("@driver", personal.driver);
                    cmd.Parameters.AddWithValue("@date", personal.date);
                    cmd.Parameters.AddWithValue("@time_start", personal.time_start);
                    cmd.Parameters.AddWithValue("@time_stop", personal.time_stop);
                    cmd.Parameters.AddWithValue("@location", personal.location);
                    cmd.Parameters.AddWithValue("@zipcode", personal.zipcode);
                    cmd.Parameters.AddWithValue("@job", personal.job);
                    cmd.Parameters.AddWithValue("@cash", personal.cash);
                    cmd.Parameters.AddWithValue("@ctbo", personal.ctbo);
                    cmd.Parameters.AddWithValue("@exp", personal.exp);
                    cmd.Parameters.AddWithValue("@pt", personal.pt);
                    cmd.Parameters.AddWithValue("@mileage_start", personal.mileage_start);
                    cmd.Parameters.AddWithValue("@mileage_stop", personal.mileage_stop);
                    cmd.Parameters.AddWithValue("@km", personal.km);
                    cmd.Parameters.AddWithValue("@program_km", personal.program_km);
                    cmd.Parameters.AddWithValue("@auto_km", personal.auto_km);
                    cmd.Parameters.AddWithValue("@description", personal.description);
                    cmd.Parameters.AddWithValue("@status", personal.status);
                    cmd.Parameters.AddWithValue("@gasoline", personal.gasoline);
                    cmd.Parameters.AddWithValue("@approver", personal.approver);
                    cmd.Parameters.AddWithValue("@last_date", personal.last_date);
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

        public List<PersonalViewModel> GetOriginalPersonalsByDate(DateTime start_date, DateTime stop_date)
        {
            List<PersonalViewModel> personals = new List<PersonalViewModel>();
            SqlConnection connection = ConnectSQL.OpenConnect();
            try
            {
                string strCmd = string.Format($@"SELECT code,
	                                            OriginalPersonal.driver,
                                                emp1.name as driver_name,
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
												OriginalPersonal.approver,
												emp2.name as approver_name,
												last_date
                                                FROM OriginalPersonal
                                                LEFT JOIN Employees emp1 ON OriginalPersonal.driver = emp1.emp_id
												LEFT JOIN Employees emp2 ON OriginalPersonal.approver = emp2.emp_id
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
                        PersonalViewModel personal = new PersonalViewModel()
                        {
                            code = dr["code"].ToString(),
                            driver = dr["driver"].ToString(),
                            driver_name = dr["driver_name"].ToString(),
                            date = dr["date"] != DBNull.Value ? Convert.ToDateTime(dr["date"].ToString()) : DateTime.MinValue,
                            time_start = dr["time_start"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_start"].ToString()).Ticks) : TimeSpan.Zero,
                            time_stop = dr["time_stop"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_stop"].ToString()).Ticks) : TimeSpan.Zero,
                            location = dr["location"].ToString(),
                            zipcode = dr["zipcode"].ToString(),
                            job = dr["job"].ToString(),
                            cash = dr["cash"] != DBNull.Value ? Convert.ToDouble(dr["cash"].ToString()) : 0,
                            ctbo = dr["ctbo"] != DBNull.Value ? Convert.ToDouble(dr["ctbo"].ToString()) : 0,
                            exp = dr["exp"] != DBNull.Value ? Convert.ToDouble(dr["exp"].ToString()) : 0,
                            pt = dr["pt"] != DBNull.Value ? Convert.ToDouble(dr["pt"].ToString()) : 0,
                            mileage_start = dr["mileage_start"] != DBNull.Value ? Convert.ToInt32(dr["mileage_start"].ToString()) : 0,
                            mileage_stop = dr["mileage_stop"] != DBNull.Value ? Convert.ToInt32(dr["mileage_stop"].ToString()) : 0,
                            km = dr["km"] != DBNull.Value ? Convert.ToInt32(dr["km"].ToString()) : 0,
                            program_km = dr["program_km"] != DBNull.Value ? Convert.ToInt32(dr["program_km"].ToString()) : 0,
                            auto_km = dr["auto_km"] != DBNull.Value ? Convert.ToInt32(dr["auto_km"].ToString()) : 0,
                            description = dr["description"].ToString(),
                            status = dr["status"].ToString(),
                            gasoline = dr["gasoline"].ToString(),
                            approver = dr["approver"].ToString(),
                            approver_name = dr["approver_name"].ToString(),
                            last_date = dr["last_date"] != DBNull.Value ? Convert.ToDateTime(dr["last_date"].ToString()) : DateTime.MinValue,
                        };
                        personals.Add(personal);
                    }
                    dr.Close();
                }
            }
            finally
            {
                connection.Close();
            }
            return personals;
        }

        public List<PersonalViewModel> GetEditPersonalsByDate(DateTime start_date, DateTime stop_date)
        {
            List<PersonalViewModel> personals = new List<PersonalViewModel>();
            SqlConnection connection = ConnectSQL.OpenConnect();
            try
            {
                string strCmd = string.Format($@"SELECT code,
	                                            EditPersonal.driver,
                                                emp1.name as driver_name,
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
												EditPersonal.approver,
												emp2.name as approver_name,
												last_date
                                                FROM EditPersonal
                                                LEFT JOIN Employees emp1 ON EditPersonal.driver = emp1.emp_id
												LEFT JOIN Employees emp2 ON EditPersonal.approver = emp2.emp_id
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
                        PersonalViewModel personal = new PersonalViewModel()
                        {
                            code = dr["code"].ToString(),
                            driver = dr["driver"].ToString(),
                            driver_name = dr["driver_name"].ToString(),
                            date = dr["date"] != DBNull.Value ? Convert.ToDateTime(dr["date"].ToString()) : DateTime.MinValue,
                            time_start = dr["time_start"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_start"].ToString()).Ticks) : TimeSpan.Zero,
                            time_stop = dr["time_stop"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_stop"].ToString()).Ticks) : TimeSpan.Zero,
                            location = dr["location"].ToString(),
                            zipcode = dr["zipcode"].ToString(),
                            job = dr["job"].ToString(),
                            cash = dr["cash"] != DBNull.Value ? Convert.ToDouble(dr["cash"].ToString()) : 0,
                            ctbo = dr["ctbo"] != DBNull.Value ? Convert.ToDouble(dr["ctbo"].ToString()) : 0,
                            exp = dr["exp"] != DBNull.Value ? Convert.ToDouble(dr["exp"].ToString()) : 0,
                            pt = dr["pt"] != DBNull.Value ? Convert.ToDouble(dr["pt"].ToString()) : 0,
                            mileage_start = dr["mileage_start"] != DBNull.Value ? Convert.ToInt32(dr["mileage_start"].ToString()) : 0,
                            mileage_stop = dr["mileage_stop"] != DBNull.Value ? Convert.ToInt32(dr["mileage_stop"].ToString()) : 0,
                            km = dr["km"] != DBNull.Value ? Convert.ToInt32(dr["km"].ToString()) : 0,
                            program_km = dr["program_km"] != DBNull.Value ? Convert.ToInt32(dr["program_km"].ToString()) : 0,
                            auto_km = dr["auto_km"] != DBNull.Value ? Convert.ToInt32(dr["auto_km"].ToString()) : 0,
                            description = dr["description"].ToString(),
                            status = dr["status"].ToString(),
                            gasoline = dr["gasoline"].ToString(),
                            approver = dr["approver"].ToString(),
                            approver_name = dr["approver_name"].ToString(),
                            last_date = dr["last_date"] != DBNull.Value ? Convert.ToDateTime(dr["last_date"].ToString()) : DateTime.MinValue,
                        };
                        personals.Add(personal);
                    }
                    dr.Close();
                }
            }
            finally
            {
                connection.Close();
            }
            return personals;
        }
    }
}
