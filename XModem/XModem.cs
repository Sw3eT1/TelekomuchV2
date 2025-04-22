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

        public bool UseCrc { get; set; } = false;

        public void Send(SerialPort port, string filePath, bool useCrc)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            int packetNumber = 1;
            int offset = 0;
            int dataSize = 128;
            int blockSize = dataSize + 5; // Powrót do pierwotnego rozmiaru

            if (useCrc)
            {
                while (port.ReadByte() != 0x43) ;
            }
            else
            {
                while (port.ReadByte() != NAK) ;
            }

            while (offset < fileBytes.Length)
            {
                byte[] packet = new byte[dataSize];
                int bytesToCopy = Math.Min(dataSize, fileBytes.Length - offset);
                Array.Copy(fileBytes, offset, packet, 0, bytesToCopy);
                if (bytesToCopy < dataSize)
                {
                    for (int i = bytesToCopy; i < dataSize; i++) packet[i] = CPMEOF;
                }

                byte[] block = new byte[blockSize];
                block[0] = SOH;
                block[1] = (byte)packetNumber;
                block[2] = (byte)~packetNumber;
                Array.Copy(packet, 0, block, 3, dataSize);

                if (useCrc)
                {
                    ushort calculatedCrc = CalculateCRC(packet);
                    block[blockSize - 2] = (byte)(calculatedCrc >> 8);  // Starszy bajt CRC (indeks 131)
                    block[blockSize - 1] = (byte)(calculatedCrc & 0xFF); // Młodszy bajt CRC (indeks 132)
                }
                else
                {
                    block[blockSize - 2] = CalcChecksum(packet); // Suma kontrolna (indeks 131) - używamy tylko jednego bajtu
                                                                 // W przypadku braku CRC, bajt na indeksie blockSize - 1 (132) pozostaje nieużywany dla sumy kontrolnej.
                }

                port.Write(block, 0, block.Length);

                int response = port.ReadByte();
                if (response == ACK)
                {
                    offset += dataSize;
                    packetNumber++;
                    if (packetNumber > 255) packetNumber = 0;
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

        public void Receive(SerialPort port, string savePath, bool useCrc)
        {
            using FileStream fs = new FileStream(savePath, FileMode.Create);
            int dataSize = 128;
            int blockSize = dataSize + 5;

            if (useCrc)
            {
                port.Write(new byte[] { 0x43 }, 0, 1);
            }
            else
            {
                port.Write(new byte[] { NAK }, 0, 1);
            }

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
                {
                    continue;
                }

                byte[] block = new byte[blockSize];
                block[0] = (byte)firstByte;
                port.Read(block, 1, blockSize - 1);

                byte receivedPacketNumber = block[1];
                byte receivedPacketNumberComplement = block[2];
                byte[] data = new byte[dataSize];
                Array.Copy(block, 3, data, 0, dataSize);

                bool isPacketNumberValid = (receivedPacketNumber + receivedPacketNumberComplement) == 0xFF;
                bool isDataValid = false;

                if (useCrc)
                {
                    ushort receivedCrc = (ushort)((block[blockSize - 2] << 8) | block[blockSize - 1]);
                    ushort calculatedCrc = CalculateCRC(data);
                    isDataValid = receivedCrc == calculatedCrc;
                }
                else
                {
                    byte receivedChecksum = block[blockSize - 2];
                    byte calculatedChecksum = CalcChecksum(data);
                    isDataValid = receivedChecksum == calculatedChecksum;
                    // W przypadku braku CRC, ignorujemy bajt na indeksie blockSize - 1.
                }

                if (isPacketNumberValid && isDataValid && receivedPacketNumber == (byte)packetNumber)
                {
                    fs.Write(data, 0, data.Length);
                    port.Write(new byte[] { ACK }, 0, 1);
                    packetNumber++;
                    if (packetNumber > 255) packetNumber = 0;
                }
                else if (receivedPacketNumber == (byte)packetNumber)
                {
                    port.Write(new byte[] { ACK }, 0, 1);
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

        public static ushort CalculateCRC(byte[] data)
        {
            ushort crc = 0;
            ushort polynomial = 0x1021;

            foreach (byte b in data)
            {
                crc ^= (ushort)(b << 8);
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x8000) != 0)
                    {
                        crc = (ushort)((crc << 1) ^ polynomial);
                    }
                    else
                    {
                        crc <<= 1;
                    }
                }
            }
            return crc;
        }
    }
}
