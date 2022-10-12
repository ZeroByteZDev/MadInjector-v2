using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Madinjector2
{
    public partial class ProcList : Form
    {
        public ProcList()
        {
            InitializeComponent();
        }

        private void ProcList_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Process[] processes = Process.GetProcesses();
            for (int i = 0; i < processes.Length; i++)
            {
                Process p = processes[i];
                listBox1.Items.Add(p.ProcessName);
            }
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.Items.Count != 0)
                {
                    MainForm.GetInstance().Processname(listBox1.SelectedItem.ToString());
                    this.Hide();
                }
            }
            catch
            {
                MessageBox.Show("Please select a process.", "No process was selected | MadInjector", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void windowlistbtn_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (var proc in Process.GetProcesses())
            {
                if (!string.IsNullOrEmpty(proc.MainWindowTitle))
                {
                    listBox1.Items.Add(proc.ProcessName);
                }
            }
        }

        private void allprocbtn_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Process[] processes = Process.GetProcesses();
            for (int i = 0; i < processes.Length; i++)
            {
                Process p = processes[i];
                listBox1.Items.Add(p.ProcessName);
            }
        }
    }
}
