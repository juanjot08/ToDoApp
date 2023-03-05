using Android.App;
using Android.OS;
using Android.Widget;

namespace ToDoApp.Views
{
    [Activity(Label = "ForgotPassword")]
    public class ForgotPassword : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_forgot_password);

            Button back = FindViewById<Button>(Resource.Id.forgotPasswordbtnBack);

            back.Click += delegate
            {
                StartActivity(typeof(MainActivity));
                Finish();
            };
        }
    }
}