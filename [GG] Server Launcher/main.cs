using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.IO;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;
using System.Collections;
using System.Threading;
using System.Diagnostics;

namespace _GG__Server_Launcher
{
    public partial class main : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        private const int FEATURE_DISABLE_NAVIGATION_SOUNDS = 21;
        private const int SET_FEATURE_ON_THREAD = 0x00000001;
        private const int SET_FEATURE_ON_PROCESS = 0x00000002;
        private const int SET_FEATURE_IN_REGISTRY = 0x00000004;
        private const int SET_FEATURE_ON_THREAD_LOCALMACHINE = 0x00000008;
        private const int SET_FEATURE_ON_THREAD_INTRANET = 0x00000010;
        private const int SET_FEATURE_ON_THREAD_TRUSTED = 0x00000020;
        private const int SET_FEATURE_ON_THREAD_INTERNET = 0x00000040;
        private const int SET_FEATURE_ON_THREAD_RESTRICTED = 0x00000080;

        public main()
        {
            InitializeComponent();
        }

        private void main_Load(object sender, EventArgs e)
        {
            run_ie9();
            webBrowser1.Navigate("http://ghostzgamerz.com/forums/breaking-news.118/");
            loaad_settings();
            try
            {
                StreamReader inStream;
                WebRequest webRequest = WebRequest.Create("http://192.99.38.184/launcher/search_server.php");
                WebResponse webresponse = webRequest.GetResponse();

                inStream = new StreamReader(webresponse.GetResponseStream());
                textBox1.Text = inStream.ReadToEnd();

            }
            catch
            {
                //MessageBox.Show("No internet connection detected");
            }

            StringBuilder lineInfo = new StringBuilder();
            lineInfo.Append(textBox1.Lines.Length.ToString() + "\n");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void run_ie9()
        {
            try
            {
                string executablePath = Environment.GetCommandLineArgs()[0];
                string executableName = System.IO.Path.GetFileName(executablePath);

                RegistryKey registrybrowser = Registry.CurrentUser.OpenSubKey
                   (@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true);

                if (registrybrowser == null)
                {
                    RegistryKey registryFolder = Registry.CurrentUser.OpenSubKey
                        (@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl", true);
                    registrybrowser = registryFolder.CreateSubKey("FEATURE_BROWSER_EMULATION");
                }
                registrybrowser.SetValue(executableName, 11000, RegistryValueKind.DWord);
                registrybrowser.Close();
            }
            catch { }
        }

        string servername = "[GG] Overpoch Taviana 1.0.5.1 |Coins|SlowZs|Group";
        private void get_servername()
        {
            //WebClient wc = new WebClient();
            //string htmlString = wc.DownloadString("http://www.gametracker.com/server_info/us.ghostzgamerz.com:3302/");
            //Match mTitle = Regex.Match(htmlString, @"</span>(.*?)&nbsp;<span class=");
            //if (mTitle.Success)
            //{
            //    servername = mTitle.Groups[1].Value;

                view_listview();

        }

        private void view_listview()
        {
            string[] items = { servername, "?", "A2-OP" /*, ........... */};
            ListViewItem lvi = new ListViewItem(items);
            listView1.Items.Add(lvi);

        }

        private void btn_server_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            get_servername();

            webBrowser1.Visible = false;
            comboBox1.Visible = true;
            listView2.Visible = false;
        }

        class WebPostRequest
        {
            WebRequest theRequest;
            HttpWebResponse theResponse;
            ArrayList theQueryData;

            public WebPostRequest(string url)
            {
                theRequest = WebRequest.Create(url);
                theRequest.Method = "POST";
                theQueryData = new ArrayList();
            }

            public void Add(string key, string value)
            {
                theQueryData.Add(String.Format("{0}={1}", key, HttpUtility.UrlEncode(value)));
            }

            public string GetResponse()
            {

                theRequest.ContentType = "application/x-www-form-urlencoded";

                string Parameters = String.Join("&", (String[])theQueryData.ToArray(typeof(string)));
                theRequest.ContentLength = Parameters.Length;

                StreamWriter sw = new StreamWriter(theRequest.GetRequestStream());
                sw.Write(Parameters);
                sw.Close();

                theResponse = (HttpWebResponse)theRequest.GetResponse();
                StreamReader sr = new StreamReader(theResponse.GetResponseStream());
                return sr.ReadToEnd();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            webBrowser1.Visible = true;
            comboBox1.Visible = false;
            listView2.Visible = false;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
        private void btn_off_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (panel4.Height > 475) timer1.Enabled = false;
            else panel4.Height += 10;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (panel4.Height <10) timer2.Enabled = false;
            else panel4.Height -= 10;
        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            timer2.Enabled = true;
        }

        private void btn_path_Click(object sender, EventArgs e)
        {
            Process.Start(@"c:\windows\");
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                //write ini file
                StreamWriter myStreamWriter = null;
                string InputString;

                try
                {
                    //settings save into text file
                    InputString =txtarma1.Text + Environment.NewLine;       //1
                    InputString += txtarma2.Text + Environment.NewLine;
                    InputString += txtarma3.Text + Environment.NewLine;
                    InputString += txtarma5.Text + Environment.NewLine; //2

                    myStreamWriter = File.CreateText(Application.StartupPath + @"\config.dat");

                    myStreamWriter.Write(InputString);
                    myStreamWriter.Flush();

                }
                catch (Exception exc)
                {
                    MessageBox.Show("File could not be created or written to." + Environment.NewLine + "Please verify that the filename is correct, and that you have write permissions for the desired directory." + Environment.NewLine + Environment.NewLine + "Exception: " + exc.Message);
                }
                finally
                {
                    //dispose the stream writer
                    if (myStreamWriter != null)
                    {
                        myStreamWriter.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loaad_settings()
        {
            StreamReader myStreamReader = null;
            string OutputString;

            int rowCount = 1;

            try
            {
                myStreamReader = File.OpenText(Application.StartupPath + @"\config.dat");

                OutputString = myStreamReader.ReadLine();

                while (OutputString != null)
                {
                    switch (rowCount)
                    {
                        case 1: txtarma1.Text = OutputString; break;
                        case 2: txtarma2.Text = OutputString; break;
                        case 3: txtarma3.Text = OutputString; break;
                        case 4: txtarma5.Text = OutputString; break;
                    }

                    rowCount++;
                    OutputString = myStreamReader.ReadLine();
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show("Setting file could not be read." + Environment.NewLine + "Please verify that the filename is correct, and that you have read permissions for the desired directory." + Environment.NewLine + Environment.NewLine + "Exception: " + exc.Message);
            }
            finally
            {
                if (myStreamReader != null)
                {
                    myStreamReader.Close();
                }
            }
        }

        private void btn_browse1_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog op1 = new FolderBrowserDialog();
            op1.ShowDialog();
            txtarma1.Text = op1.SelectedPath.ToString();
        }

        private void btn_browse2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog op1 = new FolderBrowserDialog();
            op1.ShowDialog();
            txtarma2.Text = op1.SelectedPath.ToString();
        }

        private void btn_browse3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog op1 = new FolderBrowserDialog();
            op1.ShowDialog();
            txtarma3.Text = op1.SelectedPath.ToString();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            Process.Start("http://ghostzgamerz.com/donate/");
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Process.Start("http://ghostzgamerz.com/forums/bug-reports.170/");
        }

        private void label3_Click(object sender, EventArgs e)
        {
          Process.Start("http://ghostzgamerz.com/");
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.teamspeak.com/invite/ts.ghostzgamerz.com/");
        }

        private void load_mods()
        {
           string [] arr = new string[3];
           ListViewItem itm;

           arr[0] = "Dayz Epoch 1.0.5.1";
           arr[1] = "Not Installed";
           arr[2] = "12 MB";
           itm = new ListViewItem(arr);
           listView2.Items.Add(itm);

           arr[0] = "Dayz Overwatch 0.2.5";
           arr[1] = "Not Installed";
           arr[2] = "4 MB";
           itm = new ListViewItem(arr);
           listView2.Items.Add(itm);

           arr[0] = "Dayz Taviana 2.0";
           arr[1] = "Not Installed";
           arr[2] = "5 MB";
           itm = new ListViewItem(arr);
           listView2.Items.Add(itm);

           arr[0] = "Dayz Panthera 1.8.1";
           arr[1] = "Not Installed";
           arr[2] = "23.2 MB";
           itm = new ListViewItem(arr);
           listView2.Items.Add(itm);

           arr[0] = "Dayz Lingor 1.8.1";
           arr[1] = "Not Installed";
           arr[2] = "14.3 MB";
           itm = new ListViewItem(arr);
           listView2.Items.Add(itm);

           arr[0] = "Dayz Namalsk 0.75";
           arr[1] = "Not Installed";
           arr[2] = "3.9 MB";
           itm = new ListViewItem(arr);
           listView2.Items.Add(itm);

           arr[0] = "Dayz Origins 1.7.9.5";
           arr[1] = "Not Installed";
           arr[2] = "9.0 MB";
           itm = new ListViewItem(arr);
           listView2.Items.Add(itm);


           arr[0] = "Epoch 0.3.0.3";
           arr[1] = "Not Installed";
           arr[2] = "2.1 MB";
           itm = new ListViewItem(arr);
           listView2.Items.Add(itm);

           arr[0] = "JSRS: DragonFyre RC4";
           arr[1] = "Not Installed";
           arr[2] = "11.0 MB";
           itm = new ListViewItem(arr);
           listView2.Items.Add(itm);

           arr[0] = "MAS Weapons 1.8";
           arr[1] = "Not Installed";
           arr[2] = "5 MB";
           itm = new ListViewItem(arr);
           listView2.Items.Add(itm);

           arr[0] = "MAS Vehicles 1.0";
           arr[1] = "Not Installed";
           arr[2] = "10 MB";
           itm = new ListViewItem(arr);
           listView2.Items.Add(itm);

           arr[0] = "All In ARMA Terrain Pack 1.4.1";
           arr[1] = "Not Installed";
           arr[2] = "18 MB";
           itm = new ListViewItem(arr);
           listView2.Items.Add(itm);

           arr[0] = "Bornholm 1.3";
           arr[1] = "Not Installed";
           arr[2] = "4 MB";
           itm = new ListViewItem(arr);
           listView2.Items.Add(itm);

           arr[0] = "Esseker 0.61";
           arr[1] = "Not Installed";
           arr[2] = "21.0 MB";
           itm = new ListViewItem(arr);
           listView2.Items.Add(itm);
           
        }

        private void btn_mods_Click(object sender, EventArgs e)
        {
            load_mods();
            listView2.Visible = true;
            webBrowser1.Visible = false;
            comboBox1.Visible = false;
        }
    }
}
