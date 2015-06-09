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

        private Thread thrDownload;
        private Stream strResponse;
        private Stream strLocal;
        private HttpWebRequest webRequest;
        private HttpWebResponse webResponse;
        private static int PercentProgress;
        private delegate void UpdateProgessCallback(Int64 BytesRead, Int64 TotalBytes);
 
        public main()
        {
            InitializeComponent();
        }

        string link = "";
        int row;

        private void main_Load(object sender, EventArgs e)
        {

            comboBox1.Visible = false;

            panel5.Visible = true;
            panel6.Visible = false;
            panel7.Visible = false;

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
            Application.Exit();
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

            comboBox1.Visible = true;
            panel5.Visible =false;
            panel6.Visible = true;
            panel7.Visible = false;

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
          
            comboBox1.Visible = false;

            panel5.Visible = true;
            panel6.Visible = false;
            panel7.Visible = false;
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
           dataGridView1.Rows.Add(15);
           dataGridView1.Rows[0].Cells[0].Value = "Dayz Epoch 1.0.5.1";
           dataGridView1.Rows[0].Cells[1].Value = "Not Installed";
           dataGridView1.Rows[0].Cells[2].Value = "12 MB";
           dataGridView1.Rows[0].Cells[4].Value = "Download";
           dataGridView1.Rows[0].Cells[5].Value = "http://ghostzgamerz.com/downloads/@DayZ_Epoch.zip";

           dataGridView1.Rows[1].Cells[0].Value = "Dayz Overwatch 0.2.5";
           dataGridView1.Rows[1].Cells[1].Value = "Not Installed";
           dataGridView1.Rows[1].Cells[2].Value = "4 MB";
           dataGridView1.Rows[1].Cells[4].Value = "Download";

           dataGridView1.Rows[2].Cells[0].Value = "Dayz Taviana 2.0";
           dataGridView1.Rows[2].Cells[1].Value = "Not Installed";
           dataGridView1.Rows[2].Cells[2].Value = "MB";
           dataGridView1.Rows[2].Cells[4].Value = "Download";

           dataGridView1.Rows[3].Cells[0].Value = "Dayz Panthera 1.8.1";
           dataGridView1.Rows[3].Cells[1].Value = "Not Installed";
           dataGridView1.Rows[3].Cells[2].Value = "23.2 MB";
           dataGridView1.Rows[3].Cells[4].Value = "Download";

           dataGridView1.Rows[4].Cells[0].Value = "Dayz Lingor 1.8.1";
           dataGridView1.Rows[4].Cells[1].Value = "Not Installed";
           dataGridView1.Rows[4].Cells[2].Value = "14.3 MB";
           dataGridView1.Rows[4].Cells[4].Value = "Download";

           dataGridView1.Rows[5].Cells[0].Value = "Dayz Namalsk 0.75";
           dataGridView1.Rows[5].Cells[1].Value = "Not Installed";
           dataGridView1.Rows[5].Cells[2].Value = "3.9 MB";
           dataGridView1.Rows[5].Cells[4].Value = "Download";

           dataGridView1.Rows[6].Cells[0].Value = "Dayz Origins 1.7.9.5";
           dataGridView1.Rows[6].Cells[1].Value = "Not Installed";
           dataGridView1.Rows[6].Cells[2].Value = "9.0 MB";
           dataGridView1.Rows[6].Cells[4].Value = "Download";

           dataGridView1.Rows[7].Cells[0].Value = "";

           dataGridView1.Rows[8].Cells[0].Value = "Epoch 0.3.0.3";
           dataGridView1.Rows[8].Cells[1].Value = "Not Installed";
           dataGridView1.Rows[8].Cells[2].Value = "2.1 MB";
           dataGridView1.Rows[8].Cells[4].Value = "Download";

           dataGridView1.Rows[9].Cells[0].Value = "JSRS: DragonFyre RC4";
           dataGridView1.Rows[9].Cells[1].Value = "Not Installed";
           dataGridView1.Rows[9].Cells[2].Value = "11.0 MB";
           dataGridView1.Rows[9].Cells[4].Value = "Download";

           dataGridView1.Rows[10].Cells[0].Value = "MAS Weapons 1.8";
           dataGridView1.Rows[10].Cells[1].Value = "Not Installed";
           dataGridView1.Rows[10].Cells[2].Value = "5 MB";
           dataGridView1.Rows[10].Cells[4].Value = "Download";

           dataGridView1.Rows[11].Cells[0].Value = "MAS Vehicles 1.0";
           dataGridView1.Rows[11].Cells[1].Value = "Not Installed";
           dataGridView1.Rows[11].Cells[2].Value = "10 MB";
           dataGridView1.Rows[11].Cells[4].Value = "Download";

           dataGridView1.Rows[12].Cells[0].Value = "All In ARMA Terrain Pack 1.4.1";
           dataGridView1.Rows[12].Cells[1].Value = "Not Installed";
           dataGridView1.Rows[12].Cells[2].Value = "18 MB";
           dataGridView1.Rows[12].Cells[4].Value = "Download";

           dataGridView1.Rows[13].Cells[0].Value = "Bornholm 1.3";
           dataGridView1.Rows[13].Cells[1].Value = "Not Installed";
           dataGridView1.Rows[13].Cells[2].Value = "4 MB";
           dataGridView1.Rows[13].Cells[4].Value = "Download";

           dataGridView1.Rows[14].Cells[0].Value = "Esseker 0.61";
           dataGridView1.Rows[14].Cells[1].Value = "Not Installed";
           dataGridView1.Rows[14].Cells[2].Value = "21.0 MB";
           dataGridView1.Rows[14].Cells[4].Value = "Download";
       
        }

        private void btn_mods_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns[5].Visible = false;

            load_mods();
            comboBox1.Visible = false;

            comboBox1.Visible = false;

            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = true;
        }

        string flink = "";
        int frow;

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;

                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    link = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value.ToString();
                    row = dataGridView1.CurrentRow.Index;

                    flink = link;
                    row = frow;

                    thrDownload = new Thread(Download);
                    thrDownload.Start();
                }
            }
            catch
            {

            }
        }

        private void UpdateProgress(Int64 BytesRead, Int64 TotalBytes)
        {
            // Calculate the download progress in percentages
            PercentProgress = Convert.ToInt32((BytesRead * 100) / TotalBytes);
            dataGridView1.Rows[frow].Cells[3].Value = "Downloaded " + BytesRead + " out of " + TotalBytes + " (" + PercentProgress + "%)";
        }

        private void Download()
        {
            using (WebClient wcDownload = new WebClient())
            {
                try
                {
                    webRequest = (HttpWebRequest)WebRequest.Create(flink);
                    webRequest.Credentials = CredentialCache.DefaultCredentials;
                    webResponse = (HttpWebResponse)webRequest.GetResponse();

                    Int64 fileSize = webResponse.ContentLength;
                    strResponse = wcDownload.OpenRead(flink);
                    strLocal = new FileStream(txtarma1.Text + "\\Dayz Epoch 1.0.5.1.zip", FileMode.Create, FileAccess.Write, FileShare.None);

                    int bytesSize = 0;
                    byte[] downBuffer = new byte[2048];

                    while ((bytesSize = strResponse.Read(downBuffer, 0, downBuffer.Length)) > 0)
                    {
                        strLocal.Write(downBuffer, 0, bytesSize);
                        this.Invoke(new UpdateProgessCallback(this.UpdateProgress), new object[] { strLocal.Length, fileSize });
                    }
                }
                catch { }
                finally
                {
                    strResponse.Close();
                    strLocal.Close();
                }
            }
        }
    }
}
