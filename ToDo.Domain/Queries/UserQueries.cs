using System;
using System.Linq.Expressions;
using ToDo.Domain.Entities;

namespace Todo.Domain.Queries
{
    public static class UserQueries
    {
        public static Expression<Func<TodoItem, bool>> GetAll(string user)
        {
            return x => x.User == user;
        }

        public static Expression<Func<TodoItem, bool>> GetAllDone(string user)
        {
            return x => x.User == user && x.Done == true;
        }

        public static Expression<Func<TodoItem, bool>> GetAllUndone(string user)
        {
            return x => x.User == user && x.Done == false;
        }

        public static Expression<Func<TodoItem, bool>> GetByPeriod(string user, DateTime date, bool done)
        {
            return x =>
                x.User == user &&
                x.Done == done &&
                x.Date.Date == date.Date;
        }
    }
}