using System;
using System.Collections.Generic;
using Java.Sql;

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
        
        public DateTime DeadLine { get; set; }

        public bool Important { get; set; }
        public Todo()
        {
            DeadLine = DateTime.Now.AddDays(5);
        }
    }
    
    public sealed class ToDoViewModel
    {
        private List<Todo> _todos = new List<Todo>();

        #region Singleton pattern

        private static readonly Lazy<ToDoViewModel> lazy =
            new Lazy<ToDoViewModel>(() => new ToDoViewModel());
        
        public static ToDoViewModel Instance => lazy.Value;
        

        private ToDoViewModel()
        {
            _todos.Add(new Todo()
            {
                Description = "Take a nap",
                Status = Todo.TaskStatus.OnHold
            });

            _todos.Add(new Todo()
            {
                Description = "Write a nice README",
                Status = Todo.TaskStatus.Open
            });

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
           
           
        }

        #endregion

        #region List display

        public IList<Todo> Todos => _todos;

        #endregion
        
        #region edit & commit
        
        
        // The task being edited is kept apart
        private Todo _editing;

        // Dictionnary that wil hold the temporary edited value
        public Dictionary<string, object> EditingValues { get; private set; }
        
        // the key to use for the dictionnary
        public enum EditableField
        {
            Description,
            Status,
            DeadLine,
            Important
        }
        
        // Store the given task and reset the dictionnary
        public void SetEditedTodo(Todo todo)
        {
            _editing = todo;
            EditingValues = new Dictionary<string, object>()
            {
                {
                    EditableField.Status.ToString(), _editing.Status
                },
                {
                    EditableField.Description.ToString(), _editing.Description
                },
                {
                    EditableField.DeadLine.ToString(), _editing.DeadLine
                },
                {
                    EditableField.Important.ToString(), _editing.Important
                },
            };
        }
        
        // apply the change from the dictionnary
        public void Commit()
        {
            _editing.Description = (string)EditingValues[EditableField.Description.ToString()];
            _editing.Status = (Todo.TaskStatus) EditingValues[EditableField.Status.ToString()];
            _editing.DeadLine = (DateTime) EditingValues[EditableField.DeadLine.ToString()];
            _editing.Important = (bool) EditingValues[EditableField.Important.ToString()];

        }

        #endregion
        
       
    }
}