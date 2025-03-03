using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktopServices.Interfaces.API;
public interface IApiComponentService
{
    Task<ComponentDo> GetComponent(string username, int id);
}
