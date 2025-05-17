using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaModels.Models.Electrical;
using MoriaModelsDo.Models.Dictionaries;

namespace MoriaDesktopServices.Interfaces.API;

public interface IApiElectricalCabinetService
{
    Task<ElectricalCabinetDo> CreateElectricalCabinet(string username, ElectricalCabinetDo cabinet);
    Task<bool> DeleteElectricalCabinet(string username, int id);
    Task<ElectricalCabinetDo> GetElectricalCabinet(string username, int id);
    Task<IEnumerable<ElectricalCabinetDo>> GetElectricalCabinets(string username);
    Task<ElectricalCabinetDo> UpdateElectricalCabinet(string username, ElectricalCabinetDo cabinet);

}
