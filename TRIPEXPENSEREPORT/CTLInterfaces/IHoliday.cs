using System.Collections.Generic;
using TRIPEXPENSEREPORT.CTLModels;

namespace TRIPEXPENSEREPORT.CTLInterfaces
{
    public interface IHoliday
    {
        List<HolidayModel> GetHolidays(string year);
    }
}
