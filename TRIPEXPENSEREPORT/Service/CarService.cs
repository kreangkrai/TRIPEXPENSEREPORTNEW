using Microsoft.Data.SqlClient;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Service
{
    public class CarService : ICar
    {
        public List<CarModel> GetCars()
        {
            List<CarModel> cars = new List<CarModel>();
            try
            {
                string strCmd = string.Format($@"SELECT [car_id]
                                                      ,[license_plate]
                                                      ,[brand]
                                                      ,[fleet_card_no]
                                                      ,[balance]
                                                  FROM [TRIP_EXPENSE].[dbo].[Cars]");
                SqlCommand command = new SqlCommand(strCmd, ConnectSQL.OpenConnect());
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
                if (ConnectSQL.con.State == System.Data.ConnectionState.Open)
                {
                    ConnectSQL.CloseConnect();
                }
            }
            return cars;
        }
    }
}
