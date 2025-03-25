using System.ComponentModel.DataAnnotations.Schema;

namespace MoriaModels.Models.Base;
public class Settings: BaseModel
{
    [Column(TypeName = "timestamp without time zone")]
    public DateTime LastSubiektImport
    {
        get; set;
    }
}
