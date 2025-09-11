using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Interface
{
    public interface IAllowance
    {
        string OriginalInserts(List<AllowanceModel> allowances);
        string EditInserts(List<AllowanceModel> allowances);
        List<AllowanceViewModel> GetOriginalAllowancesByDate(DateTime start_date, DateTime stop_date);
        List<AllowanceViewModel> GetEditAllowancesByDate(DateTime start_date, DateTime stop_date);
        string UpdateByCode(AllowanceModel allowance);
    }
}
