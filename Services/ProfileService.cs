using dotnet_stocks_api.Models;
using dotnet_stocks_api.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace dotnet_stocks_api.Services
{
    public class ProfileService :IProfileService
    {
        public ProfileRepo profileRepo;

        public ProfileService(ProfileRepo profileRepo)
        {
            this.profileRepo = profileRepo;
        }

        public List<ProfileModel> GetProfiles()
        {
            return profileRepo.GetAllProfiles();

        }

        public string CreateProfile(ProfileModel profile)
        {
            return profileRepo.CreateProfile(profile);
           
        }
    }
}