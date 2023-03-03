using System.Threading.Tasks;

namespace ToDoApp.Models.Entities.Interfaces
{
    public interface IUsuariosRepository
    {
        Usuarios SelecionarUno(string NombreUsuario, string ClaveUsuario);
    }
}