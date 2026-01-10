using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Interface
{
    public interface IPersonal
    {
        string OriginalInserts(List<PersonalModel> personals);
        string EditInserts(List<PersonalModel> personals);
        PersonalModel GetPersonalsByCode(string code);
        List<PersonalModel> GetPersonalsByDate(string emp_id, DateTime start_date, DateTime stop_date);
        List<PersonalViewModel> GetOriginalPersonalsByDate(DateTime start_date,DateTime stop_date);
        List<PersonalViewModel> GetEditPersonalsByDate(string emp_id,DateTime start_date, DateTime stop_date);
        string UpdateByCode(PersonalModel personal);
        string DeleteByCode(string code);
        List<EmployeeModel> GetPesonalDrivers(DateTime start_date,DateTime stop_date);
    }
}
