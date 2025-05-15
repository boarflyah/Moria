using MoriaBaseModels.Models;
using MoriaModelsDo.Base;

namespace MoriaModelsDo.Models.Base;
public class ListViewSetupDo: BaseDo
{
    public string ListViewId
    {
        get; set;
    }

    public IList<ListViewColumnProvider> Columns
    {
        get; set;
    }
}
