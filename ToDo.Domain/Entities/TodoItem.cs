using System;

namespace ToDo.Domain.Entities
{
    public class TodoItem : Entity
    {
        public TodoItem()
        {

        }
        public TodoItem(string title, bool done, DateTime date, string user)
        {
            Title = title;
            Done = done;
            Date = date;
            User = user;
        }

        public TodoItem(string commandTitle, string commandUser, DateTime commandDate)
        {
            Title = commandTitle;
            Date = commandDate;
            User = commandUser;
        }

        public string Title { get;  set; }
        public bool Done { get;  set; }
        public DateTime Date { get;  set; }
        public string User { get;  set; }
        
        public void MarkAsDone()
        {
            Done = true;
        }

        public void MarkAsUndone()
        {
            Done = false;
        }

        public void UpdateTitle(string title)
        {
            Title = title;
        }
    }
}