using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Interface
{
    public interface IPersonal
    {
        //string OriginalInserts(List<PersonalModel> personals);
        string EditInserts(List<PersonalModel> personals);
        PersonalModel GetPersonalsByCode(string code);
        List<PersonalModel> GetPersonalsByDate(string emp_id, DateTime start_date, DateTime stop_date);
        //List<PersonalViewModel> GetOriginalPersonalsByDate(DateTime start_date,DateTime stop_date);
        List<PersonalViewModel> GetEditPersonalsByDate(string emp_id,DateTime start_date, DateTime stop_date);
        string UpdateByCode(PersonalModel personal);
        string DeleteByCode(string code);
        List<EmployeeModel> GetPesonalDrivers(DateTime start_date,DateTime stop_date);

        PersonalGasolineModel GetPersonalGasoline(string emp_id,string month);
        string InsertPersonalGasoline(PersonalGasolineModel personal);
        string UpdatePersonalGasoline(PersonalGasolineModel personal);

        Stream ExportPersonalNormal(FileInfo path, List<PersonalModel> personals, string month,CTLModels.EmployeeModel emp, GasolineModel gasoline, PersonalGasolineModel gasoline_type);


        // Admin

        List<EmployeeModel> GetPesonalDriversAdmin(DateTime start_date, DateTime stop_date);
        string UpdateApproved(List<string> codes, string approver);
    }
}
