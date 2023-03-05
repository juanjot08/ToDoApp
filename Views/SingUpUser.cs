using Android.App;
using Android.OS;
using Android.Widget;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Models.Entities.Interfaces;
using ToDoApp.Models.Repository.SQLite.Services;
using ToDoApp.Models.Repository.SQLite;
using System.IO;
using System;
using System.Net.Mail;

namespace ToDoApp
{
    [Activity(Label = "RegistrarUsuario")]
    public class SingUpUser : Activity
    {
        private TextView user;
        private TextView password;
        private IServiceProvider serviceProvider;
        private IUsersRepository usersRepository;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_sing_up_user);

            InyectDependencies();

            Button buttonRegister = FindViewById<Button>(Resource.Id.singUpbtnRegister);
            Button buttonBack = FindViewById<Button>(Resource.Id.singUpbtnBack);

            user = FindViewById<TextView>(Resource.Id.singUpEmail);
            password = FindViewById<TextView>(Resource.Id.singUpPassword);

            buttonRegister.Click += delegate
            {
                if (IsValidInformation(user.Text, password.Text)) {

                    usersRepository.CrearUsuario(user.Text, password.Text);

                    StartActivity(typeof(MainActivity));

                    Finish();
                }
                else
                {
                    Toast.MakeText(this, "Digite correctamente los campos del formulario", ToastLength.Long).Show();
                }
            };

            buttonBack.Click += delegate
            {
                StartActivity(typeof(MainActivity));

                Finish();
            };

        }

        private bool IsValidInformation(string email, string password)
        {
            return !string.IsNullOrWhiteSpace(email)
                  & !string.IsNullOrWhiteSpace(password)
                  & IsValidEmail(email);
        }

        private void InyectDependencies()
        {
            IServiceCollection services = new ServiceCollection();

            string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "users.db3");
            services.AddSingleton<IDataBaseService>(new Context(path));
            services.AddAutoMapper(typeof(SQLiteProfile));
            services.AddScoped<IUsersRepository, UserService>();

            serviceProvider = services.BuildServiceProvider();

            usersRepository = serviceProvider.GetService<IUsersRepository>();

        }

        private bool IsValidEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}