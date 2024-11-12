namespace MoriaWebAPI.Services.Interfaces;

public interface ITokenGeneratorService
{
    string GenerateJwtToken(string userId);
}
