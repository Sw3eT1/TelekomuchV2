using System;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;

namespace XModem
{
    public partial class Form1 : Form
    {
        private string[] allPorts = SerialPort.GetPortNames(); 
        private SerialPort serialPort1 = new();
        private SerialPort serialPort2 = new();
        string  dataOutput;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            cBoxCOMPORT1.Items.AddRange(allPorts);
            cBoxCOMPORT2.Items.AddRange(allPorts);
        }

        private void cBoxCOMPORT1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedPort = cBoxCOMPORT1.SelectedItem.ToString();

            var updatedPorts = allPorts.Where(p => p != selectedPort).ToArray();

            cBoxCOMPORT2.Items.Clear();
            cBoxCOMPORT2.Items.AddRange(updatedPorts);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                string portName1 = cBoxCOMPORT1.Text;
                string portName2 = cBoxCOMPORT2.Text;

                if (portName1 == portName2)
                {
                    throw new Exception("Nie mo¿na wybraæ tych samych portów COM.");
                }

                serialPort1.PortName = portName1;
                serialPort2.PortName = portName2;

                serialPort1.BaudRate = Convert.ToInt32(cBoxBAUDRATE.Text);
                serialPort2.BaudRate = Convert.ToInt32(cBoxBAUDRATE.Text);

                serialPort1.DataBits = Convert.ToInt32(cBoxDATABITS.Text);
                serialPort2.DataBits = Convert.ToInt32(cBoxDATABITS.Text);

                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cBoxSTOPBITS.Text);
                serialPort2.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cBoxSTOPBITS.Text);

                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cBoxPARITYBITS.Text);
                serialPort2.Parity = (Parity)Enum.Parse(typeof(Parity), cBoxPARITYBITS.Text);

                serialPort1.Open();
                serialPort2.Open();

                progressBar1.Value = 100;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cBoxCOMPORT2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedPort = cBoxCOMPORT2.SelectedItem.ToString();

            var updatedPorts = allPorts.Where(p => p != selectedPort).ToArray();

            cBoxCOMPORT1.Items.Clear();
            cBoxCOMPORT1.Items.AddRange(updatedPorts);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
            if (serialPort2.IsOpen) serialPort2.Close();
            progressBar1.Value = 0;
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            try
            {
                if(tBoxDataInput.Text == "")
                {
                    throw new Exception("Brak danych do wyslania");
                }

                dataOutput = tBoxDataInput.Text;
                serialPort1.Write(dataOutput);
                serialPort2.Write(serialPort1.ReadExisting());
                tBoxDataOutput.Text = serialPort2.ReadExisting();

            }catch (Exception err)
            {
                MessageBox.Show(err.Message, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
