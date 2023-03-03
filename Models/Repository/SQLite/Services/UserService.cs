using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Models.Entities;
using ToDoApp.Models.Entities.Interfaces;

namespace ToDoApp.Models.Repository.SQLite.Services
{
    public class UserService : IUsuariosRepository
    {
        private readonly IDataBaseService _databaseService;
        private readonly IMapper _mapper;

        public UserService(IDataBaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public Usuarios SelecionarUno(string nombreUsuario, string claveUsuario)
        {
            Dto.UsuarioEntity usuario = _databaseService.Users
                                        .FirstOrDefault(_ => _.Password == claveUsuario 
                                                        & _.Usuario == nombreUsuario);

            return _mapper.Map<Usuarios>(usuario);

        }
    }
}