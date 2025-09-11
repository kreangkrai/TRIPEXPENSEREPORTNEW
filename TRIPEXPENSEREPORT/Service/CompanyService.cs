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

        public List<CompanyViewModel> GetEditCompaniesByDate(DateTime start_date, DateTime stop_date)
        {
            List<CompanyViewModel> companies = new List<CompanyViewModel>();
            SqlConnection connection = ConnectSQL.OpenConnect();
            try
            {
                string strCmd = string.Format($@"SELECT code,
	                                            EditCompany.driver,
                                                emp1.name as driver_name,
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
												EditCompany.approver,
												emp2.name as approver_name,
												last_date
                                                FROM EditCompany
                                                LEFT JOIN Employees emp1 ON EditCompany.driver = emp1.emp_id
												LEFT JOIN Employees emp2 ON EditCompany.approver = emp2.emp_id
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
                        CompanyViewModel company = new CompanyViewModel()
                        {
                            code = dr["code"].ToString(),
                            driver = dr["driver"].ToString(),
                            driver_name = dr["driver_name"].ToString(),
                            date = dr["date"] != DBNull.Value ? Convert.ToDateTime(dr["date"].ToString()) : DateTime.MinValue,
                            car_id = dr["car_id"].ToString(),
                            time_start = dr["time_start"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_start"].ToString()).Ticks) : TimeSpan.Zero,
                            time_stop = dr["time_stop"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_stop"].ToString()).Ticks) : TimeSpan.Zero,
                            location = dr["location"].ToString(),
                            zipcode = dr["zipcode"].ToString(),
                            job = dr["job"].ToString(),
                            fleet = dr["fleet"] != DBNull.Value ? Convert.ToDouble(dr["fleet"].ToString()) : 0,
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
                            approver = dr["approver"].ToString(),
                            approver_name = dr["approver_name"].ToString(),
                            last_date = dr["last_date"] != DBNull.Value ? Convert.ToDateTime(dr["last_date"].ToString()) : DateTime.MinValue,
                        };
                        companies.Add(company);
                    }
                    dr.Close();
                }
            }
            finally
            {
                connection.Close();
            }
            return companies;
        }

        public List<CompanyViewModel> GetOriginalCompaniesByDate(DateTime start_date, DateTime stop_date)
        {
            List<CompanyViewModel> companies = new List<CompanyViewModel>();
            SqlConnection connection = ConnectSQL.OpenConnect();
            try
            {
                string strCmd = string.Format($@"SELECT code,
	                                            OriginalCompany.driver,
                                                emp1.name as driver_name,
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
												OriginalCompany.approver,
												emp2.name as approver_name,
												last_date
                                                FROM OriginalCompany
                                                LEFT JOIN Employees emp1 ON OriginalCompany.driver = emp1.emp_id
												LEFT JOIN Employees emp2 ON OriginalCompany.approver = emp2.emp_id
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
                        CompanyViewModel company = new CompanyViewModel()
                        {
                            code = dr["code"].ToString(),
                            driver = dr["driver"].ToString(),
                            driver_name = dr["driver_name"].ToString(),
                            date = dr["date"] != DBNull.Value ? Convert.ToDateTime(dr["date"].ToString()) : DateTime.MinValue,
                            car_id = dr["car_id"].ToString(),
                            time_start = dr["time_start"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_start"].ToString()).Ticks) : TimeSpan.Zero,
                            time_stop = dr["time_stop"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_stop"].ToString()).Ticks) : TimeSpan.Zero,
                            location = dr["location"].ToString(),
                            zipcode = dr["zipcode"].ToString(),
                            job = dr["job"].ToString(),
                            fleet = dr["fleet"] != DBNull.Value ? Convert.ToDouble(dr["fleet"].ToString()) : 0,
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
                            approver = dr["approver"].ToString(),
                            approver_name = dr["approver_name"].ToString(),
                            last_date = dr["last_date"] != DBNull.Value ? Convert.ToDateTime(dr["last_date"].ToString()) : DateTime.MinValue,
                        };
                        companies.Add(company);
                    }
                    dr.Close();
                }
            }
            finally
            {
                connection.Close();
            }
            return companies;
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

        public string UpdateByCode(CompanyModel company)
        {
            SqlConnection conn = ConnectSQL.OpenConnect();
            try
            {
                string string_command = string.Format($@"
                    UPDATE 
                        EditCompany SET
                        driver = @driver,
                        date = @date,
                        car_id = @car_id,
	                    time_start = @time_start,
	                    time_stop = @time_stop,
                        location = @location,
                        zipcode = @zipcode,
                        job = @job,
                        fleet = @fleet,
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
						approver = @approver,
						last_date  = @last_date   	                                                          
                        WHERE code = @code");
                using (SqlCommand cmd = new SqlCommand(string_command, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@code", company.code);
                    cmd.Parameters.AddWithValue("@driver", company.driver);
                    cmd.Parameters.AddWithValue("@date", company.date);
                    cmd.Parameters.AddWithValue("@car_id", company.car_id);
                    cmd.Parameters.AddWithValue("@time_start", company.time_start);
                    cmd.Parameters.AddWithValue("@time_stop", company.time_stop);
                    cmd.Parameters.AddWithValue("@location", company.location);
                    cmd.Parameters.AddWithValue("@zipcode", company.zipcode);
                    cmd.Parameters.AddWithValue("@job", company.job);
                    cmd.Parameters.AddWithValue("@fleet", company.fleet);
                    cmd.Parameters.AddWithValue("@cash", company.cash);
                    cmd.Parameters.AddWithValue("@ctbo", company.ctbo);
                    cmd.Parameters.AddWithValue("@exp", company.exp);
                    cmd.Parameters.AddWithValue("@pt", company.pt);
                    cmd.Parameters.AddWithValue("@mileage_start", company.mileage_start);
                    cmd.Parameters.AddWithValue("@mileage_stop", company.mileage_stop);
                    cmd.Parameters.AddWithValue("@km", company.km);
                    cmd.Parameters.AddWithValue("@program_km", company.program_km);
                    cmd.Parameters.AddWithValue("@auto_km", company.auto_km);
                    cmd.Parameters.AddWithValue("@description", company.description);
                    cmd.Parameters.AddWithValue("@status", company.status);
                    cmd.Parameters.AddWithValue("@approver", company.approver);
                    cmd.Parameters.AddWithValue("@last_date", company.last_date);
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
