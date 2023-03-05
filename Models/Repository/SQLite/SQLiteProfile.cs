using AutoMapper;
using ToDoApp.Models.Dto;
using ToDoApp.Models.Entities;

namespace ToDoApp.Models.Repository.SQLite
{
    public class SQLiteProfile : Profile
    {

        public SQLiteProfile() 
        {

            CreateMap<Users, UsersEntity>().ReverseMap(); 
            CreateMap<Task, TaskEntity>().ReverseMap();
        
        }
    }
}