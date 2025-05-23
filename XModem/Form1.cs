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
                    throw new Exception("Nie mo�na wybra� tych samych port�w COM.");
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
                MessageBox.Show(err.Message, "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (tBoxDataInput.Text == "")
                    throw new Exception("Brak danych do wys�ania");

                string tempFilePath = Path.GetTempFileName();
                File.WriteAllText(tempFilePath, tBoxDataInput.Text);

                string receivedFilePath = Path.GetTempFileName();

                var xmodem = new XModemProtocol();

                Thread sendThread = new Thread(() =>
                {
                    xmodem.Send(serialPort1, tempFilePath);
                });

                Thread receiveThread = new Thread(() =>
                {
                    xmodem.Receive(serialPort2, receivedFilePath);
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

                File.Delete(tempFilePath);
                File.Delete(receivedFilePath);
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

                if (!serialPort1.IsOpen || !serialPort2.IsOpen)
                {
                    MessageBox.Show("Porty szeregowe musz� by� otwarte przed wys�aniem danych.", "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Thread receiveThread = new Thread(() =>
                {
                    try
                    {
                        var xmodem = new XModemProtocol();
                        xmodem.ReceiveFile(serialPort2, receivedPath);
                    }
                    catch (Exception ex)
                    {
                        Invoke(new Action(() =>
                        {
                            MessageBox.Show("B��d odbioru: " + ex.Message);
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
                            // Pr�bujemy odczyta� zawarto�� i wklei� do pola tekstowego
                            try
                            {
                                string content = File.ReadAllText(receivedPath);
                                int eofIndex = content.IndexOf((char)0x1A);
                                if (eofIndex >= 0)
                                    content = content.Substring(0, eofIndex);

                                tBoxDataOutput.Text = content;
                                MessageBox.Show("Plik zosta� wys�any i odebrany poprawnie!", "Sukces");
                            }
                            catch
                            {
                                tBoxDataOutput.Text = "[Plik odebrany, ale nie jest w formacie tekstowym]";
                                MessageBox.Show("Plik odebrany, ale nie mo�na wy�wietli� jako tekst.", "Info");
                            }
                        }));
                    }
                    catch (Exception ex)
                    {
                        Invoke(new Action(() =>
                        {
                            MessageBox.Show("B��d wysy�ki: " + ex.Message);
                        }));
                    }
                });

                receiveThread.IsBackground = true;
                sendThread.IsBackground = true;

                receiveThread.Start();
                sendThread.Start();
            }
        }
    }
}
