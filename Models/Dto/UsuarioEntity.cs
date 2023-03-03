using SQLite;

namespace ToDoApp.Models.Dto
{
    public class UsuarioEntity
    {
        [PrimaryKey, AutoIncrement]
        [MaxLength(10)]
        public int Id { set; get; }

        [MaxLength(20)]
        public string Usuario { get; set; }

        [MaxLength(20)]
        public string Password { get; set; }
    }
}