using SQLite;
using ToDoApp.Models.Dto;

namespace ToDoApp.Models.Repository.SQLite
{
    public interface IDataBaseService
    {
        public TableQuery<UsersEntity> Users { get; }

        public TableQuery<TaskEntity> Tasks { get; }
    }
}