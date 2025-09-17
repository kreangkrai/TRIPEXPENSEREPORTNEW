using Microsoft.Data.SqlClient;
using System.Data;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Service
{
    public class AllowanceService : IAllowance
    {
        private IArea Area;
        private IUser User;
        public AllowanceService(IArea area,IUser user)
        {
            Area = area;
            User = user;
        }
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

        public List<AllowanceModel> CalculateAllowanceNew(string emp_id, List<DataTripModel> trips , DateTime start, DateTime stop)
        {
            List<AllowanceModel> dataAllowances = new List<AllowanceModel>();

            List<UserManagementModel> users = User.GetUsers();
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
                string code = Guid.NewGuid().ToString("N");

                if (_trips.Count > 0) // Have Trip each date
                {
                    #region Outside Zone
                    List<DataTripModel> AllZone = _trips.Where(w => area_location.Contains(w.zipcode.Substring(0, 2))).ToList();
                        //Check All CheckIn Zone.
                    zone = AllZone.Count > 0 ? true : false;

                    if (!zone)
                    {
                        Allowance_Outside_Region = 100;
                    }
                    #endregion
                    DateTime first_start = _trips.Where(w => w.date.Date == date.Date).FirstOrDefault().date;
                    DateTime last_stop = _trips.Where(w => w.date.Date == date.Date).LastOrDefault().date;
                    double time_duration = (last_stop - first_start).TotalMinutes;

                    if (zone) // In Zone
                    {
                        if (location == "rbo")
                        {
                            Allowance1_4 = 50;
                            Allowance4_8 = 70;
                            Allowance8 = 80;
                        }
                        else  // HQ , KBO
                        {
                            bool check_customer = _trips.Any(a => a.location_mode == "CUSTOMER"); // Check Customer In Trip

                            if (check_customer)
                            {
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
                            }
                            else
                            {                               
                                double sum_time_duration = 0.0;
                                for (int i = 0; i < _trips.Count; i++)
                                {
                                    var start_ = first_start;
                                    var stop_ = last_stop;
                                    sum_time_duration += (stop_ - start_).TotalMinutes;
                                }

                                if (sum_time_duration >= 60 && sum_time_duration < 240)
                                {
                                    Allowance1_4 = 50;
                                }
                                else if (sum_time_duration >= 240 && sum_time_duration < 480)
                                {
                                    Allowance1_4 = 50;
                                    Allowance4_8 = 70;
                                }
                                else if (sum_time_duration >= 480)
                                {
                                    Allowance1_4 = 50;
                                    Allowance4_8 = 70;
                                    Allowance8 = 80;
                                }
                            }
                        }
                    }
                    else if (!zone) // Out Zone
                    {
                        Allowance1_4 = 50;
                        Allowance4_8 = 70;
                        Allowance8 = 80;
                    }
                    else // Have In and Out
                    {
                        if (location == "rbo")
                        {
                            Allowance1_4 = 50;
                            Allowance4_8 = 70;
                            Allowance8 = 80;
                        }
                        else  // HQ , KBO
                        {
                            bool check_customer = _trips.Any(a=>a.location_mode == "CUSTOMER"); // Check Customer

                            if (check_customer)
                            {
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
                            }
                            else
                            {                              
                                double sum_time_duration = 0.0;
                                for (int i = 0; i < _trips.Count; i++)
                                {
                                    var start_ = first_start;
                                    var stop_ = last_stop;
                                    sum_time_duration += (stop_ - start_).TotalMinutes;
                                }

                                if (sum_time_duration >= 60 && sum_time_duration < 240)
                                {
                                    Allowance1_4 = 50;
                                }
                                else if (sum_time_duration >= 240 && sum_time_duration < 480)
                                {
                                    Allowance1_4 = 50;
                                    Allowance4_8 = 70;
                                }
                                else if (sum_time_duration >= 480)
                                {
                                    Allowance1_4 = 50;
                                    Allowance4_8 = 70;
                                    Allowance8 = 80;
                                }
                            }
                        }
                    }

                    double Allowance_Sum = Allowance_Outside_Region + Allowance1_4 + Allowance4_8 + Allowance8;

                    var AllDay = _trips.Select(s => s.location).ToHashSet().ToArray();
                    string joinTrip = AllDay != null ? string.Join(",", AllDay.Where(w => w != "").ToArray()) : "";

                    var AllMode = _trips.Select(s => s.mode).ToHashSet().ToArray();

                    string joinMode = AllMode != null ? string.Join(",", AllMode) : "";
                    
                    AllowanceModel allowance = new AllowanceModel()
                    {
                        emp_id = emp_id,
                        date = date.Date,
                        customer = joinTrip,
                        job = _trips.FirstOrDefault().job_id,
                        allowance_province = Allowance_Outside_Region,
                        allowance_1_4 = Allowance1_4,
                        allowance_4_8 = Allowance4_8,
                        allowance_8 = Allowance8,
                        allowance_hostel = 0,
                        allowance_other = 0,
                        amount = Allowance_Sum,
                        zipcode = _trips.FirstOrDefault().zipcode,
                        approver = "",
                        status = "IN PROGRESS",
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
                        date = date.Date,
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
                        status = "IN PROGRESS",
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
    }
}
