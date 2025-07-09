using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaModels.Models.DriveComponents;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktopServices.Interfaces.API;

public interface IApiSupplementService
{
    Task<SupplementDo> CreateSupplement(string username, SupplementDo supplementDo);
    Task<bool> DeleteSupplement(string username, int id);
    Task<SupplementDo> GetSupplement(string username, int id);
    Task<IEnumerable<SupplementDo>> GetSupplements(string username);
    Task<SupplementDo> UpdateSupplement(string username, SupplementDo supplementDo);
}
