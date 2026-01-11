using System.Collections.Generic;
using TRIPEXPENSEREPORT.CTLModels;

namespace TRIPEXPENSEREPORT.CTLInterfaces
{
    public interface IEmployee
    {
        List<EmployeeModel> GetEmployees();
        List<EmpModel> GetEmps();
    }
}
