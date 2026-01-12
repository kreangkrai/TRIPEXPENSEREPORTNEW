using Microsoft.Data.SqlClient;
using System.Data;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Service
{
    public class CompanyService : ICompany
    {
        ConnectSQL connect = null;
        SqlConnection con = null;
        ConnectSQL connect_report = null;
        SqlConnection con_report = null;
        public CompanyService()
        {
            connect = new ConnectSQL();
            con = connect.OpenConnect();

            connect_report = new ConnectSQL();
            con_report = connect_report.OpenReportConnect();
        }

        public List<CompanyModel> GetCompaniesByDate(DateTime start_date, DateTime stop_date)
        {
            List<CompanyModel> companies = new List<CompanyModel>();
            try
            {
                if (con_report.State == ConnectionState.Closed)
                {
                    con_report.Open();
                }
                string strCmd = string.Format($@"SELECT code,
	                                            driver,
	                                            date,
	                                            car_id,
	                                            time_start,
	                                            time_stop,
                                                location,
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
                                                FROM EditCompany
                                                WHERE date >= @start_date AND date <= @stop_date");
                SqlCommand command = new SqlCommand(strCmd, con_report);
                command.Parameters.AddWithValue("@start_date", start_date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@stop_date", stop_date.ToString("yyyy-MM-dd"));
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CompanyModel company = new CompanyModel()
                        {
                            code = dr["code"].ToString(),
                            driver = dr["driver"].ToString(),
                            date = dr["date"] != DBNull.Value ? Convert.ToDateTime(dr["date"].ToString()) : DateTime.MinValue,
                            car_id = dr["car_id"].ToString(),
                            time_start = dr["time_start"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_start"].ToString()).Ticks) : TimeSpan.Zero,
                            time_stop = dr["time_stop"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_stop"].ToString()).Ticks) : TimeSpan.Zero,
                            location = dr["location"].ToString(),
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
                            last_date = dr["last_date"] != DBNull.Value ? Convert.ToDateTime(dr["last_date"].ToString()) : DateTime.MinValue,
                        };
                        companies.Add(company);
                    }
                    dr.Close();
                }
            }
            finally
            {
                if (con_report.State == ConnectionState.Open)
                {
                    con_report.Close();
                }
            }
            return companies;
        }
        public List<CompanyModel> GetCompaniesByDriverDate(string driver ,DateTime start_date, DateTime stop_date)
        {
            List<CompanyModel> companies = new List<CompanyModel>();
            try
            {
                if (con_report.State == ConnectionState.Closed)
                {
                    con_report.Open();
                }
                string strCmd = string.Format($@"SELECT code,
	                                            driver,
	                                            date,
	                                            car_id,
	                                            time_start,
	                                            time_stop,
                                                location,
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
                                                FROM EditCompany
                                                WHERE driver = @driver AND date >= @start_date AND date <= @stop_date");
                SqlCommand command = new SqlCommand(strCmd, con_report);
                command.Parameters.AddWithValue("@driver", driver);
                command.Parameters.AddWithValue("@start_date", start_date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@stop_date", stop_date.ToString("yyyy-MM-dd"));
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CompanyModel company = new CompanyModel()
                        {
                            code = dr["code"].ToString(),
                            driver = dr["driver"].ToString(),
                            date = dr["date"] != DBNull.Value ? Convert.ToDateTime(dr["date"].ToString()) : DateTime.MinValue,
                            car_id = dr["car_id"].ToString(),
                            time_start = dr["time_start"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_start"].ToString()).Ticks) : TimeSpan.Zero,
                            time_stop = dr["time_stop"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_stop"].ToString()).Ticks) : TimeSpan.Zero,
                            location = dr["location"].ToString(),
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
                            last_date = dr["last_date"] != DBNull.Value ? Convert.ToDateTime(dr["last_date"].ToString()) : DateTime.MinValue,
                        };
                        companies.Add(company);
                    }
                    dr.Close();
                }
            }
            finally
            {
                if (con_report.State == ConnectionState.Open)
                {
                    con_report.Close();
                }
            }
            return companies;
        }
        public List<CompanyModel> GetCompaniesByCarDate(string car, DateTime start_date, DateTime stop_date)
        {
            List<CompanyModel> companies = new List<CompanyModel>();
            try
            {
                if (con_report.State == ConnectionState.Closed)
                {
                    con_report.Open();
                }
                string strCmd = string.Format($@"SELECT code,
	                                            driver,
	                                            date,
	                                            car_id,
	                                            time_start,
	                                            time_stop,
                                                location,
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
                                                FROM EditCompany
                                                WHERE car_id = @car_id AND date >= @start_date AND date <= @stop_date");
                SqlCommand command = new SqlCommand(strCmd, con_report);
                command.Parameters.AddWithValue("@car_id", car);
                command.Parameters.AddWithValue("@start_date", start_date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@stop_date", stop_date.ToString("yyyy-MM-dd"));
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CompanyModel company = new CompanyModel()
                        {
                            code = dr["code"].ToString(),
                            driver = dr["driver"].ToString(),
                            date = dr["date"] != DBNull.Value ? Convert.ToDateTime(dr["date"].ToString()) : DateTime.MinValue,
                            car_id = dr["car_id"].ToString(),
                            time_start = dr["time_start"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_start"].ToString()).Ticks) : TimeSpan.Zero,
                            time_stop = dr["time_stop"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_stop"].ToString()).Ticks) : TimeSpan.Zero,
                            location = dr["location"].ToString(),
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
                            last_date = dr["last_date"] != DBNull.Value ? Convert.ToDateTime(dr["last_date"].ToString()) : DateTime.MinValue,
                        };
                        companies.Add(company);
                    }
                    dr.Close();
                }
            }
            finally
            {
                if (con_report.State == ConnectionState.Open)
                {
                    con_report.Close();
                }
            }
            return companies;
        }
        public string EditInserts(List<CompanyModel> companies)
        {
            try
            {
                if (con_report.State == ConnectionState.Closed)
                {
                    con_report.Open();
                }
                string string_command = string.Format($@"
                    INSERT INTO 
                        EditCompany(code,
                            driver,
                            date,
                            car_id,
                            time_start,
                            time_stop,
                            location,
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

                using (SqlCommand cmd = new SqlCommand(string_command, con_report))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@code", SqlDbType.NVarChar).Value = null;
                    cmd.Parameters.Add("@driver", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@date", SqlDbType.Date);
                    cmd.Parameters.Add("@car_id", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@time_start", SqlDbType.Time);
                    cmd.Parameters.Add("@time_stop", SqlDbType.Time);
                    cmd.Parameters.Add("@location", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@job", SqlDbType.NVarChar);
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
                    cmd.Parameters.Add("@description", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@status", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@approver", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@last_date", SqlDbType.DateTime);

                    foreach (var item in companies)
                    {
                        cmd.Parameters["@code"].Value = item.code ?? (object)DBNull.Value;
                        cmd.Parameters["@driver"].Value = item.driver ?? (object)DBNull.Value;
                        cmd.Parameters["@date"].Value = item.date;
                        cmd.Parameters["@car_id"].Value = item.car_id ?? (object)DBNull.Value;
                        cmd.Parameters["@time_start"].Value = item.time_start;
                        cmd.Parameters["@time_stop"].Value = item.time_stop;
                        cmd.Parameters["@location"].Value = item.location ?? (object)DBNull.Value;
                        cmd.Parameters["@job"].Value = item.job ?? (object)DBNull.Value;
                        cmd.Parameters["@fleet"].Value = item.fleet;
                        cmd.Parameters["@cash"].Value = item.cash;
                        cmd.Parameters["@ctbo"].Value = item.ctbo;
                        cmd.Parameters["@exp"].Value = item.exp;
                        cmd.Parameters["@pt"].Value = item.pt;
                        cmd.Parameters["@mileage_start"].Value = item.mileage_start;
                        cmd.Parameters["@mileage_stop"].Value = item.mileage_stop;
                        cmd.Parameters["@km"].Value = item.km;
                        cmd.Parameters["@program_km"].Value = item.program_km;
                        cmd.Parameters["@auto_km"].Value = item.auto_km;
                        cmd.Parameters["@description"].Value = item.description ?? (object)DBNull.Value;
                        cmd.Parameters["@status"].Value = item.status ?? (object)DBNull.Value;
                        cmd.Parameters["@approver"].Value = item.approver ?? (object)DBNull.Value;
                        cmd.Parameters["@last_date"].Value = item.last_date;

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
                if (con_report.State == ConnectionState.Open)
                {
                    con_report.Close();
                }
            }
            return "Success";
        }

        public List<CompanyViewModel> GetEditCompaniesByDate(DateTime start_date, DateTime stop_date)
        {
            List<CompanyViewModel> companies = new List<CompanyViewModel>();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
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
                SqlCommand command = new SqlCommand(strCmd, con);
                command.Parameters.AddWithValue("@start_date", start_date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@stop_date", stop_date.ToString("yyyy-MM-dd"));
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
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return companies;
        }

        public List<CompanyViewModel> GetOriginalCompaniesByDate(DateTime start_date, DateTime stop_date)
        {
            List<CompanyViewModel> companies = new List<CompanyViewModel>();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
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
                SqlCommand command = new SqlCommand(strCmd, con);
                command.Parameters.AddWithValue("@start_date", start_date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@stop_date", stop_date.ToString("yyyy-MM-dd"));
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
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return companies;
        }

        public string UpdateByCode(CompanyModel company)
        {
            try
            {
                if (con_report.State == ConnectionState.Closed)
                {
                    con_report.Open();
                }
                string string_command = string.Format($@"
                    IF EXISTS (SELECT 1 FROM [dbo].[EditCompany] WHERE code = @code)
                    BEGIN
                            UPDATE 
                                EditCompany SET
                                driver = @driver,
                                date = @date,
                                car_id = @car_id,
	                            time_start = @time_start,
	                            time_stop = @time_stop,
                                location = @location,
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
                                WHERE code = @code
                            END
                                ELSE
                                BEGIN
                                    INSERT INTO [dbo].[EditCompany] (
                                        code, car_id ,driver, [date], time_start, time_stop, location, job,
                                        fleet ,cash, ctbo, exp, pt, mileage_start, mileage_stop, km,
                                        program_km, auto_km, description, status, approver, last_date
                                    )
                                    VALUES (
                                        @code, @car_id ,@driver, @date, @time_start, @time_stop, @location, @job,
                                        @fleet ,@cash, @ctbo, @exp, @pt, @mileage_start, @mileage_stop, @km,
                                        @program_km, @auto_km, @description, @status, @approver, @last_date
                                    );
                                END");
                using (SqlCommand cmd = new SqlCommand(string_command, con_report))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@code", SqlDbType.NVarChar, 50).Value = company.code ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@driver", SqlDbType.NVarChar, 20).Value = company.driver ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@date", SqlDbType.Date).Value = company.date;   
                    cmd.Parameters.Add("@car_id", SqlDbType.NVarChar, 50).Value = company.car_id ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@time_start", SqlDbType.Time).Value = company.time_start;     // หรือ datetime ถ้าเก็บทั้งวันที่+เวลา
                    cmd.Parameters.Add("@time_stop", SqlDbType.Time).Value = company.time_stop;
                    cmd.Parameters.Add("@location", SqlDbType.NVarChar, 200).Value = company.location ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@job", SqlDbType.NVarChar, 200).Value = company.job ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@fleet", SqlDbType.Float, 50).Value = company.fleet;
                    cmd.Parameters.Add("@cash", SqlDbType.Float).Value = company.cash;           // หรือ int ถ้าเป็นจำนวนเต็ม
                    cmd.Parameters.Add("@ctbo", SqlDbType.Float).Value = company.ctbo;
                    cmd.Parameters.Add("@exp", SqlDbType.Float).Value = company.exp;
                    cmd.Parameters.Add("@pt", SqlDbType.Float).Value = company.pt;
                    cmd.Parameters.Add("@mileage_start", SqlDbType.Int).Value = company.mileage_start;
                    cmd.Parameters.Add("@mileage_stop", SqlDbType.Int).Value = company.mileage_stop;
                    cmd.Parameters.Add("@km", SqlDbType.Int).Value = company.km;
                    cmd.Parameters.Add("@program_km", SqlDbType.Int).Value = company.program_km;
                    cmd.Parameters.Add("@auto_km", SqlDbType.Int).Value = company.auto_km;
                    cmd.Parameters.Add("@description", SqlDbType.NVarChar, 255).Value = company.description ?? (object)DBNull.Value; // -1 = max
                    cmd.Parameters.Add("@status", SqlDbType.NVarChar, 20).Value = company.status ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@approver", SqlDbType.NVarChar, 100).Value = company.approver ?? (object)DBNull.Value;
                    cmd.Parameters.Add("@last_date", SqlDbType.DateTime).Value = company.last_date;      // หรือ DateTime?
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (con_report.State == ConnectionState.Open)
                {
                    con_report.Close();
                }
            }
            return "Success";
        }

        public List<EmployeeModel> GetCompanyDrivers(DateTime start_date, DateTime stop_date)
        {
            List<EmployeeModel> drivers = new List<EmployeeModel>();
            try
            {
                if (con_report.State == ConnectionState.Closed)
                {
                    con_report.Open();
                }
                string strCmd = string.Format($@"select DISTINCT EditCompany.driver as emp_id,
                                                        emp.name from EditCompany 
                                                LEFT JOIN TRIP_EXPENSE.dbo.Employees emp ON EditCompany.driver = emp.emp_id
                                                where date >= @start AND date <= @stop
                                                order by name");
                SqlCommand command = new SqlCommand(strCmd, con_report);
                command.Parameters.AddWithValue("@start", start_date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@stop", stop_date.ToString("yyyy-MM-dd"));
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        EmployeeModel driver = new EmployeeModel()
                        {
                            emp_id = dr["emp_id"].ToString(),
                            name = dr["name"].ToString(),
                        };
                        drivers.Add(driver);
                    }
                    dr.Close();
                }
            }
            finally
            {
                if (con_report.State == ConnectionState.Open)
                {

                    con_report.Close();
                }
            }
            return drivers;
        }
        public List<CarModel> GetCompanyCars(DateTime start_date, DateTime stop_date)
        {
            List<CarModel> cars = new List<CarModel>();
            try
            {
                if (con_report.State == ConnectionState.Closed)
                {
                    con_report.Open();
                }
                string strCmd = string.Format($@"select DISTINCT EditCompany.car_id as car_id,
                                                        car.license_plate from EditCompany 
                                                LEFT JOIN TRIP_EXPENSE.dbo.Cars car ON EditCompany.car_id = car.car_id
                                                where date >= @start AND date <= @stop
                                                order by car_id");
                SqlCommand command = new SqlCommand(strCmd, con_report);
                command.Parameters.AddWithValue("@start", start_date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@stop", stop_date.ToString("yyyy-MM-dd"));
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CarModel car = new CarModel()
                        {
                            car_id = dr["car_id"].ToString(),
                            license_plate = dr["license_plate"].ToString(),
                        };
                        cars.Add(car);
                    }
                    dr.Close();
                }
            }
            finally
            {
                if (con_report.State == ConnectionState.Open)
                {

                    con_report.Close();
                }
            }
            return cars;
        }

        public CompanyModel GetCompanyByCode(string code)
        {
            CompanyModel company = new CompanyModel();
            try
            {
                if (con_report.State == ConnectionState.Closed)
                {
                    con_report.Open();
                }
                string strCmd = string.Format($@"SELECT code,
	                                            driver,
	                                            date,
	                                            car_id,
	                                            time_start,
	                                            time_stop,
                                                location,
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
                                                FROM EditCompany
                                                WHERE code = @code");
                SqlCommand command = new SqlCommand(strCmd, con_report);
                command.Parameters.AddWithValue("@code", code);
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        company = new CompanyModel()
                        {
                            code = dr["code"].ToString(),
                            driver = dr["driver"].ToString(),
                            date = dr["date"] != DBNull.Value ? Convert.ToDateTime(dr["date"].ToString()) : DateTime.MinValue,
                            car_id = dr["car_id"].ToString(),
                            time_start = dr["time_start"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_start"].ToString()).Ticks) : TimeSpan.Zero,
                            time_stop = dr["time_stop"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_stop"].ToString()).Ticks) : TimeSpan.Zero,
                            location = dr["location"].ToString(),
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
                            last_date = dr["last_date"] != DBNull.Value ? Convert.ToDateTime(dr["last_date"].ToString()) : DateTime.MinValue,
                        };
                    }
                    dr.Close();
                }
            }
            finally
            {
                if (con_report.State == ConnectionState.Open)
                {
                    con_report.Close();
                }
            }
            return company;
        }

        public string DeleteByCode(string code)
        {
            try
            {
                if (con_report.State == ConnectionState.Closed)
                {
                    con_report.Open();
                }
                string string_command = string.Format($@"DELETE FROM EditCompany WHERE code = @code");
                using (SqlCommand cmd = new SqlCommand(string_command, con_report))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@code", code);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (con_report.State == ConnectionState.Open)
                {
                    con_report.Close();
                }
            }
            return "Success";
        }
    }
}
