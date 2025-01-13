using MoriaModelsDo.Models.Contacts;

namespace MoriaWebAPIServices.Services.Interfaces.Dictionaries
{
    public interface IPositionControllerService
    {
        Task<PositionDo> CreatePosition(PositionDo position);
        Task<bool> DeletePosition(int id);
        Task<PositionDo> EditPosition(PositionDo position);
        Task<List<PositionDo>> GetAllPositions();
        Task<PositionDo> GetPositionById(int id);
    }
}