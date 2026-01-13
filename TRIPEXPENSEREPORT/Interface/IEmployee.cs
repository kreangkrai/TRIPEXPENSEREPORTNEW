using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Interface
{
    public interface IEmployee
    {
        List<EmployeeModel> GetEmployees();      
        string Inserts(List<EmployeeModel> employees);
        string Update(EmployeeModel employee);
    }
}
