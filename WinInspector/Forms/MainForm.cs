using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InCore;

namespace WinInspector.Forms
{
    public partial class MainForm : Form
    {
        Worker worker = null;
        Device Device = null;

        public MainForm(Worker worker, Device Device)
        {
            InitializeComponent();
            this.worker = worker;
            this.Device = Device;
            lPortName.Text = worker.PortName;
            Device.ParamChanges += Device_ParamChanges;
        }

        private void Device_ParamChanges()
        {
            UpdateData();
        }

        public void UpdateData()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(UpdateData));
                return;
            }
            l_voltaage.Text = Device.Voltage.ToString();
            l_current.Text = Device.Current.ToString();
            l_power.Text = Device.Power.ToString();
            l_frequency.Text = Device.Frequency.ToString();
            l_readedTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void bPortOpen_Click(object sender, EventArgs e)
        {
            var isOpen = worker.OpenPort();
            lPortStat.Text = isOpen ? "Открыт" : "Закрыт";
            bPortOpen.Enabled = !isOpen;
            bPortClose.Enabled = isOpen;
            bReadParams.Enabled = isOpen;
            worker.BeginReadParams();
        }

        private void bPortClose_Click(object sender, EventArgs e)
        {
            var isOpen = worker.ClosePort();
            lPortStat.Text = isOpen ? "Открыт" : "Закрыт";
            bPortOpen.Enabled = isOpen;
            bPortClose.Enabled = !isOpen;
            bReadParams.Enabled = isOpen;
        }

        private void bPortConfig_Click(object sender, EventArgs e)
        {
            worker.ShowConfigForm();
            lPortName.Text = worker.PortName;
        }

        private void bReadParams_Click(object sender, EventArgs e)
        {
            worker.BeginReadParams();
        }
    }
}
