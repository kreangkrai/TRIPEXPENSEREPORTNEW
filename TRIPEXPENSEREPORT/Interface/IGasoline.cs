using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Interface
{
    public interface IGasoline
    {
        GasolineModel GetGasolineByMonth(string  month);
    }
}
