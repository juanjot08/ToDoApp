using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoApp.Models.Dto;

namespace ToDoApp.Models.Repository.SQLite
{
    internal class Context : SQLiteConnection, IDataBaseService
    {
        public Context(string databasePath) : base(databasePath)
        {
            CreateTable<TaskEntity>();
            CreateTable<UsuarioEntity>();
        }

        public TableQuery<UsuarioEntity> Users => Table<UsuarioEntity>();

        public TableQuery<TaskEntity> Tasks => Table<TaskEntity>();
    }
}