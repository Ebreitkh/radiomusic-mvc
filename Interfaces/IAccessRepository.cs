using MusicRadio.Models;

namespace MusicRadio.Interfaces
{
    interface IAccessRepository
    {
        void Add(User user);
        bool ValidateUser(string mail, string password);

    }
}
