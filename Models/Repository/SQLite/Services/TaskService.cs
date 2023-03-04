using AutoMapper;
using System;
using System.Collections.Generic;
using ToDoApp.Models.Dto;
using ToDoApp.Models.Entities;
using ToDoApp.Models.Entities.Interfaces;

namespace ToDoApp.Models.Repository.SQLite.Services
{
    public class TaskService : ITaskRepository
    {
        private readonly IDataBaseService _databaseService;
        private readonly IMapper _mapper;

        public TaskService(IDataBaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public void CreateTask(Task task)
        {
            TaskEntity taskEntity = _mapper.Map<TaskEntity>(task);

            _databaseService.Tasks.Connection.Insert(taskEntity);
        }

        public void DeleteTask(Task task)
        {
            TaskEntity taskEntity = _mapper.Map<TaskEntity>(task);

            _databaseService.Tasks.Connection.Delete(taskEntity);
        }

        public List<Task> GetAllTasks()
        {
            List<TaskEntity> taskEntity = _databaseService.Tasks.ToList();

            return _mapper.Map<List<Task>>(taskEntity);
        }
    }
}