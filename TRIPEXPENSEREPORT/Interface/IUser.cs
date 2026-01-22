using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Interface
{
    public interface IUser
    {
        List<UserManagementModel> GetUsers();
        string update(string emp_id, string role);
        string insert(UserManagementModel users);
    }
}
