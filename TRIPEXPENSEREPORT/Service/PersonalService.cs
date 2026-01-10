using Microsoft.Data.SqlClient;
using System.Data;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Service
{
    public class PersonalService : IPersonal
    {
        ConnectSQL connect = null;
        SqlConnection con = null;
        ConnectSQL connect_report = null;
        SqlConnection con_report = null;
        public PersonalService()
        {
            connect = new ConnectSQL();
            con = connect.OpenConnect();

            connect_report = new ConnectSQL();
            con_report = connect_report.OpenReportConnect();
        }
        public List<EmployeeModel> GetPesonalDrivers(DateTime start_date, DateTime stop_date)
        {
            List<EmployeeModel> drivers = new List<EmployeeModel>();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string strCmd = string.Format($@"select DISTINCT Personal.driver as emp_id,
                                                        emp.name from Personal 
                                                LEFT JOIN Employees emp ON Personal.driver = emp.emp_id
                                                where date >= @start AND date <= @stop AND status <> 'NA'
                                                order by name");
                SqlCommand command = new SqlCommand(strCmd, con);
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
                if (con.State == ConnectionState.Open)
                {

                    con.Close();
                }
            }
            return drivers;
        }

        public string OriginalInserts(List<PersonalModel> personals)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string string_command = string.Format($@"
                    INSERT INTO 
                        OriginalPersonal(
                            driver,
                            date,
                            time_start,
                            time_stop,
                            location,
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
                        VALUES(
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

                using (SqlCommand cmd = new SqlCommand(string_command, con))
                {
                    cmd.Parameters.Add("@driver", SqlDbType.VarChar, 100);
                    cmd.Parameters.Add("@date", SqlDbType.Date);
                    cmd.Parameters.Add("@time_start", SqlDbType.Time);
                    cmd.Parameters.Add("@time_stop", SqlDbType.Time);
                    cmd.Parameters.Add("@location", SqlDbType.VarChar, 200);
                    cmd.Parameters.Add("@job", SqlDbType.VarChar, 200);
                    cmd.Parameters.Add("@cash", SqlDbType.Float);
                    cmd.Parameters.Add("@ctbo", SqlDbType.Float);
                    cmd.Parameters.Add("@exp", SqlDbType.Float);
                    cmd.Parameters.Add("@pt", SqlDbType.Float);
                    cmd.Parameters.Add("@mileage_start", SqlDbType.Int);
                    cmd.Parameters.Add("@mileage_stop", SqlDbType.Int);
                    cmd.Parameters.Add("@km", SqlDbType.Int);
                    cmd.Parameters.Add("@program_km", SqlDbType.Int);
                    cmd.Parameters.Add("@auto_km", SqlDbType.Int);
                    cmd.Parameters.Add("@description", SqlDbType.VarChar, -1);
                    cmd.Parameters.Add("@status", SqlDbType.VarChar, 50);
                    cmd.Parameters.Add("@gasoline", SqlDbType.VarChar, 100);
                    cmd.Parameters.Add("@approver", SqlDbType.VarChar, 100);
                    cmd.Parameters.Add("@last_date", SqlDbType.DateTime);

                    foreach (var p in personals)
                    {
                        cmd.Parameters["@driver"].Value = (object)p.driver ?? DBNull.Value;
                        cmd.Parameters["@date"].Value = (object)p.date ?? DBNull.Value;
                        cmd.Parameters["@time_start"].Value = (object)p.time_start ?? DBNull.Value;
                        cmd.Parameters["@time_stop"].Value = (object)p.time_stop ?? DBNull.Value;
                        cmd.Parameters["@location"].Value = (object)p.location ?? DBNull.Value;
                        cmd.Parameters["@job"].Value = (object)p.job ?? DBNull.Value;
                        cmd.Parameters["@cash"].Value = p.cash;                  
                        cmd.Parameters["@ctbo"].Value = p.ctbo;
                        cmd.Parameters["@exp"].Value = p.exp;
                        cmd.Parameters["@pt"].Value = p.pt;
                        cmd.Parameters["@mileage_start"].Value = p.mileage_start;
                        cmd.Parameters["@mileage_stop"].Value = p.mileage_stop;
                        cmd.Parameters["@km"].Value = p.km;
                        cmd.Parameters["@program_km"].Value = p.program_km;
                        cmd.Parameters["@auto_km"].Value = p.auto_km;
                        cmd.Parameters["@description"].Value = (object)p.description ?? DBNull.Value;
                        cmd.Parameters["@status"].Value = (object)p.status ?? DBNull.Value;
                        cmd.Parameters["@gasoline"].Value = (object)p.gasoline ?? DBNull.Value;
                        cmd.Parameters["@approver"].Value = (object)p.approver ?? DBNull.Value;
                        cmd.Parameters["@last_date"].Value = (object)p.last_date ?? DBNull.Value;

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

        public string EditInserts(List<PersonalModel> personals)
        {
            try
            {
                if (con_report.State == ConnectionState.Closed)
                {
                    con_report.Open();
                }
                string string_command = string.Format($@"
                    INSERT INTO 
                        EditPersonal(code,
                            driver,
                            date,
                            time_start,
                            time_stop,
                            location,
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


                using (SqlCommand cmd = new SqlCommand(string_command, con_report))
                {
                    cmd.Parameters.Add("@code", SqlDbType.VarChar, 20);
                    cmd.Parameters.Add("@driver", SqlDbType.VarChar, 100);
                    cmd.Parameters.Add("@date", SqlDbType.Date);
                    cmd.Parameters.Add("@time_start", SqlDbType.Time);
                    cmd.Parameters.Add("@time_stop", SqlDbType.Time);
                    cmd.Parameters.Add("@location", SqlDbType.VarChar, 200);
                    cmd.Parameters.Add("@job", SqlDbType.VarChar, 200);
                    cmd.Parameters.Add("@cash", SqlDbType.Float);
                    cmd.Parameters.Add("@ctbo", SqlDbType.Float);
                    cmd.Parameters.Add("@exp", SqlDbType.Float);
                    cmd.Parameters.Add("@pt", SqlDbType.Float);
                    cmd.Parameters.Add("@mileage_start", SqlDbType.Int);
                    cmd.Parameters.Add("@mileage_stop", SqlDbType.Int);
                    cmd.Parameters.Add("@km", SqlDbType.Int);
                    cmd.Parameters.Add("@program_km", SqlDbType.Int);
                    cmd.Parameters.Add("@auto_km", SqlDbType.Int);
                    cmd.Parameters.Add("@description", SqlDbType.VarChar, -1);
                    cmd.Parameters.Add("@status", SqlDbType.VarChar, 50);
                    cmd.Parameters.Add("@gasoline", SqlDbType.VarChar, 100);
                    cmd.Parameters.Add("@approver", SqlDbType.VarChar, 100);
                    cmd.Parameters.Add("@last_date", SqlDbType.DateTime);

                    foreach (var p in personals)
                    {
                        cmd.Parameters["@code"].Value = (object)p.code ?? DBNull.Value;
                        cmd.Parameters["@driver"].Value = (object)p.driver ?? DBNull.Value;
                        cmd.Parameters["@date"].Value = (object)p.date ?? DBNull.Value;
                        cmd.Parameters["@time_start"].Value = (object)p.time_start ?? DBNull.Value;
                        cmd.Parameters["@time_stop"].Value = (object)p.time_stop ?? DBNull.Value;
                        cmd.Parameters["@location"].Value = (object)p.location ?? DBNull.Value;
                        cmd.Parameters["@job"].Value = (object)p.job ?? DBNull.Value;
                        cmd.Parameters["@cash"].Value = p.cash;
                        cmd.Parameters["@ctbo"].Value = p.ctbo;
                        cmd.Parameters["@exp"].Value = p.exp;
                        cmd.Parameters["@pt"].Value = p.pt;
                        cmd.Parameters["@mileage_start"].Value = p.mileage_start;
                        cmd.Parameters["@mileage_stop"].Value = p.mileage_stop;
                        cmd.Parameters["@km"].Value = p.km;
                        cmd.Parameters["@program_km"].Value = p.program_km;
                        cmd.Parameters["@auto_km"].Value = p.auto_km;
                        cmd.Parameters["@description"].Value = (object)p.description ?? DBNull.Value;
                        cmd.Parameters["@status"].Value = (object)p.status ?? DBNull.Value;
                        cmd.Parameters["@gasoline"].Value = (object)p.gasoline ?? DBNull.Value;
                        cmd.Parameters["@approver"].Value = (object)p.approver ?? DBNull.Value;
                        cmd.Parameters["@last_date"].Value = (object)p.last_date ?? DBNull.Value;

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

        public string UpdateByCode(PersonalModel personal)
        {
            try
            {
                if (con_report.State == ConnectionState.Closed)
                {
                    con_report.Open();
                }
                string string_command = string.Format($@"IF EXISTS (SELECT 1 FROM [dbo].[EditPersonal] WHERE code = @code)
                                BEGIN
                                    UPDATE [dbo].[EditPersonal]
                                    SET 
                                        driver        = @driver,
                                        [date]        = @date,
                                        time_start    = @time_start,
                                        time_stop     = @time_stop,
                                        location      = @location,
                                        job           = @job,
                                        cash          = @cash,
                                        ctbo          = @ctbo,
                                        exp           = @exp,
                                        pt            = @pt,
                                        mileage_start = @mileage_start,
                                        mileage_stop  = @mileage_stop,
                                        km            = @km,
                                        program_km    = @program_km,
                                        auto_km       = @auto_km,
                                        description   = @description,
                                        status        = @status,
                                        gasoline      = @gasoline,
                                        approver      = @approver,
                                        last_date     = @last_date
                                    WHERE code = @code;
                                END
                                ELSE
                                BEGIN
                                    INSERT INTO [dbo].[EditPersonal] (
                                        code, driver, [date], time_start, time_stop, location, job,
                                        cash, ctbo, exp, pt, mileage_start, mileage_stop, km,
                                        program_km, auto_km, description, status, gasoline, approver, last_date
                                    )
                                    VALUES (
                                        @code, @driver, @date, @time_start, @time_stop, @location, @job,
                                        @cash, @ctbo, @exp, @pt, @mileage_start, @mileage_stop, @km,
                                        @program_km, @auto_km, @description, @status, @gasoline, @approver, @last_date
                                    );
                                END");
                using (SqlCommand cmd = new SqlCommand(string_command, con_report))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@code", personal.code ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@driver", personal.driver ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@date", personal.date);
                    cmd.Parameters.AddWithValue("@time_start", personal.time_start);
                    cmd.Parameters.AddWithValue("@time_stop", personal.time_stop);
                    cmd.Parameters.AddWithValue("@location", personal.location ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@job", personal.job ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@cash", personal.cash);
                    cmd.Parameters.AddWithValue("@ctbo", personal.ctbo);
                    cmd.Parameters.AddWithValue("@exp", personal.exp);
                    cmd.Parameters.AddWithValue("@pt", personal.pt);
                    cmd.Parameters.AddWithValue("@mileage_start", personal.mileage_start);
                    cmd.Parameters.AddWithValue("@mileage_stop", personal.mileage_stop);
                    cmd.Parameters.AddWithValue("@km", personal.km);
                    cmd.Parameters.AddWithValue("@program_km", personal.program_km);
                    cmd.Parameters.AddWithValue("@auto_km", personal.auto_km);
                    cmd.Parameters.AddWithValue("@description", personal.description ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@status", personal.status ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@gasoline", personal.gasoline ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@approver", personal.approver ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@last_date", personal.last_date);

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

        public List<PersonalViewModel> GetOriginalPersonalsByDate(DateTime start_date, DateTime stop_date)
        {
            List<PersonalViewModel> personals = new List<PersonalViewModel>();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
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
                SqlCommand command = new SqlCommand(strCmd, con);
                command.Parameters.AddWithValue("@start_date", start_date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@stop_date", stop_date.ToString("yyyy-MM-dd"));
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
                if (con.State == ConnectionState.Open)
                {

                con.Close(); 
                }
            }
            return personals;
        }

        public List<PersonalViewModel> GetEditPersonalsByDate(string emp_id,DateTime start_date, DateTime stop_date)
        {
            List<PersonalViewModel> personals = new List<PersonalViewModel>();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
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
                                                WHERE date >= @start_date AND date <= @stop_date AND EditPersonal.driver = @driver");
                SqlCommand command = new SqlCommand(strCmd, con);
                command.Parameters.AddWithValue("@start_date", start_date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@stop_date", stop_date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@driver", emp_id);
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
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return personals;
        }
        public List<PersonalModel> GetPersonalsByDate(string emp_id, DateTime start_date, DateTime stop_date)
        {
            List<PersonalModel> personals = new List<PersonalModel>();
            try
            {
                if (con_report.State == ConnectionState.Closed)
                {
                    con_report.Open();
                }
                string strCmd = string.Format($@"SELECT code,
	                                            driver,
	                                            date,
	                                            time_start,
	                                            time_stop,
                                                location,
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
                                                FROM EditPersonal
                                                WHERE date >= @start_date AND date <= @stop_date AND driver = @driver");
                SqlCommand command = new SqlCommand(strCmd, con_report);
                command.Parameters.AddWithValue("@start_date", start_date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@stop_date", stop_date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@driver", emp_id);
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        PersonalModel personal = new PersonalModel()
                        {
                            code = dr["code"].ToString(),
                            driver = dr["driver"].ToString(),
                            date = dr["date"] != DBNull.Value ? Convert.ToDateTime(dr["date"].ToString()) : DateTime.MinValue,
                            time_start = dr["time_start"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_start"].ToString()).Ticks) : TimeSpan.Zero,
                            time_stop = dr["time_stop"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_stop"].ToString()).Ticks) : TimeSpan.Zero,
                            location = dr["location"].ToString(),
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
                            last_date = dr["last_date"] != DBNull.Value ? Convert.ToDateTime(dr["last_date"].ToString()) : DateTime.MinValue
                        };
                        personals.Add(personal);
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
            return personals;
        }

        public PersonalModel GetPersonalsByCode(string code)
        {
            PersonalModel personal = new PersonalModel();
            try
            {
                if (con_report.State == ConnectionState.Closed)
                {
                    con_report.Open();
                }
                string strCmd = string.Format($@"SELECT code,
	                                            driver,
	                                            date,
	                                            time_start,
	                                            time_stop,
                                                location,
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
                                                FROM EditPersonal
                                                WHERE code = @code");
                SqlCommand command = new SqlCommand(strCmd, con_report);
                command.Parameters.AddWithValue("@code", code);
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        personal = new PersonalModel()
                        {
                            code = dr["code"].ToString(),
                            driver = dr["driver"].ToString(),
                            date = dr["date"] != DBNull.Value ? Convert.ToDateTime(dr["date"].ToString()) : DateTime.MinValue,
                            time_start = dr["time_start"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_start"].ToString()).Ticks) : TimeSpan.Zero,
                            time_stop = dr["time_stop"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_stop"].ToString()).Ticks) : TimeSpan.Zero,
                            location = dr["location"].ToString(),
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
            return personal;
        }

        public string DeleteByCode(string code)
        {
            try
            {
                if (con_report.State == ConnectionState.Closed)
                {
                    con_report.Open();
                }
                string string_command = string.Format($@"DELETE FROM EditPersonal WHERE code = @code");
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
