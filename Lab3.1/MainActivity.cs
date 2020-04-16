using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;

namespace Lab3._1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            var calc = FindViewById<Button>(Resource.Id.button1);
            calc.Click += (sender, e) =>
            {
                var input = FindViewById<EditText>(Resource.Id.editText2);
                if (input.Text == string.Empty) return;

                if (!Int32.TryParse(input.Text, out int number)) return;

                var output = FindViewById<TextView>(Resource.Id.textView1);
                var resp = Fermat.Factorise(number);
                output.Text = $"Result: {resp.Item1}{System.Environment.NewLine}Elapsed milliseconds: {resp.Item2}";
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}