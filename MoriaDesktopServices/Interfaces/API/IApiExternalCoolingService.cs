using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaModels.Models.DriveComponents;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktopServices.Interfaces.API;

public interface IApiExternalCoolingService
{
    Task<ExternalCoolingDo> CreateExternalCooling(string username, ExternalCoolingDo externalCoolingDo);
    Task<bool> DeleteExternalCooling(string username, int id);
    Task<ExternalCoolingDo> GetExternalCooling(string username, int id);
    Task<IEnumerable<ExternalCoolingDo>> GetExternalCoolings(string username);
    Task<ExternalCoolingDo> UpdateExternalCooling(string username, ExternalCoolingDo externalCoolingDo);
}
