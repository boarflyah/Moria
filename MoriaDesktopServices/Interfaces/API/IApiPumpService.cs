using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktopServices.Interfaces.API;

public interface IApiPumpService
{
    Task<PumpDo> CreatePump(string username, PumpDo pumpDo);
    Task<bool> DeletePump(string username, int id);
    Task<PumpDo> GetPump(string username, int id);
    Task<IEnumerable<PumpDo>> GetPumps(string username);
    Task<PumpDo> UpdatePump(string username, PumpDo pumpDo);
}
