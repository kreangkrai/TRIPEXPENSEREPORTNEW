using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Interface
{
    public interface ICompany
    {
        string OriginalInserts(List<CompanyModel> companies);
        string EditInserts(List<CompanyModel> companies);
        List<CompanyViewModel> GetOriginalCompaniesByDate(DateTime start_date, DateTime stop_date);
        List<CompanyViewModel> GetEditCompaniesByDate(DateTime start_date, DateTime stop_date);
        string UpdateByCode(CompanyModel company);
    }
}
