using ToDo.Domain.Entities;
using ToDo.Domain.Repositories.Interfaces;
using ToDo.Infra.Base;
using ToDo.Infra.Context;

namespace ToDo.Infra.Repositories
{
    public class UserRepository  : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }
    }
}