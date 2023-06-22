using Android;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Com.Karumi.Dexter;
using Com.Karumi.Dexter.Listener;
using Com.Karumi.Dexter.Listener.Single;
using EDMTDev.ZXingXamarinAndroid;

namespace App3
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IPermissionListener
    {
        private ZXingScannerView scannerView;
        private TextView txtResult;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //Inin view
            txtResult = FindViewById<TextView>(Resource.Id.txt_result);
            scannerView = FindViewById<ZXingScannerView>(Resource.Id.zxscan);

            //request permission

            //Dexter.WithActivity(this)
            //    .WithPermission(Manifest.Permission.Camera)
            //    .WithListener(this)
            //    .Check();

            scannerView.SetResultHandler(new MyResultHandler(this));
            scannerView.StartCamera();

        }

        protected override void OnDestroy()
        {
            scannerView.StopCamera();
            base.OnDestroy();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OnPermissionDenied(PermissionDeniedResponse p0)
        {
            Toast.MakeText(this, "You must enable this permission", ToastLength.Long).Show();
            //throw new System.NotImplementedException();
        }

        public void OnPermissionGranted(PermissionGrantedResponse p0)
        {
            //throw new System.NotImplementedException();
            scannerView.SetResultHandler(new MyResultHandler(this));
            scannerView.StartCamera();
        }

        public void OnPermissionRationaleShouldBeShown(PermissionRequest p0, IPermissionToken p1)
        {
            //throw new System.NotImplementedException();
        }

        private class MyResultHandler : IResultHandler
        {
            private MainActivity mainActivity;

            public MyResultHandler(MainActivity mainActivity)
            {
                this.mainActivity = mainActivity;
            }

            public void HandleResult(ZXing.Result rawResult)
            {
                mainActivity.txtResult.Text = rawResult.Text;
            }
        }
    }
}