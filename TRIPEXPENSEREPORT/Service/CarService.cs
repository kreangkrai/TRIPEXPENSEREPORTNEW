using Microsoft.Data.SqlClient;
using System.Data;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Service
{
    public class CarService : ICar
    {
        ConnectSQL connect = null;
        SqlConnection con = null;
        public CarService()
        {
            connect = new ConnectSQL();
            con = connect.OpenConnect();
        }
        public List<CarModel> GetCars()
        {
            List<CarModel> cars = new List<CarModel>();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string strCmd = string.Format($@"SELECT [car_id]
                                                      ,[license_plate]
                                                      ,[brand]
                                                      ,[fleet_card_no]
                                                      ,[balance]
                                                  FROM [TRIP_EXPENSE].[dbo].[Cars]");
                SqlCommand command = new SqlCommand(strCmd, con);
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CarModel car = new CarModel()
                        {
                            car_id = dr["car_id"].ToString(),
                            license_plate = dr["license_plate"].ToString(),
                            brand = dr["brand"].ToString(),
                            fleet_card_no = dr["fleet_card_no"].ToString(),
                            balance = dr["balance"] != DBNull.Value ? Convert.ToDouble(dr["balance"].ToString()) : 0,
                        };
                        cars.Add(car);
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
            return cars;
        }

        public string Insert(CarModel car)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string string_command = string.Format($@"
                    INSERT INTO 
                        Cars(car_id,
                            license_plate,
                            brand,
                            fleet_card_no,
                            balance
                        )
                        VALUES(@car_id,
                            @license_plate,
                            @brand,
                            @fleet_card_no,
                            @balance
                        )");

                using (SqlCommand cmd = new SqlCommand(string_command, con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@car_id", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@license_plate", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@brand", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@fleet_card_no", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@balance", SqlDbType.Float);

                    cmd.Parameters["@car_id"].Value = car.car_id ?? (object)DBNull.Value;
                    cmd.Parameters["@license_plate"].Value = car.license_plate ?? (object)DBNull.Value;
                    cmd.Parameters["@brand"].Value = car.brand ?? (object)DBNull.Value;
                    cmd.Parameters["@fleet_card_no"].Value = car.fleet_card_no ?? (object)DBNull.Value;
                    cmd.Parameters["@balance"].Value = car.balance;

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
        public string Update(CarModel car)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string string_command = string.Format($@"
                    UPDATE Cars SET
                            license_plate = @license_plate,
                            brand = @brand,
                            fleet_card_no = @fleet_card_no,
                            balance = @balance
                        WHERE car_id = @car_id");

                using (SqlCommand cmd = new SqlCommand(string_command, con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@car_id", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@license_plate", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@brand", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@fleet_card_no", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@balance", SqlDbType.Float);

                    cmd.Parameters["@car_id"].Value = car.car_id ?? (object)DBNull.Value;
                    cmd.Parameters["@license_plate"].Value = car.license_plate ?? (object)DBNull.Value;
                    cmd.Parameters["@brand"].Value = car.brand ?? (object)DBNull.Value;
                    cmd.Parameters["@fleet_card_no"].Value = car.fleet_card_no ?? (object)DBNull.Value;
                    cmd.Parameters["@balance"].Value = car.balance;

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
        public string Delete(string car)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string string_command = string.Format($@"
                    DELETE FROM Cars WHERE car_id = @car_id");

                using (SqlCommand cmd = new SqlCommand(string_command, con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@car_id", SqlDbType.NVarChar);

                    cmd.Parameters["@car_id"].Value = car ?? (object)DBNull.Value;

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
