using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Interface
{
    public interface ITrip
    {
        List<DataModel> GetDatasALLByEMPID(string emp_id , DateTime start , DateTime end );
        List<DataModel> GetDatasPersonalByEMPID(string emp_id, DateTime start, DateTime end);
        List<DataModel> GetDatasCompnayByEMPID(string emp_id, DateTime start, DateTime end);
    }
}
