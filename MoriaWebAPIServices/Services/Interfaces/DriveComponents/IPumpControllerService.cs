using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaModels.Models.DriveComponents;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaWebAPIServices.Services.Interfaces.DriveComponents;

public interface IPumpControllerService
{
    Task<IEnumerable<PumpDo>> GetAllPumps();

    Task<PumpDo> GetPumpById(int id);

    Task<PumpDo> CreatePump(PumpDo pump);

    Task<PumpDo> EditPump(PumpDo pump);

    Task<bool> DeletePump(int id);
}
