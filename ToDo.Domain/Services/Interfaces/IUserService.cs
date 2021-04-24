using System.Threading.Tasks;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByUserNameAndPassword(string modelUsername, string modelPassword);


        Task<User> GetByUserName(string userUsername);


        void Create(User user);

    }
}