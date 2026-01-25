using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Interface
{
    public interface ITrip
    {
        List<DataModel> GetAllRouteByEMPID(string emp_id, DateTime start, DateTime end);
        List<DataModel> GetDatasPassengerALLByEMPID(string emp_id , DateTime start , DateTime end );
        List<DataModel> GetDatasPersonalByEMPID(string emp_id, DateTime start, DateTime end);
        List<DataModel> GetDatasCompnayByEMPID(string emp_id, DateTime start, DateTime end);
        List<DataModel> GetDatasCompnayByDate(DateTime start, DateTime end);
    }
}
