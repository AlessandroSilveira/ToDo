using System.Linq;
using System.Threading.Tasks;
using ToDo.Domain.Entities;
using ToDo.Domain.Repositories.Interfaces;
using ToDo.Domain.Services.Interfaces;

namespace ToDo.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> GetByUserNameAndPassword(string modelUsername, string modelPassword)
        {
            return _repository.Search(a => a.Username == modelUsername && a.Password == modelPassword).Result.FirstOrDefault();
        }

        public async Task<User> GetByUserName(string userUsername)
        {
            return _repository.Search(a => a.Username == userUsername).Result.FirstOrDefault();
        }

        public void Create(User user)
        {
            _repository.Add(user);
        }
    }
}