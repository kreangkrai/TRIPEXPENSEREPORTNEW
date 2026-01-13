using System.Collections.Generic;
using TRIPEXPENSEREPORT.CTLModels;

namespace TRIPEXPENSEREPORT.CTLInterfaces
{
    public interface IEmployee
    {
        List<EmployeeModel> GetEmployees();
        EmployeeModel GetEmployeeByID(string emp_id);
        List<EmpModel> GetEmps();
    }
}
