using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaModels.Models.Base;

namespace MoriaModels.Models.DriveComponents.Relations;

public class DriveToComponent : BaseModel
{
    public int DriveId
    {
        get; set;
    }
    public int ComponentId
    {
        get; set;
    }
    public Drive Drive
    {
        get; set;
    } = null!;
    public Component Component
    {
        get; set;
    } = null!;

    public int Quantity
    {
        get; set;
    }
}
