using dotnet_stocks_api.Models;

namespace dotnet_stocks_api.Repositories
{
    public class ProfileRepo
    {
         List<ProfileModel> profile = new List<ProfileModel>();

        public ProfileRepo()
        {
            // Seed with some initial data
            profile.Add(new ProfileModel { FirstName = "Ashritha", LastName = "Dharma", Email = "Ashritha.dharma@example.com" });
            profile.Add(new ProfileModel { FirstName = "Shashi", LastName = "Kiran", Email = "Shashi.Kiran@example.com" });
        }


        public List<ProfileModel> GetAllProfiles()
        {
            return profile;
        }

        public string CreateProfile(ProfileModel profilemodel)
        {
            profile.Add(profilemodel);
            return "Successfully Created Profile.";
        }

    }
}