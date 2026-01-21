using Microsoft.Data.SqlClient;
using OfficeOpenXml;
using System.Data;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Service
{
    public class AllowanceService : IAllowance
    {
        private IArea Area;
        private IUser User;
        private IProvince Province;
        private CTLInterfaces.IEmployee Employee;
        ConnectSQL connect = null;
        SqlConnection con = null;
        ConnectSQL connect_report = null;
        SqlConnection con_report = null;
        public AllowanceService()
        {
            Area = new AreaService();
            User = new UserService();
            Province = new ProvinceService();
            connect = new ConnectSQL();
            connect_report = new ConnectSQL();
            Employee = new CTLServices.EmployeeService();
            con = connect.OpenConnect();
            con_report = connect_report.OpenReportConnect();
        }
        public List<AllowanceModel> GetEditAllowancesByDate(string emp_id,DateTime start_date, DateTime stop_date)
        {
            List<AllowanceModel> allowances = new List<AllowanceModel>();
            try
            {
                if (con_report.State == ConnectionState.Closed)
                {
                    con_report.Open();
                }
                string strCmd = string.Format($@"SELECT code,
	                                           emp_id,
	                                           zipcode,
	                                           date,
                                               time_start,
                                               time_stop,
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
											   approver,
												last_date
                                                FROM EditAllowance
                                                WHERE emp_id = @emp_id AND date >= @start_date AND date <= @stop_date");
                SqlCommand command = new SqlCommand(strCmd, con_report);
                command.Parameters.AddWithValue("@emp_id", emp_id);
                command.Parameters.AddWithValue("@start_date", start_date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@stop_date", stop_date.ToString("yyyy-MM-dd"));
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        AllowanceModel allowance = new AllowanceModel()
                        {
                            code = dr["code"].ToString(),
                            emp_id = dr["emp_id"].ToString(),
                            zipcode = dr["zipcode"].ToString(),
                            date = dr["date"] != DBNull.Value ? Convert.ToDateTime(dr["date"].ToString()) : DateTime.MinValue,
                            time_start = dr["time_start"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_start"].ToString()).Ticks) : TimeSpan.Zero,
                            time_stop = dr["time_stop"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_stop"].ToString()).Ticks) : TimeSpan.Zero,
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
                            last_date = dr["last_date"] != DBNull.Value ? Convert.ToDateTime(dr["last_date"].ToString()) : DateTime.MinValue,
                        };
                        allowances.Add(allowance);
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
            return allowances;
        }

        public AllowanceModel GetAllowancesByCode(string code)
        {
            AllowanceModel allowance = new AllowanceModel();
            try
            {
                if (con_report.State == ConnectionState.Closed)
                {
                    con_report.Open();
                }
                string strCmd = string.Format($@"SELECT code,
	                                           emp_id,
	                                           zipcode,
	                                           date,
                                               time_start,
                                               time_stop,
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
											   approver,
												last_date
                                                FROM EditAllowance
                                                WHERE code = @code");
                SqlCommand command = new SqlCommand(strCmd, con_report);
                command.Parameters.AddWithValue("@code", code);
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        allowance = new AllowanceModel()
                        {
                            code = dr["code"].ToString(),
                            emp_id = dr["emp_id"].ToString(),
                            zipcode = dr["zipcode"].ToString(),
                            date = dr["date"] != DBNull.Value ? Convert.ToDateTime(dr["date"].ToString()) : DateTime.MinValue,
                            time_start = dr["time_start"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_start"].ToString()).Ticks) : TimeSpan.Zero,
                            time_stop = dr["time_stop"] != DBNull.Value ? new TimeSpan(Convert.ToDateTime(dr["time_stop"].ToString()).Ticks) : TimeSpan.Zero,
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
            return allowance;
        }

        public string EditInserts(List<AllowanceModel> allowances)
        {
            try
            {
                if (con_report.State == ConnectionState.Closed)
                {
                    con_report.Open();
                }
                string string_command = string.Format($@"
                    INSERT INTO 
                        EditAllowance(code,
                            emp_id,
                            zipcode,
                            date,
                            time_start,
                            time_stop,
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
                            approver,
                            last_date
                        )
                        VALUES(@code,
                            @emp_id,
                            @zipcode,
                            @date,
                            @time_start,
                            @time_stop,
                            @customer,
                            @job,
                            @allowance_province,
                            @allowance_1_4,
                            @allowance_4_8,
                            @allowance_8,
                            @allowance_other,
                            @allowance_hostel,
                            @list,
                            @amount,
                            @description,
                            @remark,
                            @status,
                            @approver,
                            @last_date
                        )");
                using (SqlCommand cmd = new SqlCommand(string_command, con_report))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@code", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@emp_id", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@zipcode", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@date", SqlDbType.DateTime);
                    cmd.Parameters.Add("@time_start", SqlDbType.Time);
                    cmd.Parameters.Add("@time_stop", SqlDbType.Time);
                    cmd.Parameters.Add("@customer", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@job", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@allowance_province", SqlDbType.Float);
                    cmd.Parameters.Add("@allowance_1_4", SqlDbType.Float);
                    cmd.Parameters.Add("@allowance_4_8", SqlDbType.Float);
                    cmd.Parameters.Add("@allowance_8", SqlDbType.Float);
                    cmd.Parameters.Add("@allowance_other", SqlDbType.Float);
                    cmd.Parameters.Add("@allowance_hostel", SqlDbType.Float);
                    cmd.Parameters.Add("@list", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@amount", SqlDbType.Float);
                    cmd.Parameters.Add("@description", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@remark", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@status", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@approver", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@last_date", SqlDbType.DateTime);

                    foreach (var item in allowances)
                    {
                        cmd.Parameters["@code"].Value = item.code ?? (object)DBNull.Value;
                        cmd.Parameters["@emp_id"].Value = item.emp_id ?? (object)DBNull.Value;
                        cmd.Parameters["@zipcode"].Value = item.zipcode ?? (object)DBNull.Value;
                        cmd.Parameters["@date"].Value = item.date == default ? (object)DBNull.Value : item.date;
                        cmd.Parameters["@time_start"].Value = item.time_start;
                        cmd.Parameters["@time_stop"].Value = item.time_stop;
                        cmd.Parameters["@customer"].Value = item.customer ?? (object)DBNull.Value;
                        cmd.Parameters["@job"].Value = item.job ?? (object)DBNull.Value;
                        cmd.Parameters["@allowance_province"].Value = item.allowance_province;
                        cmd.Parameters["@allowance_1_4"].Value = item.allowance_1_4;
                        cmd.Parameters["@allowance_4_8"].Value = item.allowance_4_8;
                        cmd.Parameters["@allowance_8"].Value = item.allowance_8;
                        cmd.Parameters["@allowance_other"].Value = item.allowance_other;
                        cmd.Parameters["@allowance_hostel"].Value = item.allowance_hostel;
                        cmd.Parameters["@list"].Value = item.list ?? (object)DBNull.Value;
                        cmd.Parameters["@amount"].Value = item.amount;
                        cmd.Parameters["@description"].Value = item.description ?? (object)DBNull.Value;
                        cmd.Parameters["@remark"].Value = item.remark ?? (object)DBNull.Value;
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
        
        public string UpdateByCode(AllowanceModel allowance)
        {
            try
            {
                if (con_report.State == ConnectionState.Closed)
                {
                    con_report.Open();
                }
                string string_command = string.Format($@"
                    IF EXISTS (SELECT 1 FROM [dbo].[EditAllowance] WHERE code = @code)
                    BEGIN
                    UPDATE 
                        EditAllowance SET
                        emp_id = @emp_id,
                        zipcode = @zipcode,
	                    date = @date,
                        time_start = @time_start,
                        time_stop = @time_stop,
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
                        WHERE code = @code
                   END
                   ELSE
                   BEGIN
                    INSERT INTO 
                        EditAllowance(code,
                            emp_id,
                            zipcode,
                            date,
                            time_start,
                            time_stop,
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
                            approver,
                            last_date
                        )
                        VALUES(@code,
                            @emp_id,
                            @zipcode,
                            @date,
                            @time_start,
                            @time_stop,
                            @customer,
                            @job,
                            @allowance_province,
                            @allowance_1_4,
                            @allowance_4_8,
                            @allowance_8,
                            @allowance_other,
                            @allowance_hostel,
                            @list,
                            @amount,
                            @description,
                            @remark,
                            @status,
                            @approver,
                            @last_date)
                       END");
                using (SqlCommand cmd = new SqlCommand(string_command, con_report))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@code", allowance.code);
                    cmd.Parameters.AddWithValue("@emp_id", allowance.emp_id);
                    cmd.Parameters.AddWithValue("@zipcode", allowance.zipcode);
                    cmd.Parameters.AddWithValue("@date", allowance.date);
                    cmd.Parameters.AddWithValue("@time_start", allowance.time_start);
                    cmd.Parameters.AddWithValue("@time_stop", allowance.time_stop);
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
                    cmd.Parameters.AddWithValue("@approver", allowance.approver ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@last_date", allowance.last_date);
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
        public string UpdateApproved(List<string> codes, string approver)
        {
            try
            {
                if (con_report.State == ConnectionState.Closed)
                {
                    con_report.Open();
                }
                string string_command = string.Format($@"
                                    UPDATE [dbo].[EditAllowance]
                                    SET
                                        last_date = @last_date,
                                        approver = @approver,
                                        status = @status
                                    WHERE code = @code AND status <> 'Approved'");
                using (SqlCommand cmd = new SqlCommand(string_command, con_report))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@code", SqlDbType.NVarChar);
                    cmd.Parameters.AddWithValue("@last_date", SqlDbType.DateTime);
                    cmd.Parameters.AddWithValue("@approver", SqlDbType.NVarChar);
                    cmd.Parameters.AddWithValue("@status", SqlDbType.NVarChar);

                    foreach (var code in codes)
                    {
                        cmd.Parameters[0].Value = code;
                        cmd.Parameters[1].Value = DateTime.Now;
                        cmd.Parameters[2].Value = approver;
                        cmd.Parameters[3].Value = "Approved";

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
        public string DeleteByCode(string code)
        {
            try
            {
                if (con_report.State == ConnectionState.Closed)
                {
                    con_report.Open();
                }
                string string_command = string.Format($@"DELETE FROM EditAllowance WHERE code = @code");
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
        public List<AllowanceModel> CalculateAllowanceNew(string emp_id, List<DataTripModel> trips , DateTime start, DateTime stop)
        {
            List<AllowanceModel> dataAllowances = new List<AllowanceModel>();

            List<CTLModels.EmployeeModel> users = Employee.GetEmployees();
            List<AreaModel> areas = Area.GetAreas();

            string location = users.Where(w => w.emp_id == emp_id).FirstOrDefault().location;
            List<string> area_location = new List<string>();
            if (location == "hq") {
                area_location = areas.Where(w => w.hq == true).Select(s => s.code).ToList();
            }
            if (location == "rbo")
            {
                area_location = areas.Where(w => w.rbo == true).Select(s => s.code).ToList();
            }
            if (location == "kbo")
            {
                area_location = areas.Where(w => w.kbo == true).Select(s => s.code).ToList();
            }

            for (DateTime date = start; date <= stop; date = date.AddDays(1))
            {
                double Allowance_Outside_Region = 0;
                double Allowance1_4 = 0;
                double Allowance4_8 = 0;
                double Allowance8 = 0;
                bool zone = false;     //true in zone , false out zone

                List<DataTripModel> _trips = trips.Where(w => w.date.Date == date.Date).ToList();
                string code = $"{emp_id}{date.ToString("yyyyMMddHHmmss")}";

                if (_trips.Count > 0) // Have Trip each date
                {
                    #region Outside Zone
                    //Check Zipcode Not Null.
                    if (_trips.Any(a => a.zipcode != ""))
                    {
                        List<DataTripModel> zones = _trips.Where(w => areas.Select(s => s.code).Contains(w.zipcode.Substring(0, 2))).ToList();
                        if (zones.Count != _trips.Count)
                        {
                            zone = true;
                            Allowance_Outside_Region = 100;
                        }
                        else
                        {
                            Allowance_Outside_Region = 0;
                        }
                    }
                    else
                    {
                        //Check Zipcode Not Null.
                        if (_trips.Any(a => a.zipcode != ""))
                        {
                            List<DataTripModel> AllZone = _trips.Where(w => area_location.Contains(w.zipcode.Substring(0, 2))).ToList();
                            //Check All CheckIn Zone.
                            zone = AllZone.Count > 0 ? true : false;

                            if (zone) // out zone
                            {
                                Allowance_Outside_Region = 100;
                            }
                        }
                        else
                        {
                            Allowance_Outside_Region = 0;
                        }
                    }
                    #endregion

                    TimeSpan first_start = _trips.FirstOrDefault()?.time_start ?? TimeSpan.Zero;
                    TimeSpan last_stop = _trips.LastOrDefault()?.time_stop ?? TimeSpan.Zero;
                    double time_duration = (last_stop - first_start).TotalMinutes;

                    if (!zone) // In Zone
                    {
                        if (location == "rbo")
                        {
                            Allowance1_4 = 50;
                            Allowance4_8 = 70;
                            Allowance8 = 80;
                        }
                        else  // HQ , KBO
                        {
                            //bool check_customer = _trips.Any(a => a.location_mode == "CUSTOMER"); // Check Customer In Trip

                            //if (check_customer)
                            //{
                                if (time_duration >= 60 && time_duration < 240)
                                {
                                    Allowance1_4 = 50;
                                }
                                else if (time_duration >= 240 && time_duration < 480)
                                {
                                    Allowance1_4 = 50;
                                    Allowance4_8 = 70;
                                }
                                else if (time_duration >= 480)
                                {
                                    Allowance1_4 = 50;
                                    Allowance4_8 = 70;
                                    Allowance8 = 80;
                                }
                            //}
                            //else
                            //{
                            //    double sum_time_duration = 0.0;
                            //    List<string> name_trips = _trips.GroupBy(g => g.trip).Select(s => s.FirstOrDefault().trip).ToList();
                            //    for (int i = 0; i < name_trips.Count; i++)
                            //    {
                            //        var ts = _trips.Where(w => w.trip == name_trips[i]).ToList();
                            //        var start_ = ts.FirstOrDefault().date;
                            //        var stop_ = ts.LastOrDefault().date;
                            //        sum_time_duration += (stop_ - start_).TotalMinutes;
                            //    }

                            //    if (sum_time_duration >= 60 && sum_time_duration < 240)
                            //    {
                            //        Allowance1_4 = 50;
                            //    }
                            //    else if (sum_time_duration >= 240 && sum_time_duration < 480)
                            //    {
                            //        Allowance1_4 = 50;
                            //        Allowance4_8 = 70;
                            //    }
                            //    else if (sum_time_duration >= 480)
                            //    {
                            //        Allowance1_4 = 50;
                            //        Allowance4_8 = 70;
                            //        Allowance8 = 80;
                            //    }
                            //}
                        }
                    }
                    else // Have In and Out
                    {

                        //bool check_customer = _trips.Any(a => a.location_mode == "CUSTOMER"); // Check Customer

                        //if (check_customer)
                        //{
                            if (time_duration >= 60 && time_duration < 240)
                            {
                                Allowance1_4 = 50;
                            }
                            else if (time_duration >= 240 && time_duration < 480)
                            {
                                Allowance1_4 = 50;
                                Allowance4_8 = 70;
                            }
                            else if (time_duration >= 480)
                            {
                                Allowance1_4 = 50;
                                Allowance4_8 = 70;
                                Allowance8 = 80;
                            }
                        //}
                        //else
                        //{
                        //    double sum_time_duration = 0.0;
                        //    List<string> name_trips = _trips.GroupBy(g => g.trip).Select(s => s.FirstOrDefault().trip).ToList();
                        //    for (int i = 0; i < name_trips.Count; i++)
                        //    {
                        //        var ts = _trips.Where(w => w.trip == name_trips[i]).ToList();
                        //        var start_ = ts.FirstOrDefault().date;
                        //        var stop_ = ts.LastOrDefault().date;
                        //        sum_time_duration += (stop_ - start_).TotalMinutes;
                        //    }

                        //    if (sum_time_duration >= 60 && sum_time_duration < 240)
                        //    {
                        //        Allowance1_4 = 50;
                        //    }
                        //    else if (sum_time_duration >= 240 && sum_time_duration < 480)
                        //    {
                        //        Allowance1_4 = 50;
                        //        Allowance4_8 = 70;
                        //    }
                        //    else if (sum_time_duration >= 480)
                        //    {
                        //        Allowance1_4 = 50;
                        //        Allowance4_8 = 70;
                        //        Allowance8 = 80;
                        //    }
                        //}
                    }

                    //double Allowance_Sum = Allowance_Outside_Region + Allowance1_4 + Allowance4_8 + Allowance8;

                    var AllDay = _trips.Select(s => s.location).ToHashSet().ToArray();
                    string joinTrip = AllDay != null ? string.Join(",", AllDay.Where(w => w != "").ToArray()) : "";

                    var AllMode = _trips.Select(s => s.mode).ToHashSet().ToArray();

                    string joinMode = AllMode != null ? string.Join(",", AllMode) : "";
                    
                    AllowanceModel allowance = new AllowanceModel()
                    {
                        emp_id = emp_id,
                        date = date,
                        customer = joinTrip,
                        time_start = new TimeSpan(_trips.FirstOrDefault().time_start.Hours, _trips.FirstOrDefault().time_start.Minutes, _trips.FirstOrDefault().time_start.Seconds),
                        time_stop = new TimeSpan(_trips.LastOrDefault().time_stop.Hours, _trips.LastOrDefault().time_stop.Minutes, _trips.LastOrDefault().time_stop.Seconds),
                        job = _trips.FirstOrDefault().job_id,
                        allowance_province = Allowance_Outside_Region,
                        allowance_1_4 = Allowance1_4,
                        allowance_4_8 = Allowance4_8,
                        allowance_8 = Allowance8,
                        allowance_hostel = 0,
                        allowance_other = 0,
                        amount = 0,
                        zipcode = _trips.FirstOrDefault().zipcode,
                        approver = "",
                        status = "Pending",
                        description = "",
                        last_date = DateTime.Now,
                        code = code,
                        list = "",
                        remark = ""
                    };

                    dataAllowances.Add(allowance);
                }
                else // ไม่มี Trip
                {
                    AllowanceModel allowance = new AllowanceModel()
                    {
                        emp_id = emp_id,
                        date = date,
                        time_start = TimeSpan.Zero,
                        time_stop = TimeSpan.Zero,
                        customer = "",
                        job = "",
                        allowance_province = 0,
                        allowance_1_4 = 0,
                        allowance_4_8 = 0,
                        allowance_8 = 0,
                        allowance_hostel = 0,
                        allowance_other = 0,
                        amount = 0,
                        zipcode = "",
                        approver = "",
                        status = "Pending",
                        description = "",
                        last_date = DateTime.Now,
                        code = code,
                        list = "",
                        remark = ""
                    };
                    dataAllowances.Add(allowance);
                }
            }
            return dataAllowances;
        }
        public Stream ExportAllowance(FileInfo path, List<AllowanceModel> allowances, string month, CTLModels.EmployeeModel emp)
        {
            Stream stream = new MemoryStream();
            if (path.Exists)
            {
                using (ExcelPackage p = new ExcelPackage(path))
                {
                    List<ProvinceModel> provinces = Province.GetProvinces();
                    ExcelWorksheet worksheet = p.Workbook.Worksheets["เบี้ยเลี้ยง"];
                    int startRows = 11;
                    var parts = month.Split('-');
                    if (parts.Length != 2
                        || !int.TryParse(parts[0], out int year)
                        || !int.TryParse(parts[1], out int mon))
                    {
                        return null;
                    }


                    DateTime start = new DateTime(year, mon, 1);
                    DateTime stop = start.AddMonths(1).AddDays(-1);
                    for (DateTime date = start; date <= stop; date = date.AddDays(1))
                    {
                        AllowanceModel allowance = allowances.Where(w => w.date.Date == date.Date).FirstOrDefault();
                        string _date = date.ToString("dd/MM/yyyy");
                        worksheet.Cells["B" + startRows].Value = date;
                        if (allowance != null)
                        {
                            worksheet.Cells["C" + startRows].Value = allowance.customer;
                            string prvince = provinces.Where(w => w.zipcode == allowance.zipcode).FirstOrDefault().province;
                            worksheet.Cells["D" + startRows].Value = prvince;
                            worksheet.Cells["E" + startRows].Value = allowance.job;
                            worksheet.Cells["F" + startRows].Value = allowance.allowance_province ;
                            if ((double)worksheet.Cells["F" + (startRows)].Value == 0)
                            {
                                worksheet.Cells["F" + (startRows)].Value = "";
                            }
                            worksheet.Cells["G" + (startRows)].Value = allowance.allowance_1_4;
                            if ((double)worksheet.Cells["G" + (startRows)].Value == 0)
                            {
                                worksheet.Cells["G" + (startRows)].Value = "";
                            }
                            worksheet.Cells["H" + startRows].Value = allowance.allowance_4_8;
                            if ((double)worksheet.Cells["H" + (startRows)].Value == 0)
                            {
                                worksheet.Cells["H" + (startRows)].Value = "";
                            }
                            worksheet.Cells["I" + (startRows)].Value = allowance.allowance_8;
                            if ((double)worksheet.Cells["I" + (startRows)].Value == 0)
                            {
                                worksheet.Cells["I" + (startRows)].Value = "";
                            }
                            worksheet.Cells["J" + (startRows)].Value = allowance.allowance_other;
                            if ((double)worksheet.Cells["J" + (startRows)].Value == 0)
                            {
                                worksheet.Cells["J" + (startRows)].Value = "";
                            }
                            worksheet.Cells["L" + (startRows)].Value = allowance.allowance_hostel;
                            if ((double)worksheet.Cells["L" + (startRows)].Value == 0)
                            {
                                worksheet.Cells["L" + (startRows)].Value = "";
                            }
                            worksheet.Cells["M" + (startRows)].Value = allowance.list;
                            worksheet.Cells["N" + (startRows)].Value = allowance.amount;
                            if ((double)worksheet.Cells["N" + (startRows)].Value == 0)
                            {
                                worksheet.Cells["N" + (startRows)].Value = "";
                            }

                            double sum = allowance.allowance_province + allowance.allowance_1_4 + allowance.allowance_4_8 + allowance.allowance_8 + allowance.allowance_other + allowance.allowance_hostel + allowance.amount;

                            worksheet.Cells["O" + (startRows)].Value = sum;
                            if ((double)worksheet.Cells["O" + (startRows)].Value == 0)
                            {
                                worksheet.Cells["O" + (startRows)].Value = "";
                            }
                            worksheet.Cells["P" + (startRows)].Value = allowance.remark;

                        }
                        startRows++;
                    }
                    worksheet.Cells["C5"].Value = emp.name_en;
                    worksheet.Cells["C44"].Value = emp.name_en;

                    worksheet.Cells["C46"].Value = DateTime.Now.ToString("dd/MM/yyyy");

                    p.SaveAs(stream);
                    stream.Position = 0;
                }
            }
            return stream;
        }

        public List<EmployeeModel> GetEmployeeAdmin(DateTime start_date, DateTime stop_date)
        {
            List<EmployeeModel> drivers = new List<EmployeeModel>();
            try
            {
                if (con_report.State == ConnectionState.Closed)
                {
                    con_report.Open();
                }
                string strCmd = string.Format($@"select DISTINCT EditAllowance.emp_id,
                                                        emp.name from EditAllowance 
                                                LEFT JOIN TRIP_EXPENSE.dbo.Employees emp ON EditAllowance.emp_id = emp.emp_id
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
    }
}
