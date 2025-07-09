using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaModels.Models.DriveComponents;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaWebAPIServices.Services.Interfaces.DriveComponents;

public interface ISupplementControllerService
{
    Task<IEnumerable<SupplementDo>> GetAllSupplements();

    Task<SupplementDo> GetSupplementById(int id);

    Task<SupplementDo> CreateSupplement(SupplementDo supplement);

    Task<SupplementDo> EditSupplement(SupplementDo supplement);

    Task<bool> DeleteSupplement(int id);
}
