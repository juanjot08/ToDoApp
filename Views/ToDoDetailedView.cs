using Android.App;
using Android.Content;
using Android.OS;
using Microsoft.Extensions.DependencyInjection;
using System;
using ToDoApp.Models.Entities.Interfaces;
using ToDoApp.Models.Repository.SQLite.Services;
using ToDoApp.Models.Repository.SQLite;
using System.IO;
using Context = ToDoApp.Models.Repository.SQLite.Context;
using ToDoApp.Models.Entities;
using Android.Widget;

namespace ToDoApp.Views
{
    [Activity(Label = "ToDoDetailedView")]
    public class ToDoDetailedView : Activity
    {
        private ITaskRepository _taskRepository;
        private IServiceProvider _provider;
        private TextView taskTitle;
        private EditText taskContent;
        private Button update;
        private Button back;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_todo_detailedview);

            InyectDependencies();

            Bundle bundle = Intent.Extras;

            int taskId = bundle.GetInt("TaskId");

            Task task = _taskRepository.GetTaskById(taskId);

            taskTitle = FindViewById<TextView>(Resource.Id.txtTaskTitle);
            taskContent = FindViewById<EditText>(Resource.Id.txtTaskContent);
            update = FindViewById<Button>(Resource.Id.btnDetailViewUpdate);
            back = FindViewById<Button>(Resource.Id.btnDetailViewBack);

            taskTitle.Text = task.Title;
            taskContent.Text = task.TextDescription;

            update.Click += delegate 
            {
                task.Title = taskTitle.Text;
                task.TextDescription = taskContent.Text;

                _taskRepository.UpdateTask(task);

                Toast.MakeText(this, "Tarea Actualizada", ToastLength.Long).Show();
            };

            back.Click += delegate
            {
                StartActivity(typeof(ToDoMainView));
                Finish();
            };

        }


        private void InyectDependencies()
        {
            IServiceCollection services = new ServiceCollection();

            string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "users.db3");
            services.AddSingleton<IDataBaseService>(new Context(path));
            services.AddAutoMapper(typeof(SQLiteProfile));

            services.AddScoped<ITaskRepository, TaskService>();

            _provider = services.BuildServiceProvider();

            _taskRepository = _provider.GetService<ITaskRepository>();
        }
    }
}