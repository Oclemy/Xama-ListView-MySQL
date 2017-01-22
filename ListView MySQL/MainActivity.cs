using Android.App;
using Android.OS;
using Android.Widget;
using ListView_MySQL.M_Code.m_MySQL;

namespace ListView_MySQL
{
    [Activity(Label = "ListView_MySQL", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        private static string urlAddress = "http://10.0.2.2/android/spacecraft_select.php";
        private ListView lv;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            lv = FindViewById<ListView>(Resource.Id.lv);
            Button downloadBtn = FindViewById<Button>(Resource.Id.downloadBtn);
            downloadBtn.Click += downloadBtn_Click;

        }

        void downloadBtn_Click(object sender, System.EventArgs e)
        {
            new Downloader(this, urlAddress, lv).Execute();
        }

       
    }
}

