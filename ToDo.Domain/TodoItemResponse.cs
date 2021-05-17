using System;

namespace ToDo.Domain
{
    public class TodoItemResponse
    {
        public string Title { get;  set; }
        public bool Done { get;  set; }
        public DateTime Date { get;  set; }
        public string User { get;  set; }
    }
}