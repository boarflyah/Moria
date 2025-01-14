using MoriaModelsDo.Models.Dictionaries;

namespace MoriaDesktopServices.Interfaces.API
{
    public interface IApiSteelKindService
    {
        Task<SteelKindDo> CreateSteelKind(string username, SteelKindDo steelKind);
        Task<bool> DeleteSteelKind(string username, int id);
        Task<SteelKindDo> GetSteelKind(string username, int id);
        Task<IEnumerable<SteelKindDo>> GetSteelKinds(string username);
        Task<SteelKindDo> UpdateSteelKind(string username, SteelKindDo steelKind);
    }
}