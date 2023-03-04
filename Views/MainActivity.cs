using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Models.Repository.SQLite;
using System.IO;
using ToDoApp.Models.Entities.Interfaces;
using ToDoApp.Models.Repository.SQLite.Services;
using Android.Widget;
using ToDoApp.Views;

namespace ToDoApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            RegistrarDependencias();


            Button buttonLogin = FindViewById<Button>(Resource.Id.btnInicioSesion);
            Button buttonSingUp = FindViewById<Button>(Resource.Id.btnRegistrar);
            TextView user = FindViewById<TextView>(Resource.Id.campoEmail);
            TextView password = FindViewById<TextView>(Resource.Id.campoEmail);

            buttonLogin.Click += BtnLoginOnClick;
            buttonSingUp.Click += BtnSingUpnOnClick;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void BtnLoginOnClick(object sender, EventArgs eventArgs)
        {
            StartActivity(typeof(ToDoMainView));
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

            services.AddScoped<IUsuariosRepository,UserService>();

            services.AddAutoMapper(typeof(SQLiteProfile));
        }
	}
}
