﻿using TutorialRestApi.User.App.Models;

namespace TutorialRestApi.Todo.App.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }

        public UserModel User { get; set; }

    }
}
