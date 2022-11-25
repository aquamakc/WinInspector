using System;
using System.Windows.Forms;
using InCore;

namespace WinInspector.Forms
{
    public partial class MainForm : Form
    {
        readonly Worker worker = null;
        readonly Device Device = null;

        public MainForm(Worker worker, Device Device)
        {
            InitializeComponent();
            this.worker = worker;
            this.Device = Device;
            lPortName.Text = worker.PortName;
            Device.ChangePropertyEvent += Device_ChangePropertyEvent;
        }

        private void Device_ChangePropertyEvent(Device.DevProperties property, double value)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Device.ChangePropertyDelegate(Device_ChangePropertyEvent), new object[] { property, value });
                return;
            }
            switch(property)
            {
                case Device.DevProperties.Voltage:
                    l_voltaage.Text = value.ToString();
                    break;
                case Device.DevProperties.Current:
                    l_current.Text = value.ToString();
                    break;
                case Device.DevProperties.Power:
                    l_power.Text = value.ToString();
                    break;
                case Device.DevProperties.Frequency:
                    l_frequency.Text = value.ToString();
                    break;
                default:
                    break;
            }
            l_readedTime.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        }

        public void UpdateData()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(UpdateData));
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

        private void BPortOpen_Click(object sender, EventArgs e)
        {
            var isOpen = worker.OpenPort();
            lPortStat.Text = isOpen ? "Открыт" : "Закрыт";
            bPortOpen.Enabled = !isOpen;
            bPortClose.Enabled = isOpen;
            bReadParams.Enabled = isOpen;
            worker.BeginReadParams();
        }

        private void BPortClose_Click(object sender, EventArgs e)
        {
            var isOpen = worker.ClosePort();
            lPortStat.Text = isOpen ? "Открыт" : "Закрыт";
            bPortOpen.Enabled = isOpen;
            bPortClose.Enabled = !isOpen;
            bReadParams.Enabled = isOpen;
        }

        private void BPortConfig_Click(object sender, EventArgs e)
        {
            worker.ShowConfigForm();
            lPortName.Text = worker.PortName;
        }

        private void BReadParams_Click(object sender, EventArgs e)
        {
            worker.BeginReadParams();
        }
    }
}
