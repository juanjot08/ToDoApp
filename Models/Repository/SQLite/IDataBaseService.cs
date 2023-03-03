using SQLite;
using ToDoApp.Models.Dto;

namespace ToDoApp.Models.Repository.SQLite
{
    public interface IDataBaseService
    {
        public TableQuery<UsuarioEntity> Users { get; }
    }
}