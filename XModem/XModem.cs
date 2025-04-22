using System;
using System.IO.Ports;
using System.IO;
using System.Threading;

namespace XModem
{
    public class XModemProtocol
    {
        private const byte SOH = 0x01;
        private const byte EOT = 0x04;
        private const byte ACK = 0x06;
        private const byte NAK = 0x15;
        private const byte CAN = 0x18;
        private const byte CPMEOF = 0x1A;
        private const int PacketSize = 128;

        public void Send(SerialPort port, string filePath)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            int packetNumber = 1;
            int offset = 0;

            // Czekamy na NAK
            while (port.ReadByte() != NAK) ;

            while (offset < fileBytes.Length)
            {
                byte[] packet = new byte[PacketSize];
                int bytesToCopy = Math.Min(PacketSize, fileBytes.Length - offset);
                Array.Copy(fileBytes, offset, packet, 0, bytesToCopy);
                if (bytesToCopy < PacketSize)
                    for (int i = bytesToCopy; i < PacketSize; i++) packet[i] = CPMEOF;

                byte[] block = new byte[PacketSize + 5];
                block[0] = SOH;
                block[1] = (byte)packetNumber;
                block[2] = (byte)~packetNumber;
                Array.Copy(packet, 0, block, 3, PacketSize);
                block[PacketSize + 3] = CalcChecksum(packet);

                port.Write(block, 0, block.Length);

                int response = port.ReadByte();
                if (response == ACK)
                {
                    offset += PacketSize;
                    packetNumber++;
                }
                else if (response == NAK)
                {
                    continue; // retry
                }
                else
                {
                    throw new Exception("Nieoczekiwana odpowiedź: " + response);
                }
            }

            // Wysłanie EOT
            port.Write(new byte[] { EOT }, 0, 1);
            while (port.ReadByte() != ACK) ;
        }

        public void Receive(SerialPort port, string savePath)
        {
            using FileStream fs = new FileStream(savePath, FileMode.Create);
            port.Write(new byte[] { NAK }, 0, 1);

            int packetNumber = 1;

            while (true)
            {
                int firstByte = port.ReadByte();
                if (firstByte == EOT)
                {
                    port.Write(new byte[] { ACK }, 0, 1);
                    break;
                }

                if (firstByte != SOH)
                    continue;

                byte[] block = new byte[PacketSize + 5];
                block[0] = (byte)firstByte;
                port.Read(block, 1, block.Length - 1);

                byte blockNum = block[1];
                byte blockComp = block[2];
                byte[] data = new byte[PacketSize];
                Array.Copy(block, 3, data, 0, PacketSize);
                byte checksum = block[PacketSize + 3];

                if (blockNum == (byte)packetNumber && blockComp == (byte)~packetNumber && checksum == CalcChecksum(data))
                {
                    fs.Write(data, 0, data.Length);
                    port.Write(new byte[] { ACK }, 0, 1);
                    packetNumber++;
                }
                else
                {
                    port.Write(new byte[] { NAK }, 0, 1);
                }
            }
        }

        public void SendFile(SerialPort port, string filePath)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            int packetNumber = 1;
            int offset = 0;

            // Czekaj na NAK
            while (port.ReadByte() != NAK) ;

            while (offset < fileBytes.Length)
            {
                byte[] packet = new byte[PacketSize];
                int bytesToCopy = Math.Min(PacketSize, fileBytes.Length - offset);
                Array.Copy(fileBytes, offset, packet, 0, bytesToCopy);
                if (bytesToCopy < PacketSize)
                    for (int i = bytesToCopy; i < PacketSize; i++) packet[i] = CPMEOF;

                byte[] block = new byte[PacketSize + 5];
                block[0] = SOH;
                block[1] = (byte)packetNumber;
                block[2] = (byte)~packetNumber;
                Array.Copy(packet, 0, block, 3, PacketSize);
                block[PacketSize + 3] = CalcChecksum(packet);

                port.Write(block, 0, block.Length);

                int response = port.ReadByte();
                if (response == ACK)
                {
                    offset += PacketSize;
                    packetNumber++;
                }
                else if (response == NAK)
                {
                    continue;
                }
                else
                {
                    throw new Exception("Nieoczekiwana odpowiedź: " + response);
                }
            }

            port.Write(new byte[] { EOT }, 0, 1);
            while (port.ReadByte() != ACK) ;
        }

        public void ReceiveFile(SerialPort port, string savePath)
        {
            using FileStream fs = new FileStream(savePath, FileMode.Create);
            port.Write(new byte[] { NAK }, 0, 1);

            int packetNumber = 1;

            while (true)
            {
                int firstByte = port.ReadByte();
                if (firstByte == EOT)
                {
                    port.Write(new byte[] { ACK }, 0, 1);
                    break;
                }

                if (firstByte != SOH)
                    continue;

                byte[] block = new byte[PacketSize + 5];
                block[0] = (byte)firstByte;
                port.Read(block, 1, block.Length - 1);

                byte blockNum = block[1];
                byte blockComp = block[2];
                byte[] data = new byte[PacketSize];
                Array.Copy(block, 3, data, 0, PacketSize);
                byte checksum = block[PacketSize + 3];

                if (blockNum == (byte)packetNumber && blockComp == (byte)~packetNumber && checksum == CalcChecksum(data))
                {
                    fs.Write(data, 0, data.Length);
                    port.Write(new byte[] { ACK }, 0, 1);
                    packetNumber++;
                }
                else
                {
                    port.Write(new byte[] { NAK }, 0, 1);
                }
            }
        }


        private byte CalcChecksum(byte[] data)
        {
            byte sum = 0;
            foreach (byte b in data)
                sum += b;
            return sum;
        }
    }
}
