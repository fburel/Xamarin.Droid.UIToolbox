using System;
using System.Collections.Generic;

namespace Demo.ViewModel
{
    public class Todo
    {
        public enum TaskStatus
        {
            Open, 
            InProgress,
            OnHold,
            Done
        }
        
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        
        
    }
    
    public sealed class ToDoViewModel
    {
        private List<Todo> _todos = new List<Todo>();

        #region Singleton pattern

        private static readonly Lazy<ToDoViewModel> lazy =
            new Lazy<ToDoViewModel>(() => new ToDoViewModel());

        private Todo _editing;

        public static ToDoViewModel Instance => lazy.Value;
        

        private ToDoViewModel()
        {
            _todos.Add(new Todo()
            {
                Description = "Update to Android 13",
                Status = Todo.TaskStatus.Done
            });
            _todos.Add(new Todo()
            {
                Description = "Create a cool demo",
                Status = Todo.TaskStatus.InProgress
            });
            _todos.Add(new Todo()
            {
                Description = "Write a nice README",
                Status = Todo.TaskStatus.Open
            });
            _todos.Add(new Todo()
            {
                Description = "Take a nap",
                Status = Todo.TaskStatus.OnHold
            });
        }

        #endregion

        public IList<Todo> Todos => _todos;
        public Todo EditingTask => _editing;

        public void SetEditedTodo(Todo todo)
        {
            _editing = todo;
        }
    }
}