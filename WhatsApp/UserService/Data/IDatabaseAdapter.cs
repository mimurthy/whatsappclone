using UserService.Models;

namespace UserService.Data
{
    public interface IDatabaseAdapter
    {
        void UpdateProfile(User userinfo);

        void SignUp(User userinfo);

        string Login(string username, string password);
    }
}
