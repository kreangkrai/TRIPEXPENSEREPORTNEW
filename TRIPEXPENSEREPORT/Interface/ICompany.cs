using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Interface
{
    public interface ICompany
    {
        string OriginalInserts(List<CompanyModel> companies);
        string EditInserts(List<CompanyModel> companies);
    }
}
