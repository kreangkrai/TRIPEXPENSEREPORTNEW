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

        //public string OriginalInserts(List<CompanyModel> companies)
        //{
        //    try
        //    {
        //        if (con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }
        //        string string_command = string.Format($@"
        //            INSERT INTO 
        //                OriginalCompany(code,
        //                    driver,
        //                    date,
        //                    car_id,
        //                    time_start,
        //                    time_stop,
        //                    location,
        //                    zipcode,
        //                    job,
        //                    fleet,
        //                    cash,
        //                    ctbo,
        //                    exp,
        //                    pt,
        //                    mileage_start,
        //                    mileage_stop,
        //                    km,
        //                    program_km,
        //                    auto_km,
        //                    description,
        //                    status,
        //                    approver,
        //                    last_date
        //                )
        //                VALUES(@code,
        //                    @driver,
        //                    @date,
        //                    @car_id,
        //                    @time_start,
        //                    @time_stop,
        //                    @location,
        //                    @zipcode,
        //                    @job,
        //                    @fleet,
        //                    @cash,
        //                    @ctbo,
        //                    @exp,
        //                    @pt,
        //                    @mileage_start,
        //                    @mileage_stop,
        //                    @km,
        //                    @program_km,
        //                    @auto_km,
        //                    @description,
        //                    @status,
        //                    @approver,
        //                    @last_date
        //                )");
        //        using (SqlCommand cmd = new SqlCommand(string_command, con))
        //        {
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Parameters.Add("@code", SqlDbType.Text);
        //            cmd.Parameters.Add("@driver", SqlDbType.Text);
        //            cmd.Parameters.Add("@date", SqlDbType.Date);
        //            cmd.Parameters.Add("@car_id", SqlDbType.Text);
        //            cmd.Parameters.Add("@time_start", SqlDbType.Time);
        //            cmd.Parameters.Add("@time_stop", SqlDbType.Time);
        //            cmd.Parameters.Add("@location", SqlDbType.Text);
        //            cmd.Parameters.Add("@zipcode", SqlDbType.Text);
        //            cmd.Parameters.Add("@job", SqlDbType.Text);
        //            cmd.Parameters.Add("@fleet", SqlDbType.Float);
        //            cmd.Parameters.Add("@cash", SqlDbType.Float);
        //            cmd.Parameters.Add("@ctbo", SqlDbType.Float);
        //            cmd.Parameters.Add("@exp", SqlDbType.Float);
        //            cmd.Parameters.Add("@pt", SqlDbType.Float);
        //            cmd.Parameters.Add("@mileage_start", SqlDbType.Int);
        //            cmd.Parameters.Add("@mileage_stop", SqlDbType.Int);
        //            cmd.Parameters.Add("@km", SqlDbType.Int);
        //            cmd.Parameters.Add("@program_km", SqlDbType.Int);
        //            cmd.Parameters.Add("@auto_km", SqlDbType.Int);
        //            cmd.Parameters.Add("@description", SqlDbType.Text);
        //            cmd.Parameters.Add("@status", SqlDbType.Text);
        //            cmd.Parameters.Add("@approver", SqlDbType.Text);
        //            cmd.Parameters.Add("@last_date", SqlDbType.DateTime);

        //            for (int i = 0; i < companies.Count; i++)
        //            {
        //                cmd.Parameters[0].Value = companies[i].code;
        //                cmd.Parameters[1].Value = companies[i].driver;
        //                cmd.Parameters[2].Value = companies[i].date;
        //                cmd.Parameters[3].Value = companies[i].car_id;
        //                cmd.Parameters[4].Value = companies[i].time_start;
        //                cmd.Parameters[5].Value = companies[i].time_stop;
        //                cmd.Parameters[6].Value = companies[i].location;
        //                cmd.Parameters[7].Value = companies[i].zipcode;
        //                cmd.Parameters[8].Value = companies[i].job;
        //                cmd.Parameters[9].Value = companies[i].fleet;
        //                cmd.Parameters[10].Value = companies[i].cash;
        //                cmd.Parameters[11].Value = companies[i].ctbo;
        //                cmd.Parameters[12].Value = companies[i].exp;
        //                cmd.Parameters[13].Value = companies[i].pt;
        //                cmd.Parameters[14].Value = companies[i].mileage_start;
        //                cmd.Parameters[15].Value = companies[i].mileage_stop;
        //                cmd.Parameters[16].Value = companies[i].km;
        //                cmd.Parameters[17].Value = companies[i].program_km;
        //                cmd.Parameters[18].Value = companies[i].auto_km;
        //                cmd.Parameters[19].Value = companies[i].description;
        //                cmd.Parameters[20].Value = companies[i].status;
        //                cmd.Parameters[21].Value = companies[i].approver;
        //                cmd.Parameters[22].Value = companies[i].last_date;
        //                cmd.ExecuteNonQuery();
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //    finally
        //    {
        //        if (con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //    }
        //    return "Success";
        //}

        public string UpdateByCode(CompanyModel company)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string string_command = string.Format($@"
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
                        WHERE code = @code");
                using (SqlCommand cmd = new SqlCommand(string_command, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@code", company.code);
                    cmd.Parameters.AddWithValue("@driver", company.driver);
                    cmd.Parameters.AddWithValue("@date", company.date);
                    cmd.Parameters.AddWithValue("@car_id", company.car_id);
                    cmd.Parameters.AddWithValue("@time_start", company.time_start);
                    cmd.Parameters.AddWithValue("@time_stop", company.time_stop);
                    cmd.Parameters.AddWithValue("@location", company.location);
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
    }
}
