using Microsoft.VisualBasic.Devices;
using System;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace XModem
{
    public partial class Form1 : Form
    {
        private string[] allPorts = SerialPort.GetPortNames();
        private SerialPort serialPort1 = new();
        private SerialPort serialPort2 = new();
        string dataOutput;

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            AllocConsole();
            cBoxCOMPORT1.Items.AddRange(allPorts);

        }

        private void cBoxCOMPORT1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedPort = cBoxCOMPORT1.SelectedItem.ToString();

            var updatedPorts = allPorts.Where(p => p != selectedPort).ToArray();


        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                string portName1 = cBoxCOMPORT1.Text;



                serialPort1.PortName = portName1;

                serialPort1.BaudRate = Convert.ToInt32(cBoxBAUDRATE.Text);


                if (!serialPort1.IsOpen) serialPort1.Open();

                progressBar1.Value = 100;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();

            progressBar1.Value = 0;
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            try
            {
                if (tBoxDataInput.Text == "")
                    throw new Exception("Brak danych do wys�ania");

                string tempFilePath = Path.GetTempFileName();
                File.WriteAllText(tempFilePath, tBoxDataInput.Text);

                string receivedFilePath = Path.GetTempFileName();

                var xmodem = new XModemProtocol();

                Thread sendThread = new Thread(() =>
                {
                    xmodem.Send(serialPort1, tempFilePath, checkBox1.Checked);
                });

                Thread.Sleep(100);
                sendThread.Start();

                sendThread.Join();

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filePath = ofd.FileName;
                string receivedPath = Path.Combine(Path.GetTempPath(), "received_" + Path.GetFileName(filePath));

                //Thread receiveThread = new Thread(() =>
                //{
                //    try
                //    {
                //        var xmodem = new XModemProtocol();
                //        //xmodem.ReceiveFile(serialPort2, receivedPath);
                //    }
                //    catch (Exception ex)
                //    {
                //        Invoke(new Action(() =>
                //        {
                //            MessageBox.Show("B��d odbioru: " + ex.Message);
                //        }));
                //    }
                //});

                Thread sendThread = new Thread(() =>
                {
                    try
                    {
                        var xmodem = new XModemProtocol();
                        Thread.Sleep(100); // Delay dla NAK
                        xmodem.SendFile(serialPort1, filePath, checkBox1.Checked);
                    }
                    catch (Exception ex)
                    {
                        Invoke(new Action(() =>
                        {
                            MessageBox.Show("B��d wysy�ki: " + ex.Message);
                        }));
                    }
                });

                //receiveThread.IsBackground = true;
                sendThread.IsBackground = true;

                //receiveThread.Start();
                sendThread.Start();
            }
        }

        private void rButtonReceiver_CheckedChanged(object sender, EventArgs e)
        {
            btnSendFile.Enabled = false;
            btnSendData.Enabled = false;
            if (rButtonReceiver.Checked)
            {
                var ofd = new SaveFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = ofd.FileName;
                    var xmodem = new XModemProtocol();
                    xmodem.ReceiveFile(serialPort1, filePath, checkBox1.Checked);
                    string output = File.ReadAllText(filePath);
                }
            }
        }

        private void rButtonTransmiter_CheckedChanged(object sender, EventArgs e)
        {
            btnSendFile.Enabled = true;
            btnSendData.Enabled = true;
        }
    }
}
