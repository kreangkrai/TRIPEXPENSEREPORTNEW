using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Interface
{
    public interface ICar
    {
        List<CarModel> GetCars();
        string Insert(CarModel car);
        string Update(CarModel car);
        string Delete(string car);
    }
}
