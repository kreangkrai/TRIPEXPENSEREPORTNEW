using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Interface
{
    public interface IPersonal
    {
        string OriginalInserts(List<PersonalModel> personals);
        string EditInserts(List<PersonalModel> personals);
    }
}
