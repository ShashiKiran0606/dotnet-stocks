using dotnet_stocks_api.Models;

public interface IProfileService
{
    public string CreateProfile(ProfileModel profile);
    public List<ProfileModel> GetProfiles();
}