namespace Service.Security;

public interface ITokenClaimsService
{
    Task<string> GetTokenAsync(string userName);
}