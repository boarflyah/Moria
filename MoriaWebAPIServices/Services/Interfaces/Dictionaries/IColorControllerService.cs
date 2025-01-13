using MoriaModelsDo.Models.Dictionaries;

namespace MoriaWebAPIServices.Services.Interfaces.Dictionaries
{
    public interface IColorControllerService
    {
        Task<ColorDo> CreateColor(ColorDo color);
        Task<bool> DeleteColor(int id);
        Task<ColorDo> EditColor(ColorDo color);
        Task<List<ColorDo>> GetAllColors();
        Task<ColorDo> GetColorById(int id);
    }
}