using Android.App;
using Android.OS;
using Android.Widget;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Models.Entities.Interfaces;
using ToDoApp.Models.Repository.SQLite.Services;
using ToDoApp.Models.Repository.SQLite;
using System.IO;
using System;

namespace ToDoApp
{
    [Activity(Label = "RegistrarUsuario")]
    public class RegistrarUsuario : Activity
    {
        private TextView user;
        private TextView password;
        private IServiceProvider serviceProvider;
        private IUsuariosRepository usuariosRepository;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_registrar_usuario);

            RegistrarDependencias();

            Button buttonRegister = FindViewById<Button>(Resource.Id.singUpbtnRegister);
            Button buttonBack = FindViewById<Button>(Resource.Id.singUpbtnBack);

            user = FindViewById<TextView>(Resource.Id.singUpEmail);
            password = FindViewById<TextView>(Resource.Id.singUpPassword);

            buttonRegister.Click += delegate
            {
                usuariosRepository.CrearUsuario(user.Text, password.Text);

                StartActivity(typeof(MainActivity));
                
                Finish();
            };

            buttonBack.Click += delegate
            {
                StartActivity(typeof(MainActivity));

                Finish();
            };

        }

        private void RegistrarDependencias()
        {
            IServiceCollection services = new ServiceCollection();

            string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "users.db3");
            services.AddSingleton<IDataBaseService>(new Context(path));
            services.AddAutoMapper(typeof(SQLiteProfile));
            services.AddScoped<IUsuariosRepository, UserService>();

            serviceProvider = services.BuildServiceProvider();

            usuariosRepository = serviceProvider.GetService<IUsuariosRepository>();

        }
    }
}