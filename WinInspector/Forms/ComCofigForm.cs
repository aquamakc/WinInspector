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

namespace WinInspector.Forms
{
    public partial class ComCofigForm : Form
    {
        SerialPort sp = null;

        public ComCofigForm(SerialPort sp)
        {
            InitializeComponent();
            this.sp = sp;
            cb_portName.Items.AddRange(SerialPort.GetPortNames());
            cb_portName.SelectedItem = sp.PortName;
            cb_baudRates.Items.AddRange(new string[] { "9600", "19200" });
            cb_baudRates.SelectedItem = sp.BaudRate.ToString();
            cb_dataBits.Items.AddRange(new string[] { "5", "6", "7", "8" });
            cb_dataBits.SelectedItem = sp.DataBits.ToString();
            cb_parity.Items.AddRange(Enum.GetNames(typeof(Parity)));
            cb_parity.SelectedItem = sp.Parity.ToString();
            cb_stopBits.Items.AddRange(Enum.GetNames(typeof(StopBits)));
            cb_stopBits.SelectedItem = sp.StopBits.ToString();
        }

        private void b_ok_Click(object sender, EventArgs e)
        {
            sp.PortName = cb_portName.SelectedItem.ToString();
            sp.BaudRate = int.Parse(cb_baudRates.SelectedItem.ToString());
            sp.DataBits = int.Parse(cb_dataBits.SelectedItem.ToString());
            sp.Parity = (Parity)Enum.Parse(typeof(Parity), cb_parity.SelectedItem.ToString());
            sp.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cb_stopBits.SelectedItem.ToString());
            this.DialogResult = DialogResult.OK;
        }

        private void b_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
