using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Interface
{
    public interface ICompany
    {
        //string OriginalInserts(List<CompanyModel> companies);
        string EditInserts(List<CompanyModel> companies);
        List<CompanyModel> GetCompaniesByDate(DateTime start_date, DateTime stop_date);
        CompanyModel GetCompanyByCode(string code);
        List<CompanyModel> GetCompaniesByDriverDate(string driver,DateTime start_date, DateTime stop_date);
        List<CompanyModel> GetCompaniesByCarDate(string car,DateTime start_date, DateTime stop_date);
        List<CompanyViewModel> GetOriginalCompaniesByDate(DateTime start_date, DateTime stop_date);
        List<CompanyViewModel> GetEditCompaniesByDate(DateTime start_date, DateTime stop_date);
        string UpdateByCode(CompanyModel company);
        string DeleteByCode(string code);
        List<EmployeeModel> GetCompanyDrivers(DateTime start_date, DateTime stop_date);
        List<CarModel> GetCompanyCars(DateTime start_date, DateTime stop_date);
        Stream ExportCompanyNormal(FileInfo path, List<CompanyModel> companies, string month, CTLModels.EmployeeModel emp, List<CarModel> cars);

        string UpdateApproved(List<string> codes, string approver);
    }
}
