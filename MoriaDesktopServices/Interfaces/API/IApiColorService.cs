using MoriaModelsDo.Models.Dictionaries;

namespace MoriaDesktopServices.Interfaces.API
{
    public interface IApiColorService
    {
        Task<ColorDo> CreateColor(string username, ColorDo color);
        Task<bool> DeleteColor(string username, int id);
        Task<ColorDo> GetColor(string username, int id);
        Task<IEnumerable<ColorDo>> GetColors(string username);
        Task<ColorDo> UpdateColor(string username, ColorDo color);
    }
}