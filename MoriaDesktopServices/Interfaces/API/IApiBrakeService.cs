using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktopServices.Interfaces.API;

public interface IApiBrakeService
{
    Task<BrakeDo> CreateBrake(string username, BrakeDo brakeDo);
    Task<bool> DeleteBrake(string username, int id);
    Task<BrakeDo> GetBrake(string username, int id);
    Task<IEnumerable<BrakeDo>> GetBrakes(string username);
    Task<BrakeDo> UpdateBrake(string username, BrakeDo brakeDo);
}
