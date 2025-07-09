using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaModels.Models.DriveComponents;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaWebAPIServices.Services.Interfaces.DriveComponents;

public interface IExternalCoolingControllerService
{
    Task<IEnumerable<ExternalCoolingDo>> GetAllExternalCoolings();

    Task<ExternalCoolingDo> GetExternalCoolingById(int id);

    Task<ExternalCoolingDo> CreateExternalCooling(ExternalCoolingDo external);

    Task<ExternalCoolingDo> EditExternalCooling(ExternalCoolingDo externalCoolingDo);

    Task<bool> DeleteExternalCooling(int id);
}
