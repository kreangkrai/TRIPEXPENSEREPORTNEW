using Microsoft.Data.SqlClient;
using System.Data;
using TRIPEXPENSEREPORT.Interface;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Service
{
    public class TripService : ITrip
    {
        ConnectSQL connect = null;
        SqlConnection con = null;
        public TripService()
        {
            connect = new ConnectSQL();
            con = connect.OpenConnect();
        }
        public List<DataModel> GetDatasALLByEMPID(string emp_id, DateTime start, DateTime end)
        {
            List<DataModel> trips = new List<DataModel>();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string strCmd = string.Format($@"SELECT '' AS car_id,
                                           driver,
                                           '' AS passenger,
                                           job_id,
                                           trip,
                                           date,
                                           status,
                                           distance,
                                           speed,
                                           latitude,
                                           longitude,
                                           accuracy,
                                           location_mode,
                                           location,
                                           zipcode,
                                           mileage,
                                           cash,
                                           0 AS fleetcard,
                                           '' AS borrower,
                                           'PERSONAL' AS mode
                                    FROM Personal 
                                    WHERE driver = @emp_id 
                                      AND date BETWEEN '{start.ToString("yyyy-MM-dd")}' AND '{end.ToString("yyyy-MM-dd")}' 
                                      AND status <> 'NA'

                                    UNION ALL

                                    SELECT car_id,
                                           driver,
                                           '' AS passenger,
                                           job_id,
                                           trip,
                                           date,
                                           status,
                                           distance,
                                           speed,
                                           latitude,
                                           longitude,
                                           accuracy,
                                           location_mode,
                                           location,
                                           zipcode,
                                           mileage,
                                           cash,
                                           fleetcard,
                                           borrower,
                                           'COMPANY' AS mode
                                    FROM Company 
                                    WHERE  driver = @emp_id 
                                      AND date BETWEEN '{start.ToString("yyyy-MM-dd")}' AND '{end.ToString("yyyy-MM-dd")}' 
                                      AND status <> 'NA'

                                    UNION ALL

                                    SELECT '' AS car_id,
                                           '' AS driver,
                                           passenger,
                                           job_id,
                                           trip,
                                           date,
                                           status,
                                           distance,
                                           speed,
                                           latitude,
                                           longitude,
                                           accuracy,
                                           location_mode,
                                           location,
                                           zipcode,
                                           0 AS mileage,
                                           0 AS cash,
                                           0 AS fleetcard,
                                           '' AS borrower,
                                           'PUBLIC' AS mode
                                    FROM [Public] 
                                    WHERE passenger = @emp_id 
                                      AND date BETWEEN '{start.ToString("yyyy-MM-dd")}' AND '{end.ToString("yyyy-MM-dd")}' 
                                      AND status <> 'NA'

                                    UNION ALL

                                    SELECT '' AS car_id,
                                           driver,
                                           passenger,
                                           job_id,
                                           trip,
                                           date,
                                           status,
                                           0 AS distance,
                                           0 AS speed,
                                           latitude,
                                           longitude,
                                           accuracy,
                                           location_mode,
                                           location,
                                           zipcode,
                                           0 AS mileage,
                                           0 AS cash,
                                           0 AS fleetcard,
                                           '' AS borrower,
                                           'PASSENGER PERSONAL' AS mode
                                    FROM Passenger_Personal 
                                    WHERE passenger = @emp_id 
                                      AND date BETWEEN '{start.ToString("yyyy-MM-dd")}' AND '{end.ToString("yyyy-MM-dd")}' 
                                      AND status <> 'NA'

                                    UNION ALL

                                    SELECT car_id,
                                           driver,
                                           passenger,
                                           job_id,
                                           trip,
                                           date,
                                           status,
                                           0 AS distance,
                                           0 AS speed,
                                           latitude,
                                           longitude,
                                           accuracy,
                                           location_mode,
                                           location,
                                           zipcode,
                                           0 AS mileage,
                                           0 AS cash,
                                           0 AS fleetcard,
                                           '' AS borrower,
                                           'PASSENGER COMPANY' AS mode
                                    FROM Passenger_Company 
                                    WHERE passenger = @emp_id 
                                      AND date BETWEEN '{start.ToString("yyyy-MM-dd")}' AND '{end.ToString("yyyy-MM-dd")}' 
                                      AND status <> 'NA';
                                    ");
                SqlCommand command = new SqlCommand(strCmd, con);
                command.Parameters.AddWithValue("@emp_id", emp_id);
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DataModel trip = new DataModel()
                        {
                            car_id = dr["car_id"].ToString(),
                            driver = dr["driver"].ToString(),
                            passenger = dr["passenger"].ToString(),
                            job_id = dr["job_id"].ToString(),
                            trip = dr["trip"].ToString(),
                            date = dr["date"] != DBNull.Value ? Convert.ToDateTime(dr["date"].ToString()) : DateTime.MinValue,
                            status = dr["status"].ToString(),
                            distance = dr["distance"] != DBNull.Value ? Convert.ToDouble(dr["distance"].ToString()) : 0,
                            speed = dr["speed"] != DBNull.Value ? Convert.ToDouble(dr["speed"].ToString()) : 0,
                            latitude = dr["latitude"] != DBNull.Value ? Convert.ToDouble(dr["latitude"].ToString()) : 0,
                            longitude = dr["longitude"] != DBNull.Value ? Convert.ToDouble(dr["longitude"].ToString()) : 0,
                            accuracy = dr["accuracy"] != DBNull.Value ? Convert.ToDouble(dr["accuracy"].ToString()) : 0,
                            location_mode = dr["location_mode"].ToString(),
                            location = dr["location"].ToString(),
                            zipcode = dr["zipcode"].ToString(),
                            mileage = dr["mileage"] != DBNull.Value ? Convert.ToInt32(dr["mileage"].ToString()) : 0,
                            cash = dr["cash"] != DBNull.Value ? Convert.ToDouble(dr["cash"].ToString()) : 0,
                            fleetcard = dr["fleetcard"] != DBNull.Value ? Convert.ToDouble(dr["fleetcard"].ToString()) : 0,
                            borrower = dr["borrower"].ToString(),
                            mode = dr["mode"].ToString()
                        };

                        trips.Add(trip);
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
            return trips;
        }
        public List<DataModel> GetDatasPersonalByEMPID(string emp_id, DateTime start, DateTime end)
        {
            List<DataModel> trips = new List<DataModel>();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string strCmd = string.Format($@"SELECT '' AS car_id,
                                           driver,
                                           '' AS passenger,
                                           job_id,
                                           trip,
                                           date,
                                           status,
                                           distance,
                                           speed,
                                           latitude,
                                           longitude,
                                           accuracy,
                                           location_mode,
                                           location,
                                           zipcode,
                                           mileage,
                                           cash,
                                           0 AS fleetcard,
                                           '' AS borrower,
                                           'PERSONAL' AS mode
                                    FROM Personal 
                                    WHERE driver = @emp_id 
                                      AND date BETWEEN '{start.ToString("yyyy-MM-dd")}' AND '{end.ToString("yyyy-MM-dd")}' 
                                      AND status <> 'NA';");
                SqlCommand command = new SqlCommand(strCmd, con);
                command.Parameters.AddWithValue("@emp_id", emp_id);
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DataModel trip = new DataModel()
                        {
                            car_id = dr["car_id"].ToString(),
                            driver = dr["driver"].ToString(),
                            passenger = dr["passenger"].ToString(),
                            job_id = dr["job_id"].ToString(),
                            trip = dr["trip"].ToString(),
                            date = dr["date"] != DBNull.Value ? Convert.ToDateTime(dr["date"].ToString()) : DateTime.MinValue,
                            status = dr["status"].ToString(),
                            distance = dr["distance"] != DBNull.Value ? Convert.ToDouble(dr["distance"].ToString()) : 0,
                            speed = dr["speed"] != DBNull.Value ? Convert.ToDouble(dr["speed"].ToString()) : 0,
                            latitude = dr["latitude"] != DBNull.Value ? Convert.ToDouble(dr["latitude"].ToString()) : 0,
                            longitude = dr["longitude"] != DBNull.Value ? Convert.ToDouble(dr["longitude"].ToString()) : 0,
                            accuracy = dr["accuracy"] != DBNull.Value ? Convert.ToDouble(dr["accuracy"].ToString()) : 0,
                            location_mode = dr["location_mode"].ToString(),
                            location = dr["location"].ToString(),
                            zipcode = dr["zipcode"].ToString(),
                            mileage = dr["mileage"] != DBNull.Value ? Convert.ToInt32(dr["mileage"].ToString()) : 0,
                            cash = dr["cash"] != DBNull.Value ? Convert.ToDouble(dr["cash"].ToString()) : 0,
                            fleetcard = dr["fleetcard"] != DBNull.Value ? Convert.ToDouble(dr["fleetcard"].ToString()) : 0,
                            borrower = dr["borrower"].ToString(),
                            mode = dr["mode"].ToString()
                        };

                        trips.Add(trip);
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
            return trips;
        }
        public List<DataModel> GetDatasCompnayByEMPID(string emp_id, DateTime start, DateTime end)
        {
            List<DataModel> trips = new List<DataModel>();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string strCmd = string.Format($@" SELECT car_id,
                                           driver,
                                           '' AS passenger,
                                           job_id,
                                           trip,
                                           date,
                                           status,
                                           distance,
                                           speed,
                                           latitude,
                                           longitude,
                                           accuracy,
                                           location_mode,
                                           location,
                                           zipcode,
                                           mileage,
                                           cash,
                                           fleetcard,
                                           borrower,
                                           'COMPANY' AS mode
                                    FROM Company 
                                    WHERE  driver = @emp_id 
                                      AND date BETWEEN '{start.ToString("yyyy-MM-dd")}' AND '{end.ToString("yyyy-MM-dd")}' 
                                      AND status <> 'NA';");
                SqlCommand command = new SqlCommand(strCmd, con);
                command.Parameters.AddWithValue("@emp_id", emp_id);
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DataModel trip = new DataModel()
                        {
                            car_id = dr["car_id"].ToString(),
                            driver = dr["driver"].ToString(),
                            passenger = dr["passenger"].ToString(),
                            job_id = dr["job_id"].ToString(),
                            trip = dr["trip"].ToString(),
                            date = dr["date"] != DBNull.Value ? Convert.ToDateTime(dr["date"].ToString()) : DateTime.MinValue,
                            status = dr["status"].ToString(),
                            distance = dr["distance"] != DBNull.Value ? Convert.ToDouble(dr["distance"].ToString()) : 0,
                            speed = dr["speed"] != DBNull.Value ? Convert.ToDouble(dr["speed"].ToString()) : 0,
                            latitude = dr["latitude"] != DBNull.Value ? Convert.ToDouble(dr["latitude"].ToString()) : 0,
                            longitude = dr["longitude"] != DBNull.Value ? Convert.ToDouble(dr["longitude"].ToString()) : 0,
                            accuracy = dr["accuracy"] != DBNull.Value ? Convert.ToDouble(dr["accuracy"].ToString()) : 0,
                            location_mode = dr["location_mode"].ToString(),
                            location = dr["location"].ToString(),
                            zipcode = dr["zipcode"].ToString(),
                            mileage = dr["mileage"] != DBNull.Value ? Convert.ToInt32(dr["mileage"].ToString()) : 0,
                            cash = dr["cash"] != DBNull.Value ? Convert.ToDouble(dr["cash"].ToString()) : 0,
                            fleetcard = dr["fleetcard"] != DBNull.Value ? Convert.ToDouble(dr["fleetcard"].ToString()) : 0,
                            borrower = dr["borrower"].ToString(),
                            mode = dr["mode"].ToString()
                        };

                        trips.Add(trip);
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
            return trips;
        }
    }
}
