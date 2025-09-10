using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Interface
{
    public interface IUser
    {
        List<UserManagementModel> GetUsers();
        //string update(string name, string role);
        //string insert(string name);
    }
}
