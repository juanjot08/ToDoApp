using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Models.Repository.SQLite;
using System.IO;
using ToDoApp.Models.Entities.Interfaces;
using ToDoApp.Models.Repository.SQLite.Services;
using Android.Widget;
using ToDoApp.Views;
using ToDoApp.Models.Entities;
using Android.Content;
using Context = ToDoApp.Models.Repository.SQLite.Context;

namespace ToDoApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private IUsuariosRepository usuariosRepository;
        private IServiceProvider serviceProvider;
        private TextView user;
        private TextView password;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            RegistrarDependencias();

            Button buttonLogin = FindViewById<Button>(Resource.Id.btnInicioSesion);
            Button buttonSingUp = FindViewById<Button>(Resource.Id.btnRegistrar);
            user = FindViewById<TextView>(Resource.Id.campoEmail);
            password = FindViewById<TextView>(Resource.Id.campoContra);

            buttonLogin.Click += BtnLoginOnClick;
            buttonSingUp.Click += BtnSingUpnOnClick;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void BtnLoginOnClick(object sender, EventArgs eventArgs)
        {
            Usuarios usuario = usuariosRepository.SelecionarUno(user.Text, password.Text);

            if (usuario != null)
            {
                var sharedPreferences = Application.Context.GetSharedPreferences("ToDoApp", FileCreationMode.Private);

                var editor = sharedPreferences.Edit();

                editor.PutInt("UserId", usuario.Id);
                editor.Commit();

                StartActivity(typeof(ToDoMainView));
                Finish();
            }
            else
            {
                Toast.MakeText(this, "Usuario o clave errados", ToastLength.Long).Show();
            }
        }

        private void BtnSingUpnOnClick(object sender, EventArgs eventArgs)
        {
            StartActivity(typeof(RegistrarUsuario));
        }

        private void RegistrarDependencias()
        {
            IServiceCollection services = new ServiceCollection();

            string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "users.db3");
            services.AddSingleton<IDataBaseService>(new Context(path));
            services.AddAutoMapper(typeof(SQLiteProfile));

            services.AddScoped<IUsuariosRepository,UserService>();

            serviceProvider = services.BuildServiceProvider();

            usuariosRepository = serviceProvider.GetService<IUsuariosRepository>();

        }
	}
}
