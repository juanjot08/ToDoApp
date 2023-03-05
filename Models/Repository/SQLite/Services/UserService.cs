using AutoMapper;
using ToDoApp.Models.Dto;
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

        public void CrearUsuario(string NombreUsuario, string ClaveUsuario)
        {
            _databaseService.Users.Connection.Insert(new UsuarioEntity()
            {
                Usuario = NombreUsuario,
                Password = ClaveUsuario
            });
        }

        public Usuarios SelecionarUno(string nombreUsuario, string claveUsuario)
        {
            UsuarioEntity usuario = _databaseService.Users
                                    .FirstOrDefault(_ => _.Usuario == nombreUsuario & _.Password == claveUsuario);

            return _mapper.Map<Usuarios>(usuario);

        }
    }
}