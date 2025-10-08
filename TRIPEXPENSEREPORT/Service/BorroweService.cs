using Microsoft.Data.SqlClient;
using System.Data;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Service
{
    public class BorroweService : IBorrow
    {
        ConnectSQL connect = null;
        SqlConnection con = null;
        public BorroweService()
        {
            connect = new ConnectSQL();
            con = connect.OpenConnect();
        }
        public string Delete(string borrow_id)
        {
            
            try
            {              
                string string_command = string.Format($@"DELETE FROM Borrowers WHERE borrow_id = @borrow_id");
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                using (SqlCommand cmd = new SqlCommand(string_command, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@borrow_id", borrow_id);
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

        public List<BorrowerModel> GetBorrowers()
        {
            List<BorrowerModel> borrowers = new List<BorrowerModel>();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(@"SELECT Borrowers.borrow_id,
                                                    Borrowers.car_id,
	                                                Cars.License_Plate as license_plate,
	                                                Borrowers.job_id,
	                                                Borrowers.mileage_start,
                                                    Borrowers.main_location,
	                                                Borrowers.customer,
	                                                Borrowers.borrower,
                                                    emp1.name as borrower_name,
	                                                Borrowers.borrow_date,
	                                                Borrowers.plan_return_date,           
	                                                Borrowers.status,
	                                                Borrowers.admin,
                                                    emp2.name as admin_name
                                                FROM Borrowers
                                                LEFT JOIN Cars ON Cars.car_id = Borrowers.car_id
                                                LEFT JOIN Employees emp1 ON emp1.emp_id = Borrowers.borrower
                                                LEFT JOIN Employees emp2 ON emp2.emp_id = Borrowers.admin", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        BorrowerModel borrower = new BorrowerModel()
                        {
                            borrow_id = dr["borrow_id"].ToString(),
                            main_location = dr["main_location"].ToString(),
                            car_id = dr["car_id"].ToString(),
                            license_plate = dr["license_plate"].ToString(),
                            job_id = dr["job_id"].ToString(),
                            mileage_start = dr["mileage_start"] != DBNull.Value ? Convert.ToInt32(dr["mileage_start"].ToString()) : 0,
                            customer = dr["customer"].ToString(),
                            borrower = dr["borrower"].ToString(),
                            borrower_name = dr["borrower_name"].ToString(),
                            borrow_date = dr["borrow_date"] != DBNull.Value ? Convert.ToDateTime(dr["borrow_date"].ToString()) : DateTime.MinValue,
                            plan_return_date = dr["plan_return_date"] != DBNull.Value ? Convert.ToDateTime(dr["plan_return_date"].ToString()) : DateTime.MinValue,
                            actual_return_date = DateTime.MinValue,
                            status = dr["status"].ToString(),
                            remark = "",
                            admin = dr["admin"].ToString(),
                            admin_name = dr["admin_name"].ToString(),
                        };
                        borrowers.Add(borrower);
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
            return borrowers;
        }

        public List<BorrowerModel> GetBorrowersLog()
        {
            List<BorrowerModel> borrowers = new List<BorrowerModel>();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(@"SELECT Borrowers_Log.borrow_id,
                                                    Borrowers_Log.car_id,
	                                                Cars.License_Plate as license_plate,
	                                                Borrowers_Log.job_id,
	                                                Borrowers_Log.mileage_start,
	                                                Borrowers_Log.customer,
	                                                Borrowers_Log.borrower,
													emp1.name as borrower_name,
	                                                Borrowers_Log.borrow_date,
	                                                Borrowers_Log.plan_return_date,
													Borrowers_Log.actual_return_date, 
	                                                Borrowers_Log.status,
	                                                Borrowers_Log.admin,
													emp2.name as admin_name,
													Borrowers_Log.remark
                                                FROM Borrowers_Log
                                                LEFT JOIN Cars ON Cars.car_id = Borrowers_Log.car_id
												LEFT JOIN Employees emp1 ON emp1.emp_id = Borrowers_Log.borrower
												LEFT JOIN Employees emp2 ON emp2.emp_id = Borrowers_Log.admin", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        BorrowerModel borrower = new BorrowerModel()
                        {
                            borrow_id = dr["borrow_id"].ToString(),
                            main_location = dr["main_location"].ToString(),
                            car_id = dr["car_id"].ToString(),
                            license_plate = dr["license_plate"].ToString(),
                            job_id = dr["job_id"].ToString(),
                            mileage_start = dr["mileage_start"] != DBNull.Value ? Convert.ToInt32(dr["mileage_start"].ToString()) : 0,
                            customer = dr["customer"].ToString(),
                            borrower = dr["borrower"].ToString(),
                            borrower_name = dr["borrower_name"].ToString(),
                            borrow_date = dr["borrow_date"] != DBNull.Value ? Convert.ToDateTime(dr["borrow_date"].ToString()) : DateTime.MinValue,
                            plan_return_date = dr["plan_return_date"] != DBNull.Value ? Convert.ToDateTime(dr["plan_return_date"].ToString()) : DateTime.MinValue,
                            actual_return_date = dr["actual_return_date"] != DBNull.Value ? Convert.ToDateTime(dr["actual_return_date"].ToString()) : DateTime.MinValue,
                            status = dr["status"].ToString(),
                            remark = dr["remark"].ToString(),
                            admin = dr["admin"].ToString(),
                            admin_name = dr["admin_name"].ToString(),
                        };
                        borrowers.Add(borrower);
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
            return borrowers;
        }

        public string Insert(BorrowerModel borrower)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string string_command = string.Format($@"
                    INSERT INTO 
                        Borrowers(borrow_id,car_id,job_id,mileage_start,main_location,customer,borrower,borrow_date,plan_return_date,status,admin
                        )
                        VALUES(@borrow_id,@car_id,@job_id,@mileage_start,@main_location,@customer,@borrower,@borrow_date,@plan_return_date,@status,@admin
                        )");
                using (SqlCommand cmd = new SqlCommand(string_command, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@borrow_id", borrower.borrow_id);
                    cmd.Parameters.AddWithValue("@car_id", borrower.car_id);
                    cmd.Parameters.AddWithValue("@job_id", borrower.job_id);
                    cmd.Parameters.AddWithValue("@mileage_start", borrower.mileage_start);
                    cmd.Parameters.AddWithValue("@main_location", borrower.main_location);
                    cmd.Parameters.AddWithValue("@customer", borrower.customer);
                    cmd.Parameters.AddWithValue("@borrower", borrower.borrower);
                    cmd.Parameters.AddWithValue("@borrow_date", borrower.borrow_date);
                    cmd.Parameters.AddWithValue("@plan_return_date", borrower.plan_return_date);
                    cmd.Parameters.AddWithValue("@status", borrower.status);
                    cmd.Parameters.AddWithValue("@admin", borrower.admin);
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

        public string InsertLog(BorrowerModel borrower)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string string_command = string.Format($@"
                    INSERT INTO 
                        Borrowers_Log(borrow_id,car_id,job_id,mileage_start,main_location,customer,borrower,borrow_date,plan_return_date,actual_return_date,status,admin,remark
                        )
                        VALUES(@borrow_id,@car_id,@job_id,@mileage_start,@main_location,@customer,@borrower,@borrow_date,@plan_return_date,@actual_return_date,@status,@admin,@remark
                        )");
                using (SqlCommand cmd = new SqlCommand(string_command, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@borrow_id", borrower.borrow_id);
                    cmd.Parameters.AddWithValue("@car_id", borrower.car_id);
                    cmd.Parameters.AddWithValue("@job_id", borrower.job_id);
                    cmd.Parameters.AddWithValue("@mileage_start", borrower.mileage_start);
                    cmd.Parameters.AddWithValue("@main_location", borrower.main_location);
                    cmd.Parameters.AddWithValue("@customer", borrower.customer);
                    cmd.Parameters.AddWithValue("@borrower", borrower.borrower);
                    cmd.Parameters.AddWithValue("@borrow_date", borrower.borrow_date);
                    cmd.Parameters.AddWithValue("@plan_return_date", borrower.plan_return_date);
                    cmd.Parameters.AddWithValue("@actual_return_date", borrower.actual_return_date);
                    cmd.Parameters.AddWithValue("@status", borrower.status);
                    cmd.Parameters.AddWithValue("@admin", borrower.admin);
                    cmd.Parameters.AddWithValue("@remark", borrower.remark);
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
        public string Update(BorrowerModel borrower)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string string_command = string.Format($@"
                    UPDATE Borrowers SET
                                    car_id = @car_id,
                                    job_id = @job_id,
                                    mileage_start = @mileage_start,
                                    main_location = @main_location,
                                    customer = @customer,
                                    borrower = @borrower,
                                    borrow_date = @borrow_date,
                                    plan_return_date = @plan_return_date,
                                    status = @status,
                                    admin = @admin
                                    WHERE borrow_id = @borrow_id");
                using (SqlCommand cmd = new SqlCommand(string_command, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@borrow_id", borrower.borrow_id);
                    cmd.Parameters.AddWithValue("@car_id", borrower.car_id);
                    cmd.Parameters.AddWithValue("@job_id", borrower.job_id);
                    cmd.Parameters.AddWithValue("@mileage_start", borrower.mileage_start);
                    cmd.Parameters.AddWithValue("@main_location", borrower.main_location);
                    cmd.Parameters.AddWithValue("@customer", borrower.customer);
                    cmd.Parameters.AddWithValue("@borrower", borrower.borrower);
                    cmd.Parameters.AddWithValue("@borrow_date", borrower.borrow_date);
                    cmd.Parameters.AddWithValue("@plan_return_date", borrower.plan_return_date);
                    cmd.Parameters.AddWithValue("@status", borrower.status);
                    cmd.Parameters.AddWithValue("@admin", borrower.admin);
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
    }
}
