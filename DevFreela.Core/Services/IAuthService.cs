namespace DevFreela.Core.Services;

public interface IAuthService
{
    public Task<string?> GenerateTokenJWT(string email, int role);

    public Task<string> GeneratePasswordHash256(string password);

}
