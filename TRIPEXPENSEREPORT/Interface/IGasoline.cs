using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRIPEXPENSEREPORT.Models;

namespace TRIPEXPENSEREPORT.Interface
{
    public interface IGasoline
    {
        GasolineModel GetGasolineByMonth(string month);
        List<GasolineModel> GetGasoline();
        double GetSohol(string month);
        double GetDiesel(string month);
        double GetValueGasByMonth(string gas, string month);
        string Update(GasolineModel model);
        string Insert(GasolineModel model);
        string Delete(GasolineModel model);
    }
}
