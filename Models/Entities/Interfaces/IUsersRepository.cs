using System.Threading.Tasks;

namespace ToDoApp.Models.Entities.Interfaces
{
    public interface IUsersRepository
    {
        Users SelecionarUno(string NombreUsuario, string ClaveUsuario);

        void CrearUsuario(string NombreUsuario, string ClaveUsuario);
    }
}