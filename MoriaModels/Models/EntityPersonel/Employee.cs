using Microsoft.EntityFrameworkCore;
using MoriaBaseModels.Attributes;
using MoriaBaseModels.Models;
using MoriaModels.Models.Base;

namespace MoriaModels.Models.EntityPersonel;

[Index(nameof(Username), IsUnique = true)]
[LookupHeaders(true, "Imię", true, "Nazwisko", true, "Nazwa użytkownika")]
public class Employee: BaseModel
{
    //public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }

    //public int PositionId { get; set; }
    public Position? Position { get; set; }
    public bool Admin { get; set; }

    //public ICollection<Permission> Permissions { get; set; } = new List<Permission>();

    public override LookupModel GetLookupObject() => new(Id, FirstName, LastName, Username);
}
