using MoriaModelsDo.Models.Contacts;

namespace MoriaDesktopServices.Interfaces.API
{
    public interface IApiPositionService
    {
        Task<PositionDo> CreatePosition(string username, PositionDo position);
        Task<bool> DeletePosition(string username, int id);
        Task<PositionDo> GetPosition(string username, int id);
        Task<IEnumerable<PositionDo>> GetPositions(string username);
        Task<PositionDo> UpdatePosition(string username, PositionDo position);
    }
}