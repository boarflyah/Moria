using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktopServices.Interfaces.API;

public interface IApiInverterService
{
    Task<InverterDo> CreateInverter(string username, InverterDo inverterDo);
    Task<bool> DeleteInverter(string username, int id);
    Task<InverterDo> GetInverter(string username, int id);
    Task<IEnumerable<InverterDo>> GetInverters(string username);
    Task<InverterDo> UpdateInverter(string username, InverterDo inverterDo);
}
