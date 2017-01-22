using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Lang;
using Java.Net;
using Console = System.Console;
using Exception = System.Exception;
using Object = Java.Lang.Object;

namespace ListView_MySQL.M_Code.m_MySQL
{
    class Downloader : AsyncTask
    {
        private Context c;
        private string urlAddress;
        private ListView lv;

        private ProgressDialog pd;

        public Downloader(Context c, string urlAddress, ListView lv)
        {
            this.c = c;
            this.urlAddress = urlAddress;
            this.lv = lv;
        }

        protected override void OnPreExecute()
        {
            base.OnPreExecute();
            pd=new ProgressDialog(c);
            pd.SetTitle("Fetch data");
            pd.SetMessage("Fetching..Please wait");
            pd.Show();


        }

        protected override Object DoInBackground(params Object[] @params)
        {
            return DownloadData();
        }

        protected override void OnPostExecute(Object jsonData)
        {
            base.OnPostExecute(jsonData);

            pd.Dismiss();


            if (jsonData == null)
            {
                Toast.MakeText(c,"UnSuccessful,No Data retrieved",ToastLength.Short).Show();
            }
            else
            {
                //PARSER
                new DataParser(c, jsonData.ToString(), lv).Execute();
            }


        }

        private string DownloadData()
        {
            HttpURLConnection con = Connector.connect(urlAddress);
            if (con == null)
            {
                return null;
            }

            try
            {
                Stream s=new BufferedStream(con.InputStream);
                BufferedReader br=new BufferedReader(new InputStreamReader(s));

                string line;
                StringBuffer jsonData=new StringBuffer();

                while ((line=br.ReadLine()) != null)
                {
                    jsonData.Append(line);
                }

                br.Close();
                s.Close();

                return jsonData.ToString();

            }
            catch (Exception e)
            {
                
                Console.WriteLine(e.Message);
            }

            return null;
        }
    }
}