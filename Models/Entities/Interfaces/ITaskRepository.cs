﻿using System.Collections.Generic;

namespace ToDoApp.Models.Entities.Interfaces
{
    public interface ITaskRepository
    {

        void DeleteTask(Task task);

        void CreateTask(Task task);

        List<Task> GetAllTasks();

    }
}