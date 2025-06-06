namespace dotnet_stocks_api.Interfaces
{

    public interface IJwtService
    {
        public string GenerateToken(string username);
    }
}