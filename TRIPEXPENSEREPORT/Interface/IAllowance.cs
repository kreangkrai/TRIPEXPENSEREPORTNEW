using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Interface
{
    public interface IAllowance
    {
        string EditInserts(List<AllowanceModel> allowances);
        //List<AllowanceViewModel> GetOriginalAllowancesByDate(DateTime start_date, DateTime stop_date);
        List<AllowanceModel> GetEditAllowancesByDate(string emp_id,DateTime start_date, DateTime stop_date);
        AllowanceModel GetAllowancesByCode(string code);
        string UpdateByCode(AllowanceModel allowance);
        string DeleteByCode(string code);
        List<AllowanceModel> CalculateAllowanceNew(string emp_id, List<DataTripModel> trips, DateTime start, DateTime stop);
        Stream ExportAllowance(FileInfo path, List<AllowanceModel> allowances, string month, CTLModels.EmployeeModel emp);

        List<EmployeeModel> GetEmployeeAdmin(DateTime start_date, DateTime stop_date);
        string UpdateApproved(List<string> codes, string approver);
    }
}
