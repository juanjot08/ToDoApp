using Android.App;
using Android.OS;
using Android.Widget;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using ToDoApp.Models.Entities;
using ToDoApp.Models.Entities.Interfaces;
using ToDoApp.Models.Repository.SQLite.Services;
using ToDoApp.Models.Repository.SQLite;
using ToDoApp.ViewModels;
using System.IO;
using Java.Security;
using System;
using Android.Views;
using Google.Android.Material.Snackbar;
using Google.Android.Material.FloatingActionButton;
using Android.Content;
using Context = ToDoApp.Models.Repository.SQLite.Context;

namespace ToDoApp.Views
{
    [Activity(Label = "ToDoMainView")]
    public class ToDoMainView : Activity
    {
        private ITaskRepository _taskRepository;
        private ListView lstTask;
        private ListAdapter listAdapter;
        private IServiceProvider _provider;
        EditText edtTask;

        public ToDoMainView() { }

        public ToDoMainView(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_todo_mainwiew);

            RegistrarDependencias();

            lstTask = FindViewById<ListView>(Resource.Id.lstTask);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            LoadTaskList();
        }

        public void LoadTaskList()
        {
            List<Task> taskList = _taskRepository.GetAllTasks();
            listAdapter = new ListAdapter(this, taskList, _taskRepository);
            lstTask.Adapter = listAdapter;
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            edtTask = new EditText(this);
            AlertDialog alertDialog = new AlertDialog.Builder(this)
                .SetTitle("Add New Task")
                .SetMessage("What do you want to do next?")
                .SetView(edtTask)
                .SetPositiveButton("Add", OkAction)
                .SetNegativeButton("Cancel", CancelAction)
                .Create();
            alertDialog.Show();
        }

        private void OkAction(object sender, DialogClickEventArgs e)
        {
            string task = edtTask.Text;
            _taskRepository.CreateTask(new Task()
            {
                Title = task,
            });

            LoadTaskList();
        }

        private void CancelAction(object sender, DialogClickEventArgs e)
        {
        }

        private void RegistrarDependencias()
        {
            IServiceCollection services = new ServiceCollection();

            string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "users.db3");
            services.AddSingleton<IDataBaseService>(new Context(path));

            services.AddScoped<IUsuariosRepository, UserService>();
            services.AddScoped<ITaskRepository, TaskService>();

            services.AddAutoMapper(typeof(SQLiteProfile));
            _provider = services.BuildServiceProvider();

            _taskRepository = _provider.GetService<ITaskRepository>();
        }
    }
}