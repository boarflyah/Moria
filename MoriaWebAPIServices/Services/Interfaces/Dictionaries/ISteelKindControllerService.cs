using MoriaModelsDo.Models.Dictionaries;

namespace MoriaWebAPIServices.Services.Interfaces.Dictionaries
{
    public interface ISteelKindControllerService
    {
        Task<SteelKindDo> CreateSteelKind(SteelKindDo steelKind);
        Task<bool> DeleteSteelKind(int id);
        Task<SteelKindDo> EditSteelKind(SteelKindDo steelKind);
        Task<List<SteelKindDo>> GetAllSteelKinds();
        Task<SteelKindDo> GetSteelKindById(int id);
    }
}