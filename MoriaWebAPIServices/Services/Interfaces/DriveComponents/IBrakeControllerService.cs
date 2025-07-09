using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaWebAPIServices.Services.Interfaces.DriveComponents;

public interface IBrakeControllerService 
{
    Task<IEnumerable<BrakeDo>> GetAllBrakes();

    Task<BrakeDo> GetBrakeById(int id);

    Task<BrakeDo> CreateBrake(BrakeDo brakeDo);

    Task<BrakeDo> EditBrake(BrakeDo brakeDo);

    Task<bool> DeleteBrake(int id);
}
