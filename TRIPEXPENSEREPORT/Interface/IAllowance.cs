using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Interface
{
    public interface IAllowance
    {
        string OriginalInserts(List<AllowanceModel> allowances);
        string EditInserts(List<AllowanceModel> allowances);
    }
}
