using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaModels.Models.Electrical;
using MoriaModelsDo.Models.Dictionaries;

namespace MoriaWebAPIServices.Services.Interfaces.Dictionaries;

public interface IElectricalCabinetControllerService
{
    Task<ElectricalCabinetDo> CreateElectricalCabinet( ElectricalCabinetDo cabinet);
    Task<bool> DeleteElectricalCabinet(int id);
    Task<ElectricalCabinetDo> GetElectricalCabinet(int id);
    Task<IEnumerable<ElectricalCabinetDo>> GetElectricalCabinets();
    Task<ElectricalCabinetDo> UpdateElectricalCabinet(ElectricalCabinetDo cabinet);
}
