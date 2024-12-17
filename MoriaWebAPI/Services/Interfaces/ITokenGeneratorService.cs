namespace MoriaWebAPI.Services.Interfaces;

public interface ITokenGeneratorService
{
    string GenerateJwtToken(int userId);
}
