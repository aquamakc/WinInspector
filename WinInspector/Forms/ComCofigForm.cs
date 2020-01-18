using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using InCore;

namespace WinInspector.Forms
{
    public partial class ComCofigForm : Form
    {
        PortConfig PortConfig = null;

        public ComCofigForm(PortConfig portConfig)
        {
            InitializeComponent();
            this.PortConfig = portConfig;
            cb_portName.Items.AddRange(SerialPort.GetPortNames());
            cb_portName.SelectedItem = portConfig.ComName;
            cb_baudRates.Items.AddRange(new string[] { "9600", "19200" });
            cb_baudRates.SelectedItem = portConfig.BaudRate.ToString();
            cb_dataBits.Items.AddRange(new string[] { "5", "6", "7", "8" });
            cb_dataBits.SelectedItem = portConfig.DataBits.ToString();
            cb_parity.Items.AddRange(Enum.GetNames(typeof(Parity)));
            cb_parity.SelectedItem = Enum.GetName(typeof(Parity), portConfig.Parity).ToString();
            cb_stopBits.Items.AddRange(Enum.GetNames(typeof(StopBits)));
            cb_stopBits.SelectedItem = Enum.GetName(typeof(StopBits), portConfig.StopBits).ToString();
        }

        private void b_ok_Click(object sender, EventArgs e)
        {
            PortConfig.ComName = cb_portName.SelectedItem.ToString();
            PortConfig.BaudRate = int.Parse(cb_baudRates.SelectedItem.ToString());
            PortConfig.DataBits = int.Parse(cb_dataBits.SelectedItem.ToString());
            PortConfig.Parity = (int)Enum.Parse(typeof(Parity), cb_parity.SelectedItem.ToString());
            PortConfig.StopBits = (int)Enum.Parse(typeof(StopBits), cb_stopBits.SelectedItem.ToString());
            this.DialogResult = DialogResult.OK;
        }

        private void b_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
