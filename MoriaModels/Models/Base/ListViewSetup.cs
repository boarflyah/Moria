using MoriaModels.Models.EntityPersonel;

namespace MoriaModels.Models.Base;
public class ListViewSetup: BaseModel
{
    public string ListViewId
    {
        get; set;
    }
    public Employee? Employee
    {
        get; set;
    }

    public string Columns
    {
        get; set;
    }
}
