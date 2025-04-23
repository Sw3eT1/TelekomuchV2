using Microsoft.VisualBasic.Devices;
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
        string dataOutput;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
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
                //serialPort2.PortName = portName2;

                serialPort1.BaudRate = Convert.ToInt32(cBoxBAUDRATE.Text);
                //serialPort2.BaudRate = Convert.ToInt32(cBoxBAUDRATE.Text);


                if (!serialPort1.IsOpen) serialPort1.Open();
                //if (!serialPort2.IsOpen) serialPort2.Open();

                progressBar1.Value = 100;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
            //if (serialPort2.IsOpen) serialPort2.Close();
            progressBar1.Value = 0;
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            try
            {
                if (tBoxDataInput.Text == "")
                    throw new Exception("Brak danych do wys³ania");

                string tempFilePath = Path.GetTempFileName();
                File.WriteAllText(tempFilePath, tBoxDataInput.Text);

                string receivedFilePath = Path.GetTempFileName();

                var xmodem = new XModemProtocol();

                Thread sendThread = new Thread(() =>
                {
                    xmodem.Send(serialPort1, tempFilePath, checkBox1.Checked);
                });

                Thread receiveThread = new Thread(() =>
                {
                    // xmodem.Receive(serialPort2, receivedFilePath, checkBox1.Checked);
                });

                receiveThread.Start();
                Thread.Sleep(100);
                sendThread.Start();

                sendThread.Join();
                receiveThread.Join();

                //bez usuwania kwadratow zeby dpoelnilo wiadomosc
                //tBoxDataOutput.Text = File.ReadAllText(receivedFilePath);

                string output = File.ReadAllText(receivedFilePath);
                int eofIndex = output.IndexOf((char)0x1A);
                if (eofIndex >= 0)
                    output = output.Substring(0, eofIndex);

                tBoxDataOutput.Text = output;

                //File.Delete(tempFilePath);
                //File.Delete(receivedFilePath);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filePath = ofd.FileName;
                string receivedPath = Path.Combine(Path.GetTempPath(), "received_" + Path.GetFileName(filePath));

                //if (!serialPort1.IsOpen || !serialPort2.IsOpen)
                //{
                //    MessageBox.Show("Porty szeregowe musz¹ byæ otwarte przed wys³aniem danych.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                Thread receiveThread = new Thread(() =>
                {
                    try
                    {
                        var xmodem = new XModemProtocol();
                        //xmodem.ReceiveFile(serialPort2, receivedPath);
                    }
                    catch (Exception ex)
                    {
                        Invoke(new Action(() =>
                        {
                            MessageBox.Show("B³¹d odbioru: " + ex.Message);
                        }));
                    }
                });

                Thread sendThread = new Thread(() =>
                {
                    try
                    {
                        var xmodem = new XModemProtocol();
                        Thread.Sleep(100); // Delay dla NAK
                        xmodem.SendFile(serialPort1, filePath);

                        Invoke(new Action(() =>
                        {
                            // Próbujemy odczytaæ zawartoœæ i wkleiæ do pola tekstowego
                            try
                            {
                                string content = File.ReadAllText(receivedPath);
                                int eofIndex = content.IndexOf((char)0x1A);
                                if (eofIndex >= 0)
                                    content = content.Substring(0, eofIndex);

                                tBoxDataOutput.Text = content;
                                MessageBox.Show("Plik zosta³ wys³any i odebrany poprawnie!", "Sukces");
                            }
                            catch
                            {
                                tBoxDataOutput.Text = "[Plik odebrany, ale nie jest w formacie tekstowym]";
                                MessageBox.Show("Plik odebrany, ale nie mo¿na wyœwietliæ jako tekst.", "Info");
                            }
                        }));
                    }
                    catch (Exception ex)
                    {
                        Invoke(new Action(() =>
                        {
                            MessageBox.Show("B³¹d wysy³ki: " + ex.Message);
                        }));
                    }
                });

                receiveThread.IsBackground = true;
                sendThread.IsBackground = true;

                receiveThread.Start();
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
                    xmodem.ReceiveFile(serialPort1, filePath);
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
