using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaModelsDo.Models.DriveComponents;
using MoriaModelsDo.Models.Products;

namespace MoriaWebAPIServices.Services.Interfaces.DriveComponents;

public interface IVariatorControllerService
{
    Task<IEnumerable<VariatorDo>> GetAllVariators();

    Task<VariatorDo> GetVariatorsById(int id);

    Task<VariatorDo> CreateVariator(VariatorDo variator);

    Task<VariatorDo> EditVariator(VariatorDo variator);

    Task<bool> DeleteVariator(int id);
}
