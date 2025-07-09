using MoriaModelsDo.Models.DriveComponents;

namespace MoriaWebAPIServices.Services.Interfaces.DriveComponents;

public interface IInverterControllerService
{
    Task<IEnumerable<InverterDo>> GetAllInverters();

    Task<InverterDo> GetInverterById(int id);

    Task<InverterDo> CreateInverter(InverterDo inverter);

    Task<InverterDo> EditInverter(InverterDo inverter);

    Task<bool> DeleteInverter(int id);
}

