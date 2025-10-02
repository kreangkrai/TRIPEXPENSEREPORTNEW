using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Interface
{
    public interface ITrip
    {
        List<DataModel> GetDatasByID(string emp_id , DateTime start , DateTime end );
    }
}
