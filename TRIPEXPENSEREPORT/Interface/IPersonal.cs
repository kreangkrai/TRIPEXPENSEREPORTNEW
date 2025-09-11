using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Interface
{
    public interface IPersonal
    {
        string OriginalInserts(List<PersonalModel> personals);
        string EditInserts(List<PersonalModel> personals);
        List<PersonalViewModel> GetOriginalPersonalsByDate(DateTime start_date,DateTime stop_date);
        List<PersonalViewModel> GetEditPersonalsByDate(DateTime start_date, DateTime stop_date);
        string UpdateByCode(PersonalModel personal);
    }
}
