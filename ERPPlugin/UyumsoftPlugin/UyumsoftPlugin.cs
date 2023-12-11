using ERPPlugin;
using ERPPlugin.Dto;

namespace UyumsoftPlugin
{
    public class UyumsoftPlugin : IERPPlugin
    {
        public async Task<List<Title>> GetTitle()
        {
            var titles = new List<Title>();

            for (int i = 0; i < 4; i++)
            {
                var title = new Title
                {
                    Id = Guid.NewGuid(),
                    TitleCode = "001"+i,
                    TitleName = "Portföy Yöneticisi"+i,
                    Description = "description"+i,
                    ParentId = Guid.NewGuid()
                };

                titles.Add(title);
            }

            return await Task.FromResult(titles);
        }

        public async Task<List<User>> GetUser()
        {
            var users = new List<User>();

            for (int i = 0; i < 4; i++)
            {
                var birthDateTime = new DateTime(1970, 1, 1);
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "FE2261"+i,
                    GivenName = "Name"+i,
                    Surname = "LastName"+i,
                    Phone = "5000001"+i,
                    Email = "mail@"+i,
                    IsActive = true,
                    IdentityNumber = ""+i,
                    BirthDate = new DateOnly(birthDateTime.Year, birthDateTime.Month, birthDateTime.Day),
                    ParentId = Guid.NewGuid(),
                    TitleId = Guid.NewGuid()
                };

                users.Add(user);
            }

            return await Task.FromResult(users);
        }
    }
}
