using TRIPEXPENSEREPORT.Service;

namespace TRIPEXPENSEREPORT.Interface
{
    public interface IService
    {
        List<ServiceTypeModel> GetServiceTypes();
        List<ServiceModel> GetSevices();
        ServiceModel GetSeviceByCar(string car_id, string service_id);
        List<ServiceModel> GetSevicesByService(string _service);
        List<ServiceModel> GetSevicesHistory();
        List<ServiceModel> GetSevicesHistoryByService(string _service);
        string InsertService(ServiceModel service);
        string InsertServiceHistory(ServiceModel service);
        string UpdateService(ServiceModel service);
        string UpdateMileage(string car, int mileage);
    }
}
