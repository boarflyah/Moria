using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaModels.Models.DriveComponents;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktopServices.Interfaces.API;

public interface IApiVariatorService
{
    Task<VariatorDo> CreateVariator(string username, VariatorDo variatorDo);
    Task<bool> DeleteVariator(string username, int id);
    Task<VariatorDo> GetVariator(string username, int id);
    Task<IEnumerable<VariatorDo>> GetVariators(string username);
    Task<VariatorDo> UpdateVariator(string username, VariatorDo variatorDo);
}
