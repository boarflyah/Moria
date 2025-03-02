using MoriaModelsDo.Models.DriveComponents;

namespace MoriaWebAPIServices.Services.Interfaces.Products;

public interface IComponentControllerService
{
    Task<ComponentDo> GetComponent(int id);

}
