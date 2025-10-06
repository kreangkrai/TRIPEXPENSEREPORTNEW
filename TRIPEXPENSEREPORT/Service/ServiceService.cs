using Microsoft.Data.SqlClient;
using System.Data;
using TRIPEXPENSEREPORT.Interface;

namespace TRIPEXPENSEREPORT.Service
{
    public class ServiceService : IService
    {
        public List<ServiceTypeModel> GetServiceTypes()
        {
            List<ServiceTypeModel> services = new List<ServiceTypeModel>();
            try
            {
                SqlCommand cmd = new SqlCommand(@"SELECT [service_id]
                                                  ,[service_name]
                                                  ,[due_mileage]
                                                  ,[due_date]
                                              FROM [gps_sale_tracking].[dbo].[ServiceType]", ConnectSQL.OpenReportConnect());
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ServiceTypeModel service = new ServiceTypeModel()
                        {
                            service_id = dr["service_id"].ToString(),
                            service_name = dr["service_name"].ToString(),
                            due_mileage = dr["due_mileage"] != DBNull.Value ? Convert.ToInt32(dr["due_mileage"].ToString()) : 0,
                            due_date = dr["due_date"] != DBNull.Value ? Convert.ToInt32(dr["due_date"].ToString()) : 0,
                        };
                        services.Add(service);
                    }
                    dr.Close();
                }
            }
            finally
            {
                if (ConnectSQL.con_report.State == ConnectionState.Open)
                {
                    ConnectSQL.CloseReportConnect();
                }
            }
            return services;
        }

        public ServiceModel GetSeviceByCar(string car_id, string service_id)
        {
            ServiceModel service = new ServiceModel();
            try
            {
                SqlCommand cmd = new SqlCommand($@"SELECT ServiceCar.car_id,
                                                [TRIP_EXPENSE].dbo.Cars.License_Plate as license_plate,
	                                            ServiceCar.service_id,
	                                            ServiceType.service_name,
                                                current_mileage,
	                                            mileage_at_service,
	                                            date_at_service,
	                                            appointment_mileage,
	                                            appointment_date,
	                                            next_appointment_mileage,
	                                            next_appointment_date
                                            FROM ServiceCar
                                            LEFT JOIN ServiceType ON ServiceType.service_id = ServiceCar.service_id
                                            LEFT JOIN [TRIP_EXPENSE].dbo.Cars ON Cars.car_id = ServiceCar.car_id
                                            WHERE ServiceCar.car_id = '{car_id}' AND ServiceCar.service_id = '{service_id}'", ConnectSQL.OpenReportConnect());
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        service = new ServiceModel()
                        {
                            car_id = dr["car_id"].ToString(),
                            service_id = dr["service_id"].ToString(),
                            license_plate = dr["license_plate"].ToString(),
                            service_name = dr["service_name"].ToString(),
                            current_mileage = dr["current_mileage"] != DBNull.Value ? Convert.ToInt32(dr["current_mileage"].ToString()) : 0,
                            mileage_at_service = dr["mileage_at_service"] != DBNull.Value ? Convert.ToInt32(dr["mileage_at_service"].ToString()) : 0,
                            date_at_service = dr["date_at_service"] != DBNull.Value ? Convert.ToDateTime(dr["date_at_service"].ToString()) : DateTime.MinValue,
                            appointment_mileage = dr["appointment_mileage"] != DBNull.Value ? Convert.ToInt32(dr["appointment_mileage"].ToString()) : 0,
                            appointment_date = dr["appointment_date"] != DBNull.Value ? Convert.ToDateTime(dr["appointment_date"].ToString()) : DateTime.MinValue,
                            next_appointment_mileage = dr["next_appointment_mileage"] != DBNull.Value ? Convert.ToInt32(dr["next_appointment_mileage"].ToString()) : 0,
                            next_appointment_date = dr["next_appointment_date"] != DBNull.Value ? Convert.ToDateTime(dr["next_appointment_date"].ToString()) : DateTime.MinValue
                        };
                    }
                    dr.Close();
                }
            }
            finally
            {
                if (ConnectSQL.con_report.State == ConnectionState.Open)
                {
                    ConnectSQL.CloseReportConnect();
                }
            }
            return service;
        }

        public List<ServiceModel> GetSevices()
        {
            List<ServiceModel> services = new List<ServiceModel>();
            try
            {
                SqlCommand cmd = new SqlCommand(@"SELECT ServiceCar.car_id,
                                                [TRIP_EXPENSE].dbo.Cars.License_Plate as license_plate,
	                                            ServiceCar.service_id,
	                                            ServiceType.service_name,
                                                current_mileage,
	                                            mileage_at_service,
	                                            date_at_service,
	                                            appointment_mileage,
	                                            appointment_date,
	                                            next_appointment_mileage,
	                                            next_appointment_date
                                            FROM ServiceCar
                                            LEFT JOIN ServiceType ON ServiceType.service_id = ServiceCar.service_id
                                            LEFT JOIN [TRIP_EXPENSE].dbo.Cars ON Cars.car_id = ServiceCar.car_id
                                            ORDER BY ServiceCar.car_id", ConnectSQL.OpenReportConnect());
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ServiceModel service = new ServiceModel()
                        {
                            car_id = dr["car_id"].ToString(),
                            license_plate = dr["license_plate"].ToString(),
                            service_id = dr["service_id"].ToString(),
                            service_name = dr["service_name"].ToString(),
                            current_mileage = dr["current_mileage"] != DBNull.Value ? Convert.ToInt32(dr["current_mileage"].ToString()) : 0,
                            mileage_at_service = dr["mileage_at_service"] != DBNull.Value ? Convert.ToInt32(dr["mileage_at_service"].ToString()) : 0,
                            date_at_service = dr["date_at_service"] != DBNull.Value ? Convert.ToDateTime(dr["date_at_service"].ToString()) : DateTime.MinValue,
                            appointment_mileage = dr["appointment_mileage"] != DBNull.Value ? Convert.ToInt32(dr["appointment_mileage"].ToString()) : 0,
                            appointment_date = dr["appointment_date"] != DBNull.Value ? Convert.ToDateTime(dr["appointment_date"].ToString()) : DateTime.MinValue,
                            next_appointment_mileage = dr["next_appointment_mileage"] != DBNull.Value ? Convert.ToInt32(dr["next_appointment_mileage"].ToString()) : 0,
                            next_appointment_date = dr["next_appointment_date"] != DBNull.Value ? Convert.ToDateTime(dr["next_appointment_date"].ToString()) : DateTime.MinValue
                        };
                        services.Add(service);
                    }
                    dr.Close();
                }
            }
            finally
            {
                if (ConnectSQL.con_report.State == ConnectionState.Open)
                {
                    ConnectSQL.CloseReportConnect();
                }
            }
            return services;
        }

        public List<ServiceModel> GetSevicesByService(string _service)
        {
            List<ServiceModel> services = new List<ServiceModel>();
            try
            {
                SqlCommand cmd = new SqlCommand($@"SELECT ServiceCar.car_id,
                                                [TRIP_EXPENSE].dbo.Cars.License_Plate as license_plate,
	                                            ServiceCar.service_id,
	                                            ServiceType.service_name,
                                                current_mileage,
	                                            mileage_at_service,
	                                            date_at_service,
	                                            appointment_mileage,
	                                            appointment_date,
	                                            next_appointment_mileage,
	                                            next_appointment_date
                                            FROM ServiceCar
                                            LEFT JOIN ServiceType ON ServiceType.service_id = ServiceCar.service_id
                                            LEFT JOIN [TRIP_EXPENSE].dbo.Cars ON Cars.car_id = ServiceCar.car_id
                                            WHERE ServiceCar.service_id = '{_service}'
                                            ORDER BY car_id", ConnectSQL.OpenReportConnect());
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ServiceModel service = new ServiceModel()
                        {
                            car_id = dr["car_id"].ToString(),
                            license_plate = dr["license_plate"].ToString(),
                            service_id = dr["service_id"].ToString(),
                            service_name = dr["service_name"].ToString(),
                            current_mileage = dr["current_mileage"] != DBNull.Value ? Convert.ToInt32(dr["current_mileage"].ToString()) : 0,
                            mileage_at_service = dr["mileage_at_service"] != DBNull.Value ? Convert.ToInt32(dr["mileage_at_service"].ToString()) : 0,
                            date_at_service = dr["date_at_service"] != DBNull.Value ? Convert.ToDateTime(dr["date_at_service"].ToString()) : DateTime.MinValue,
                            appointment_mileage = dr["appointment_mileage"] != DBNull.Value ? Convert.ToInt32(dr["appointment_mileage"].ToString()) : 0,
                            appointment_date = dr["appointment_date"] != DBNull.Value ? Convert.ToDateTime(dr["appointment_date"].ToString()) : DateTime.MinValue,
                            next_appointment_mileage = dr["next_appointment_mileage"] != DBNull.Value ? Convert.ToInt32(dr["next_appointment_mileage"].ToString()) : 0,
                            next_appointment_date = dr["next_appointment_date"] != DBNull.Value ? Convert.ToDateTime(dr["next_appointment_date"].ToString()) : DateTime.MinValue
                        };
                        services.Add(service);
                    }
                    dr.Close();
                }
            }
            finally
            {
                if (ConnectSQL.con_report.State == ConnectionState.Open)
                {
                    ConnectSQL.CloseReportConnect();
                }
            }
            return services;
        }

        public List<ServiceModel> GetSevicesHistory()
        {
            List<ServiceModel> services = new List<ServiceModel>();
            try
            {
                SqlCommand cmd = new SqlCommand(@"SELECT 
	                                                ServiceCarHistory.car_id,
                                                    [TRIP_EXPENSE].dbo.Cars.License_Plate as license_plate,
	                                                ServiceCarHistory.service_id,
	                                                ServiceType.service_name,
	                                                mileage_at_service,
	                                                date_at_service,
	                                                appointment_mileage,
	                                                appointment_date,
	                                                detail,
	                                                note,
	                                                location_service,
	                                                name
                                                FROM ServiceCarHistory
                                                LEFT JOIN ServiceType ON ServiceType.service_id = ServiceCarHistory.service_id
                                                LEFT JOIN [TRIP_EXPENSE].dbo.Cars ON Cars.car_id = ServiceCarHistory.car_id
                                                ORDER BY car_id", ConnectSQL.OpenReportConnect());
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ServiceModel service = new ServiceModel()
                        {
                            car_id = dr["car_id"].ToString(),
                            license_plate = dr["license_plate"].ToString(),
                            service_id = dr["service_id"].ToString(),
                            service_name = dr["service_name"].ToString(),
                            mileage_at_service = dr["mileage_at_service"] != DBNull.Value ? Convert.ToInt32(dr["mileage_at_service"].ToString()) : 0,
                            date_at_service = dr["date_at_service"] != DBNull.Value ? Convert.ToDateTime(dr["date_at_service"].ToString()) : DateTime.MinValue,
                            appointment_mileage = dr["appointment_mileage"] != DBNull.Value ? Convert.ToInt32(dr["appointment_mileage"].ToString()) : 0,
                            appointment_date = dr["appointment_date"] != DBNull.Value ? Convert.ToDateTime(dr["appointment_date"].ToString()) : DateTime.MinValue,
                            detail = dr["detail"].ToString(),
                            note = dr["note"].ToString(),
                            location_service = dr["location_service"].ToString(),
                            name = dr["name"].ToString()
                        };
                        services.Add(service);
                    }
                    dr.Close();
                }
            }
            finally
            {
                if (ConnectSQL.con_report.State == ConnectionState.Open)
                {
                    ConnectSQL.CloseReportConnect();
                }
            }
            return services;
        }

        public List<ServiceModel> GetSevicesHistoryByService(string _service)
        {
            List<ServiceModel> services = new List<ServiceModel>();
            try
            {
                SqlCommand cmd = new SqlCommand($@"SELECT 
	                                                ServiceCarHistory.car_id,
                                                    [TRIP_EXPENSE].dbo.Cars.License_Plate as license_plate,
	                                                ServiceCarHistory.service_id,
	                                                ServiceType.service_name,
	                                                mileage_at_service,
	                                                date_at_service,
	                                                appointment_mileage,
	                                                appointment_date,
	                                                detail,
	                                                note,
	                                                location_service,
	                                                name
                                                FROM ServiceCarHistory
                                                LEFT JOIN ServiceType ON ServiceType.service_id = ServiceCarHistory.service_id
                                                LEFT JOIN [TRIP_EXPENSE].dbo.Cars ON Cars.car_id = ServiceCarHistory.car_id
                                                WHERE ServiceCarHistory.service_id = '{_service}'
                                                ORDER BY car_id", ConnectSQL.OpenReportConnect());
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ServiceModel service = new ServiceModel()
                        {
                            car_id = dr["car_id"].ToString(),
                            license_plate = dr["license_plate"].ToString(),
                            service_id = dr["service_id"].ToString(),
                            service_name = dr["service_name"].ToString(),
                            mileage_at_service = dr["mileage_at_service"] != DBNull.Value ? Convert.ToInt32(dr["mileage_at_service"].ToString()) : 0,
                            date_at_service = dr["date_at_service"] != DBNull.Value ? Convert.ToDateTime(dr["date_at_service"].ToString()) : DateTime.MinValue,
                            appointment_mileage = dr["appointment_mileage"] != DBNull.Value ? Convert.ToInt32(dr["appointment_mileage"].ToString()) : 0,
                            appointment_date = dr["appointment_date"] != DBNull.Value ? Convert.ToDateTime(dr["appointment_date"].ToString()) : DateTime.MinValue,
                            detail = dr["detail"].ToString(),
                            note = dr["note"].ToString(),
                            location_service = dr["location_service"].ToString(),
                            name = dr["name"].ToString()
                        };
                        services.Add(service);
                    }
                    dr.Close();
                }
            }
            finally
            {
                if (ConnectSQL.con_report.State == ConnectionState.Open)
                {
                    ConnectSQL.CloseReportConnect();
                }
            }
            return services;
        }

        public string InsertService(ServiceModel service)
        {
            try
            {
                string string_command = string.Format($@"
                    INSERT INTO 
                        ServiceCar(car_id,service_id,mileage_at_service,date_at_service,appointment_mileage,appointment_date,next_appointment_mileage,next_appointment_date
                        )
                        VALUES(@car_id,@service_id,@mileage_at_service,@date_at_service,@appointment_mileage,@appointment_date,@next_appointment_mileage,@next_appointment_date
                        )");
                using (SqlCommand cmd = new SqlCommand(string_command, ConnectSQL.OpenReportConnect()))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@car_id", service.car_id);
                    cmd.Parameters.AddWithValue("@service_id", service.service_id);
                    cmd.Parameters.AddWithValue("@mileage_at_service", service.mileage_at_service);
                    cmd.Parameters.AddWithValue("@date_at_service", service.date_at_service);
                    cmd.Parameters.AddWithValue("@appointment_mileage", service.appointment_mileage);
                    cmd.Parameters.AddWithValue("@appointment_date", service.appointment_date);
                    cmd.Parameters.AddWithValue("@next_appointment_mileage", service.next_appointment_mileage);
                    cmd.Parameters.AddWithValue("@next_appointment_date", service.next_appointment_date);

                    if (ConnectSQL.con_report.State != ConnectionState.Open)
                    {
                        ConnectSQL.CloseReportConnect();
                        ConnectSQL.OpenReportConnect();
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
                if (ConnectSQL.con_report.State == ConnectionState.Open)
                {
                    ConnectSQL.CloseReportConnect();
                }
            }
            return "Success";
        }

        public string InsertServiceHistory(ServiceModel service)
        {
            try
            {
                string string_command = string.Format($@"
                    INSERT INTO 
                        ServiceCarHistory(car_id,service_id,mileage_at_service,date_at_service,appointment_mileage,appointment_date,detail,note,location_service,name
                        )
                        VALUES(@car_id,@service_id,@mileage_at_service,@date_at_service,@appointment_mileage,@appointment_date,@detail,@note,@location_service,@name
                        )");
                using (SqlCommand cmd = new SqlCommand(string_command, ConnectSQL.OpenReportConnect()))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@car_id", service.car_id);
                    cmd.Parameters.AddWithValue("@service_id", service.service_id);
                    cmd.Parameters.AddWithValue("@mileage_at_service", service.mileage_at_service);
                    cmd.Parameters.AddWithValue("@date_at_service", service.date_at_service);
                    cmd.Parameters.AddWithValue("@appointment_mileage", service.appointment_mileage);
                    cmd.Parameters.AddWithValue("@appointment_date", service.appointment_date);
                    cmd.Parameters.AddWithValue("@detail", service.detail);
                    cmd.Parameters.AddWithValue("@note", service.note);
                    cmd.Parameters.AddWithValue("@location_service", service.location_service);
                    cmd.Parameters.AddWithValue("@name", service.name);

                    if (ConnectSQL.con_report.State != ConnectionState.Open)
                    {
                        ConnectSQL.CloseReportConnect();
                        ConnectSQL.OpenReportConnect();
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
                if (ConnectSQL.con_report.State == ConnectionState.Open)
                {
                    ConnectSQL.CloseReportConnect();
                }
            }
            return "Success";
        }

        public string UpdateMileage(string car, int mileage)
        {
            try
            {
                string string_command = string.Format($@"
                    UPDATE ServiceCar SET current_mileage = @current_mileage
                                      WHERE car_id = @car_id");
                using (SqlCommand cmd = new SqlCommand(string_command, ConnectSQL.OpenReportConnect()))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@car_id", car);
                    cmd.Parameters.AddWithValue("@current_mileage", mileage);

                    if (ConnectSQL.con_report.State != ConnectionState.Open)
                    {
                        ConnectSQL.CloseReportConnect();
                        ConnectSQL.OpenReportConnect();
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
                if (ConnectSQL.con_report.State == ConnectionState.Open)
                {
                    ConnectSQL.CloseReportConnect();
                }
            }
            return "Success";
        }

        public string UpdateService(ServiceModel service)
        {
            try
            {
                string string_command = string.Format($@"
                    UPDATE ServiceCar SET current_mileage = @current_mileage,
                                          mileage_at_service = @mileage_at_service,
                                          date_at_service = @date_at_service,
                                          appointment_mileage = @appointment_mileage,
                                          appointment_date = @appointment_date,
                                          next_appointment_mileage = @next_appointment_mileage,
                                          next_appointment_date = @next_appointment_date
                                      WHERE car_id = @car_id AND service_id = @service_id");
                using (SqlCommand cmd = new SqlCommand(string_command, ConnectSQL.OpenReportConnect()))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@car_id", service.car_id);
                    cmd.Parameters.AddWithValue("@service_id", service.service_id);
                    cmd.Parameters.AddWithValue("@current_mileage", service.current_mileage);
                    cmd.Parameters.AddWithValue("@mileage_at_service", service.mileage_at_service);
                    cmd.Parameters.AddWithValue("@date_at_service", service.date_at_service);
                    cmd.Parameters.AddWithValue("@appointment_mileage", service.appointment_mileage);
                    cmd.Parameters.AddWithValue("@appointment_date", service.appointment_date);
                    cmd.Parameters.AddWithValue("@next_appointment_mileage", service.next_appointment_mileage);
                    cmd.Parameters.AddWithValue("@next_appointment_date", service.next_appointment_date);

                    if (ConnectSQL.con_report.State != ConnectionState.Open)
                    {
                        ConnectSQL.CloseReportConnect();
                        ConnectSQL.OpenReportConnect();
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
                if (ConnectSQL.con_report.State == ConnectionState.Open)
                {
                    ConnectSQL.CloseReportConnect();
                }
            }
            return "Success";
        }
    }
}
