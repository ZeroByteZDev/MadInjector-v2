using Madinjector2.Handler;
using Madinjector2.Injection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Madinjector2
{
    public partial class MainForm : Form
    {
        static private MainForm instance;
        static public MainForm GetInstance()
        {
            return instance;
        }

        public MainForm()
        {
            instance = this;
            InitializeComponent();
            LoadLibFolder();
        }

        public void Processname(String text)
        {
            this.procname.Text = text;
        }

        private void Updater()
        {
            try
            {
                WebClient webClient = new WebClient();
                var newversion = webClient.DownloadString("https://website.com/injector.txt");
                var newversionparsed = int.Parse(newversion);
                if (110 < newversionparsed)
                {
                    notifyIcon1.BalloonTipTitle = "MadInjector New Version Found!";
                    notifyIcon1.BalloonTipText = "New Version Found! [Click Here to Download New Version]";
                    notifyIcon1.ShowBalloonTip(1000);
                    updatecheckerlbl.Text = "New Version Found! [Click Here to Download New Version]";
                    updatecheckerlbl.BackColor = Color.Green;
                }
                else
                {
                    updatecheckerlbl.Text = "You have the latest version!";
                }

            }
            catch
            {
                updatecheckerlbl.Text = "Can't find new updates.";
                notifyIcon1.BalloonTipTitle = "MadInjector New Version Found!";
                notifyIcon1.BalloonTipText = "New Version Found! [Click Here to Download New Version]";
                notifyIcon1.ShowBalloonTip(1000);
            }
        }

        public static void LoadLibFolder()
        {
            try
            {
                string Path = @"C:\Windows\MadInjector";
                bool exists = System.IO.Directory.Exists(Path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(Path);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Unknown Error has occured.\n" + ex.Message + " \nPlease Screenshot this and send it to the developer!", "Unknown Error | MadInjector", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void DelClosing()
        {
            try
            {
                Directory.Delete("C:/Windows/MadInjector/", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Unknown Error has occured.\n" + ex.Message + " \nPlease Screenshot this and send it to the developer!", "Unknown Error | MadInjector", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void siticoneControlBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void browsedllbtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Dll Files|*.dll*" })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        dllpathbox.Text = ofd.FileName;
                        FileInfo fileInfo = new FileInfo(dllpathbox.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Unknown Error has occured.\n" + ex.Message + " \nPlease Screenshot this and send it to the developer!", "Unknown Error | MadInjector", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadlibchk_CheckedChanged(object sender, EventArgs e)
        {
            if (loadlibchk.Checked)
            {
                manualmapchk.Checked = false;
            }
        }

        private void manualmapchk_CheckedChanged(object sender, EventArgs e)
        {
            if (manualmapchk.Checked)
            {
                loadlibchk.Checked = false;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DelClosing();
            Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Process.GetProcessesByName(procname.Text).Length == 0)
                {
                    if (themechk.Checked)
                    {
                        labelproc.ForeColor = Color.Black;
                    }
                    else
                    {
                        labelproc.ForeColor = Color.White;
                    }
                    labelproc.Text = "Process Not found";
                    ProcessIcon.Image = Utils.ProcessIcon;
                }
                else
                {
                    labelproc.ForeColor = Color.FromArgb(1, 23, 203, 17);
                    labelproc.Text = "Process Found!";
                    ProcessIcon.Image = Icon.ExtractAssociatedIcon(Process.GetProcessesByName(procname.Text.Split(new string[] { " | " }, StringSplitOptions.None)[0]).FirstOrDefault().MainModule.FileName).ToBitmap();

                }
            }
            catch
            {

            }

        }

        private void ontopchk_CheckedChanged(object sender, EventArgs e)
        {
            if (ontopchk.Checked)
            {
                TopMost = true;
            }
            else
            {
                TopMost = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            siticoneSeparator1.FillColor = siticoneColorTransition1.Value;
            injbtn2.BorderColor = siticoneColorTransition2.Value;
            autoinjchk.CheckedState.FillColor = siticoneColorTransition1.Value;
            manualmapchk.CheckedState.FillColor = siticoneColorTransition1.Value;
            loadlibchk.CheckedState.FillColor = siticoneColorTransition1.Value;
            siticoneMetroTrackBar1.ThumbColor = siticoneColorTransition1.Value;
        }

        private void colortransitionchk_CheckedChanged(object sender, EventArgs e)
        {
            if (colortransitionchk.Checked)
            {
                timer2.Enabled = true;
            }
            else
            {
                timer2.Enabled = false;
            }
        }

        private async void injbtn2_Click(object sender, EventArgs e)
        {
            try
            {
                if (procname.Text == "")
                {
                    MessageBox.Show("Please select a process", "Process Unknown | MadInjector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (dllpathbox.Text == "")
                {
                    MessageBox.Show("Please browse your DLL to be Injected.", "DLL Not Selected | MadInjector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (loadlibchk.Checked)
                    {
                        injbtn2.Enabled = false;
                        injbtn2.Text = "Injecting...";
                        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                        var stringChars = new char[10];
                        var random = new Random();
                        for (int i = 0; i < stringChars.Length; i++)
                        {
                            stringChars[i] = chars[random.Next(chars.Length)];
                        }
                        var finalString = new String(stringChars);
                        File.Copy(dllpathbox.Text, @"C:/Windows/MadInjector/" + finalString + "_" + ".dll", true);
                        await Task.Delay(TimeSpan.FromSeconds(siticoneMetroTrackBar1.Value));
                        InjectMethod.Inject(@"C:/Windows/MadInjector/" + finalString + "_" + ".dll", procname.Text);
                        MessageBox.Show("DLL Loaded to " + procname.Text + ".", "DLL Loaded | MadInjector", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        injbtn2.Enabled = true;
                        injbtn2.Text = "Inject";
                    }
                    else if (manualmapchk.Checked)
                    {
                        injbtn2.Enabled = false;
                        injbtn2.Text = "Injecting...";
                        var name = procname.Text;
                        var target = Process.GetProcessesByName(name).FirstOrDefault();
                        var file = File.ReadAllBytes(dllpathbox.Text);
                        var injector = new ManualMapInjector(target) { AsyncInjection = true };
                        await Task.Delay(TimeSpan.FromSeconds(siticoneMetroTrackBar1.Value));
                        label2.Text = $"hmodule = 0x{injector.Inject(file).ToInt64():x8}";
                        MessageBox.Show("DLL Loaded to " + procname.Text + ".", "DLL Loaded | MadInjector", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        injbtn2.Enabled = true;
                        injbtn2.Text = "Inject";
                    }
                } 
            }
            catch
            {
                
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void proclist_Click(object sender, EventArgs e)
        {
            new ProcList().ShowDialog();
        }

        private void updatecheckerlbl_Click(object sender, EventArgs e)
        {
            Process.Start("https://website.com/");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            updatecheckerlbl.Text = "Checking for updates...";
            //Updater();
        }

        private void siticoneMetroTrackBar1_ValueChanged(object sender, EventArgs e)
        {
            //siticoneColorTransition1.Interval = siticoneMetroTrackBar1.Value;
            label10.Text = siticoneMetroTrackBar1.Value + " Seconds";
        }

        private void themechk_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
