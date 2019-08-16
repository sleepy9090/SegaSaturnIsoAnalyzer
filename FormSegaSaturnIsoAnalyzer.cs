/**
 *  @file           FormSegaSaturnIsoAnalyzer.cs
 *  @brief          Form class for Sega Saturn ISO image modification front-end 
 *  @copyright      2019 Shawn M. Crawford [sleepy]
 *  @date           08/16/2019
 *
 *  @remark Author  Shawn M. Crawford [sleepy]
 *
 *  @note           N/A
 *
 */
using System;
using System.Windows.Forms;

namespace SegaSaturnIsoAnalyzer
{
    public partial class FormSegaSaturnIsoAnalyzer : Form
    {
        private string _path;

        public FormSegaSaturnIsoAnalyzer()
        {
            InitializeComponent();
            SetTextBoxMaxLength();
            SetNonModifiableTextBoxesReadOnly();

            //SetTextBoxesReadOnly();
            //SetCompatiblePeripheralsCheckBoxesReadOnly();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = @"Open file...";
            openFileDialog.Filter = @"iso/bin/img files (*.iso, *.bin, *.img)|*.iso;*.bin;*.img|All files (*.*)|*.*";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _path = openFileDialog.FileName;

                ClearCheckBoxes();
                ParseROMFile();
            }
        }

        private void ParseROMFile()
        {
            SSROMInfo ssRomInfo = new SSROMInfo(_path);
            string compatibleAreaSymbols = ssRomInfo.GetCompatibleAreaSymbol();
            string compatiblePeripherals = ssRomInfo.GetCompatiblePeripheral();

            textBoxHardwareIdentifier.Text = ssRomInfo.GetHardwareIdentifier();
            textBoxMakerId.Text = ssRomInfo.GetMakerId();
            textBoxProductNumber.Text = ssRomInfo.GetProductNumber();
            textBoxVersion.Text = ssRomInfo.GetVersion();
            textBoxReleaseDate.Text = ssRomInfo.GetReleaseDate();
            textBoxDeviceInformation.Text = ssRomInfo.GetDeviceInformation();
            textBoxCompatibleAreaSymbol.Text = compatibleAreaSymbols;
            textBoxCompatiblePeripheral.Text = compatiblePeripherals;
            textBoxGameTitle.Text = ssRomInfo.GetGameTitle();
            textBoxIpSize.Text = ssRomInfo.GetIpSize();
            textBoxStackM.Text = ssRomInfo.GetStackM();
            textBoxStackS.Text = ssRomInfo.GetStackS();
            textBoxFirstReadAddress.Text = ssRomInfo.GetFirstReadAddress();
            textBoxFirstReadSize.Text = ssRomInfo.GetFirstReadSize();

            SetCompatibleAreaSymbolsCheckboxes(compatibleAreaSymbols);
            SetCompatiblePeripheralsCheckBoxes(compatiblePeripherals);
        }

        private void SetTextBoxMaxLength()
        {
            textBoxHardwareIdentifier.MaxLength = 0x10;
            textBoxMakerId.MaxLength = 0x10;
            textBoxProductNumber.MaxLength = 0xA;
            textBoxVersion.MaxLength = 0x6;
            textBoxReleaseDate.MaxLength = 0x8;
            textBoxDeviceInformation.MaxLength = 0x8;
            textBoxCompatibleAreaSymbol.MaxLength = 0xA;
            textBoxCompatiblePeripheral.MaxLength = 0x10;
            textBoxGameTitle.MaxLength = 0x70;
            textBoxIpSize.MaxLength = 0x4;
            textBoxStackM.MaxLength = 0x4;
            textBoxStackS.MaxLength = 0x4;
            textBoxFirstReadAddress.MaxLength = 0x4;
            textBoxFirstReadSize.MaxLength = 0x4;
        }

        private void SetCompatiblePeripheralsCheckBoxes(string compatiblePeripherals)
        {
            foreach (char c in compatiblePeripherals)
            {
                switch (c)
                {
                    case 'J':
                        checkBoxJControlPad.Checked = true;
                        break;
                    case 'M':
                        checkBoxMMouse.Checked = true;
                        break;
                    case 'G':
                        checkBoxGGunVirtuaGun.Checked = true;
                        break;
                    case 'W':
                        checkBoxWRamCarts.Checked = true;
                        break;
                    case 'S':
                        checkBoxSSteeringWheel.Checked = true;
                        break;
                    case 'A':
                        checkBoxAVirtuaStick.Checked = true;
                        break;
                    case 'E':
                        checkBoxEAnalogController.Checked = true;
                        break;
                    case 'T':
                        checkBoxTMultiTap.Checked = true;
                        break;
                    case 'C':
                        checkBoxCLinkCable.Checked = true;
                        break;
                    case 'D':
                        checkBoxDLinkCableDirectLink.Checked = true;
                        break;
                    case 'X':
                        checkBoxXXBandModemNetlinkModem.Checked = true;
                        break;
                    case 'K':
                        checkBoxKKeyboard.Checked = true;
                        break;
                    case 'Q':
                        checkBoxQPachinkoController.Checked = true;
                        break;
                    case 'F':
                        checkBoxFFloppyDiskDrive.Checked = true;
                        break;
                    case 'R':
                        checkBoxRRomCart.Checked = true;
                        break;
                    case 'P':
                        checkBoxPVideoCdCard.Checked = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetCompatibleAreaSymbolsCheckboxes(string compatibleAreaSymbols)
        {
            foreach (char c in compatibleAreaSymbols)
            {
                switch (c)
                {
                    case 'J':
                        checkBoxJJapan.Checked = true;
                        break;
                    case 'T':
                        checkBoxTAsia.Checked = true;
                        break;
                    case 'U':
                        checkBoxUNorthAmerica.Checked = true;
                        break;
                    case 'B':
                        checkBoxBCentralSouthAmerica.Checked = true;
                        break;
                    case 'K':
                        checkBoxKorea.Checked = true;
                        break;
                    case 'A':
                        checkBoxAEastAsia.Checked = true;
                        break;
                    case 'E':
                        checkBoxEEurope.Checked = true;
                        break;
                    case 'L':
                        checkBoxLCentralSouthAmerica.Checked = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void ClearCheckBoxes()
        {
            foreach (Control control in Controls)
            {
                if (control is GroupBox)
                {
                    foreach (Control control2 in control.Controls)
                    {
                        if (control2 is CheckBox)
                        {
                            CheckBox checkBox = (CheckBox)control2;
                            checkBox.Checked = false;
                        }
                    }
                }
            }
        }

        private void SetCompatiblePeripheralsCheckBoxesReadOnly()
        {
            foreach (Control control in Controls)
            {
                if (control is GroupBox && control.Text == @"Compatible Peripherals:")
                {
                    foreach (Control control2 in control.Controls)
                    {
                        if (control2 is CheckBox)
                        {
                            CheckBox checkBox = (CheckBox)control2;
                            checkBox.Enabled = false;
                        }
                    }
                }
            }
        }

        private void SetNonModifiableTextBoxesReadOnly()
        {

            textBoxCompatibleAreaSymbol.ReadOnly = true;
            textBoxCompatiblePeripheral.ReadOnly = true;

        }

        private void SetTextBoxesReadOnly()
        {
            foreach (Control control in Controls)
            {
                if (control is GroupBox)
                {
                    foreach (Control control2 in control.Controls)
                    {
                        if (control2 is TextBox)
                        {
                            TextBox textBox = (TextBox)control2;
                            textBox.ReadOnly = true;
                        }
                    }
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void UpdateTextBoxCompatiblePeripherals(object sender, EventArgs e)
        {
            string newCompatiblePeripheralsString = "";

            if (checkBoxJControlPad.Checked)
            {
                newCompatiblePeripheralsString += "J";
            }

            if (checkBoxMMouse.Checked)
            {
                newCompatiblePeripheralsString += "M";
            }

            if (checkBoxGGunVirtuaGun.Checked)
            {
                newCompatiblePeripheralsString += "G";
            }

            if (checkBoxWRamCarts.Checked)
            {
                newCompatiblePeripheralsString += "W";
            }

            if (checkBoxSSteeringWheel.Checked)
            {
                newCompatiblePeripheralsString += "S";
            }

            if (checkBoxAVirtuaStick.Checked)
            {
                newCompatiblePeripheralsString += "A";
            }

            if (checkBoxEAnalogController.Checked)
            {
                newCompatiblePeripheralsString += "E";
            }

            if (checkBoxTMultiTap.Checked)
            {
                newCompatiblePeripheralsString += "T";
            }

            if (checkBoxCLinkCable.Checked)
            {
                newCompatiblePeripheralsString += "C";
            }

            if (checkBoxDLinkCableDirectLink.Checked)
            {
                newCompatiblePeripheralsString += "D";
            }

            if (checkBoxXXBandModemNetlinkModem.Checked)
            {
                newCompatiblePeripheralsString += "X";
            }

            if (checkBoxKKeyboard.Checked)
            {
                newCompatiblePeripheralsString += "K";
            }

            if (checkBoxQPachinkoController.Checked)
            {
                newCompatiblePeripheralsString += "Q";
            }

            if (checkBoxFFloppyDiskDrive.Checked)
            {
                newCompatiblePeripheralsString += "F";
            }

            if (checkBoxRRomCart.Checked)
            {
                newCompatiblePeripheralsString += "R";
            }

            if (checkBoxPVideoCdCard.Checked)
            {
                newCompatiblePeripheralsString += "P";
            }

            textBoxCompatiblePeripheral.Text = newCompatiblePeripheralsString;
        }

        private void UpdateTextBoxCompatibleAreaSymbol(object sender, EventArgs e)
        {
            string newCompatibleAreaSymbolString = "";

            if (checkBoxJJapan.Checked)
            {
                newCompatibleAreaSymbolString += "J";
            }

            if (checkBoxTAsia.Checked)
            {
                newCompatibleAreaSymbolString += "T";
            }

            if (checkBoxUNorthAmerica.Checked)
            {
                newCompatibleAreaSymbolString += "U";
            }

            if (checkBoxBCentralSouthAmerica.Checked)
            {
                newCompatibleAreaSymbolString += "B";
            }

            if (checkBoxKorea.Checked)
            {
                newCompatibleAreaSymbolString += "K";
            }

            if (checkBoxAEastAsia.Checked)
            {
                newCompatibleAreaSymbolString += "A";
            }

            if (checkBoxEEurope.Checked)
            {
                newCompatibleAreaSymbolString += "E";
            }

            if (checkBoxLCentralSouthAmerica.Checked)
            {
                newCompatibleAreaSymbolString += "L";
            }

            textBoxCompatibleAreaSymbol.Text = newCompatibleAreaSymbolString;
        }

        private void buttonPatchPeripherals_Click(object sender, EventArgs e)
        {
            try
            {
                SSROMInfo ssRomInfo = new SSROMInfo(_path);
                string newCompatiblePeripheralsString = textBoxCompatiblePeripheral.Text;
                ssRomInfo.WriteCompatiblePeripherals(newCompatiblePeripheralsString);

                MessageBox.Show(@"Peripherals updated.", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Console.WriteLine(exception);
                //throw;
            }
        }

        private void buttonPatchHeader_Click(object sender, EventArgs e)
        {
            try
            {
                SSROMInfo ssRomInfo = new SSROMInfo(_path);
                string newHardwareIdentifier = textBoxHardwareIdentifier.Text;
                string newMakerId = textBoxMakerId.Text;
                string newProductNumber = textBoxProductNumber.Text;
                string newVersion = textBoxVersion.Text;
                string newReleaseDate = textBoxReleaseDate.Text;
                string newDeviceInformation = textBoxDeviceInformation.Text;
                string newCompatibleAreaSymbols = textBoxCompatibleAreaSymbol.Text;
                string newCompatiblePeripheralsString = textBoxCompatiblePeripheral.Text;
                string newGameTitle = textBoxGameTitle.Text;
                string newIpSize = textBoxIpSize.Text;
                string newStackM = textBoxStackM.Text;
                string newStackS = textBoxStackS.Text;
                string newFirstReadAddress = textBoxFirstReadAddress.Text;
                string newFirstReadSize = textBoxFirstReadSize.Text;

                ssRomInfo.WriteHardwareIdentifier(newHardwareIdentifier);
                ssRomInfo.WriteMakerId(newMakerId);
                ssRomInfo.WriteProductNumber(newProductNumber);
                ssRomInfo.WriteVersion(newVersion);
                ssRomInfo.WriteReleaseDate(newReleaseDate);
                ssRomInfo.WriteDeviceInformation(newDeviceInformation);
                ssRomInfo.WriteCompatibleAreaSymbol(newCompatibleAreaSymbols);
                ssRomInfo.WriteCompatiblePeripherals(newCompatiblePeripheralsString);
                ssRomInfo.WriteGameTitle(newGameTitle);
                ssRomInfo.WriteIpSize(newIpSize);
                ssRomInfo.WriteStackM(newStackM);
                ssRomInfo.WriteStackS(newStackS);
                ssRomInfo.WriteFirstReadAddress(newFirstReadAddress);
                ssRomInfo.WriteFirstReadSize(newFirstReadSize);

                MessageBox.Show(@"Header updated.", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Console.WriteLine(exception);
                //throw;
            }
        }

        private void buttonPatchRegion_Click(object sender, EventArgs e)
        {
            try
            {
                SSROMInfo ssRomInfo = new SSROMInfo(_path);
                string newCompatibleAreaSymbols = textBoxCompatibleAreaSymbol.Text;
                ssRomInfo.WriteCompatibleAreaSymbol(newCompatibleAreaSymbols);

                MessageBox.Show(@"Region updated.", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Console.WriteLine(exception);
                //throw;
            }

        }
    }
}
