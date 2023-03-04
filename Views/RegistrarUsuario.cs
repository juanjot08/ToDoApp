using Android.App;
using Android.OS;

namespace ToDoApp
{
    [Activity(Label = "RegistrarUsuario")]
    public class RegistrarUsuario : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_registrar_usuario);

        }
    }
}