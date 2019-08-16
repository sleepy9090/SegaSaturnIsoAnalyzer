/**
 *  @file           SSROMInfo.cs
 *  @brief          Class for modifying Sega Saturn ISO images 
 *  @copyright      2019 Shawn M. Crawford [sleepy]
 *  @date           08/16/2019
 *
 *  @remark Author  Shawn M. Crawford [sleepy]
 *
 *  @note           N/A
 *
 */
using System;
using System.IO;
using System.Linq;

namespace SegaSaturnIsoAnalyzer
{
    public class SSROMInfo
    {
        private string _path;

        public SSROMInfo(string gamePath)
        {
            _path = gamePath;
        }

        public string GetHardwareIdentifier()
        {
            return ConvertHexToAscii(GetHexStringFromFile(0x10, 0x10));
        }

        public string GetMakerId()
        {
            return ConvertHexToAscii(GetHexStringFromFile(0x20, 0x10));
        }

        public string GetProductNumber()
        {
            return ConvertHexToAscii(GetHexStringFromFile(0x30, 0xA));
        }

        public string GetVersion()
        {
            return ConvertHexToAscii(GetHexStringFromFile(0x3A, 0x6));
        }

        public string GetReleaseDate()
        {
            return ConvertHexToAscii(GetHexStringFromFile(0x40, 0x8));
        }

        public string GetDeviceInformation()
        {
            return ConvertHexToAscii(GetHexStringFromFile(0x48, 0x8));
        }

        /*
         * J : Japan
         * T : Asia (NTSC)
         * U : North America
         * B : Central/South America (NTSC)
         * K : Korea
         * A : East Asia (PAL)
         * E : Europe (PAL)
         * L : Central/South America (PAL)
         */
        public string GetCompatibleAreaSymbol()
        {
            return ConvertHexToAscii(GetHexStringFromFile(0x50, 0xA));
        }

        /*
         * J : Control Pad
         * M : Mouse
         * G : Gun/Virtua Gun
         * W : RAM Carts (1MB and/or 4MB)
         * S : Steering Wheel/Racing Controller/Arcade Racer
         * A : Virtua Stick/Mission Stick and/or Analog Controller
         * E : Analog Controller/3D-pad/ Multi controller
         * T : Multi-Tap
         * C : Link Cable
         * D : Link Cable/DirectLink
         * X : X-Band modem/Netlink modem
         * K : Keyboard
         * Q : Pachinko Controller/Pachinko Handle Model Personal Use Controller
         * F : Floppy Disk Drive
         * R : ROM cart
         * P : Video CD Card/Mpeg Movie Card
         * Non-specific/Unknown : Twin Stick controller
         * Non-specific/Unknown : Arcade Stick controller
         * Non-specific/Unknown : Backup RAM cart
         * Non-specific/Unknown : Densha de Go! Controller
         * Non-specific/Unknown : Brain controller
         */
        public string GetCompatiblePeripheral()
        {
            return ConvertHexToAscii(GetHexStringFromFile(0x60, 0x10));
        }

        public string GetGameTitle()
        {
            return ConvertHexToAscii(GetHexStringFromFile(0x70, 0x70));
        }

        public string GetIpSize()
        {
            return GetHexStringFromFile(0xF0, 0x4);
        }

        public string GetStackM()
        {
            return GetHexStringFromFile(0xF8, 0x4);
        }

        public string GetStackS()
        {
            return GetHexStringFromFile(0xFC, 0x4);
        }

        public string GetFirstReadAddress()
        {
            return GetHexStringFromFile(0x100, 0x4);
        }

        public string GetFirstReadSize()
        {
            return GetHexStringFromFile(0x104, 0x4);
        }

        public void WriteHardwareIdentifier(string hardwareIdentifier)
        {
            WriteByteArrayToFile(0x10, StringToByteArray(ConvertAsciiToHex(hardwareIdentifier.PadRight(0x10, ' '))));
        }

        public void WriteMakerId(string makerId)
        {
            WriteByteArrayToFile(0x20, StringToByteArray(ConvertAsciiToHex(makerId.PadRight(0x10, ' '))));
        }

        public void WriteProductNumber(string productNumber)
        {
            WriteByteArrayToFile(0x30, StringToByteArray(ConvertAsciiToHex(productNumber.PadRight(0xA, ' '))));
        }

        public void WriteVersion(string version)
        {
            WriteByteArrayToFile(0x3A, StringToByteArray(ConvertAsciiToHex(version.PadRight(0x6, ' '))));
        }

        public void WriteReleaseDate(string releaseDate)
        {
            WriteByteArrayToFile(0x40, StringToByteArray(ConvertAsciiToHex(releaseDate.PadRight(0x8, ' '))));
        }

        public void WriteDeviceInformation(string deviceInformation)
        {
            WriteByteArrayToFile(0x48, StringToByteArray(ConvertAsciiToHex(deviceInformation.PadRight(0x8, ' '))));
        }

        public void WriteCompatibleAreaSymbol(string region)
        {
            WriteByteArrayToFile(0x50, StringToByteArray(ConvertAsciiToHex(region.PadRight(0xA, ' '))));
        }

        public void WriteCompatiblePeripherals(string peripheral)
        {
            WriteByteArrayToFile(0x60, StringToByteArray(ConvertAsciiToHex(peripheral.PadRight(0x10, ' '))));
        }

        public void WriteGameTitle(string gameTitle)
        {
            WriteByteArrayToFile(0x70, StringToByteArray(ConvertAsciiToHex(gameTitle.PadRight(0x70, ' '))));
        }

        public void WriteIpSize(string ipSize)
        {
            WriteByteArrayToFile(0xF0, StringToByteArray(ipSize.PadRight(0x4, '0')));
        }

        public void WriteStackM(string stackM)
        {
            WriteByteArrayToFile(0xF8, StringToByteArray(stackM.PadRight(0x4, '0')));
        }

        public void WriteStackS(string stackS)
        {
            WriteByteArrayToFile(0xFC, StringToByteArray(stackS.PadRight(0x4, '0')));
        }

        public void WriteFirstReadAddress(string firstReadAddress)
        {
            WriteByteArrayToFile(0x100, StringToByteArray(firstReadAddress.PadRight(0x4, '0')));
        }

        public void WriteFirstReadSize(string firstReadSize)
        {
            WriteByteArrayToFile(0x104, StringToByteArray(firstReadSize.PadRight(0x4, '0')));
        }

        private static string ConvertAsciiToHex(string asciiString)
        {
            char[] charValues = asciiString.ToCharArray();
            string hexValue = "";
            foreach (char c in charValues)
            {
                int value = Convert.ToInt32(c);
                hexValue += $"{value:X}";
            }
            return hexValue;
        }

        private static string ConvertHexToAscii(string hexString)
        {
            string ascii = string.Empty;

            try
            {
                for (int i = 0; i < hexString.Length; i += 2)
                {
                    string hexSubstring = hexString.Substring(i, 2);
                    uint decimalValue = Convert.ToUInt32(hexSubstring, 16);
                    char character = Convert.ToChar(decimalValue);
                    ascii += character;
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.Message);
                throw ex;
            }

            return ascii;
        }

        private string GetHexStringFromFile(int startPoint, int length)
        {
            string hexString = "";
            using (FileStream fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read))
            {
                fileStream.Seek(startPoint, SeekOrigin.Begin);

                for (int x = 0; x < length; x++)
                {
                    hexString += fileStream.ReadByte().ToString("X2");
                }
            }

            return hexString;
        }

        private static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        private bool WriteByteArrayToFile(int startPoint, byte[] byteArray)
        {
            bool result = false;
            try
            {
                using (FileStream fileStream = new FileStream(_path, FileMode.Open, FileAccess.ReadWrite))
                {
                    fileStream.Position = startPoint;
                    fileStream.Write(byteArray, 0, byteArray.Length);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error writing file: {0}", ex);
                //result = false;
                throw ex;
            }

            return result;
        }
    }
}