using MoriaModels.Models.Base;

namespace MoriaModels.Models.EntityPersonel;

public class Employee: BaseModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }

    public int PositionId { get; set; }
    public Position Position { get; set; }

    //public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
