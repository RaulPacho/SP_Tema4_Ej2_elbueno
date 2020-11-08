using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_Tema4_Ej2_elbueno
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            label1.Text = "";
            Process[] procs = Process.GetProcesses();
            textBox1.Text += String.Format("{0,-10}|{1,-20}|{2,20}\r\n", "PID", "Name", "Main Window");

            foreach (Process p in procs)
            {
                textBox1.Text += String.Format("{0,-10}|{1,-20}|{2,20}\r\n", p.Id, (p.ProcessName.Length > 15 ? p.ProcessName.Substring(0, 12) + "..." : p.ProcessName), (p.MainWindowTitle.Length > 15 ? p.MainWindowTitle.Substring(0, 12) + "..." : p.MainWindowTitle));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool permiso = true;
            label1.Text = "";
            ProcessThreadCollection ptc = null;
            ProcessModuleCollection pmc = null;
            if (int.TryParse(textBox2.Text, out int pid))
            {
                try
                {
                    Process p = Process.GetProcessById(pid);
                    try
                    {
                        ptc = p.Threads;
                        pmc = p.Modules;
                    }
                    catch (SystemException)
                    {
                        permiso = false;
                        label1.Text = "Not enough privileges";
                    }
                    textBox1.Text = String.Format("PID: {0}\r\nName: {1,-20}\r\nMain Window: {2,20}\r\n", p.Id, (p.ProcessName.Length > 15 ? p.ProcessName.Substring(0, 12) + "..." : p.ProcessName), (p.MainWindowTitle.Length > 15 ? p.MainWindowTitle.Substring(0, 12) + "..." : p.MainWindowTitle));

                    if (permiso)
                    {
                        textBox1.Text += "Threads: \r\n";

                        foreach (ProcessThread th in ptc)
                        {
                            textBox1.Text += String.Format("\tId: {0}\r\n\tStart: {1}\r\n\r\n", th.Id, th.StartTime);
                        }

                        textBox1.Text += "Modules: \r\n";

                        foreach (ProcessModule mo in pmc)
                        {
                            textBox1.Text += String.Format("\tName: {0} \r\n\tFileName: {1}\r\n\r\n", mo.ModuleName, mo.FileName);
                        }
                    }

                }
                catch (ArgumentException)
                {
                    textBox1.Text = "";
                    label1.Text = "Didn't find PID";
                }
            }
            else
            {
                textBox1.Text = "";
                label1.Text = "Not a valid PID";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            if (int.TryParse(textBox2.Text, out int pid))
            {
                try
                {
                    Process p = Process.GetProcessById(pid);
                    p.CloseMainWindow();
                    textBox1.Text = "";
                }
                catch (ArgumentException)
                {
                    textBox1.Text = "";
                    label1.Text = "Didn't find PID";
                }
            }
            else
            {
                textBox1.Text = "";
                label1.Text = "Not a valid PID";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            if (int.TryParse(textBox2.Text, out int pid))
            {
                try
                {
                    textBox1.Text = "I said you're killed by death";
                    Process p = Process.GetProcessById(pid);
                    p.Kill();
                }
                catch (ArgumentException)
                {
                    textBox1.Text = "";
                    label1.Text = "Didn't find PID";
                }
                catch (Win32Exception)
                {
                    label1.Text = "You though you have privileges?\r\nNo You don't!";
                }
            }
            else
            {
                textBox1.Text = "";
                label1.Text = "Not a valid PID";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string solProc = textBox2.Text;
            label1.Text = "";
            if (solProc != "")
            {
                try
                {
                    Process p = Process.Start(solProc);
                }
                catch (Win32Exception)
                {
                    label1.Text = "Not a valid process";
                }
            }
            else
            {
                label1.Text = "You didn't even write";
            }
        }
    }
}
