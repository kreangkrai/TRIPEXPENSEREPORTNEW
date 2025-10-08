using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Interface
{
    public interface IBorrow
    {
        List<BorrowerModel> GetBorrowers();
        List<BorrowerModel> GetBorrowersLog();
        string Insert(BorrowerModel borrower);
        string Delete(string borrow_id);
        string InsertLog(BorrowerModel borrower);
    }
}
