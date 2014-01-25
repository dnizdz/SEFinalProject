using DPUruNet;
using System;
using System.Windows.Forms;

namespace SEFinalProject {
    public partial class DeviceInfo : Form {
        private MainWindow mainWindow;

        public DeviceInfo(MainWindow mainWindow) {
            InitializeComponent();
            this.Activated += new System.EventHandler(this.DeviceInfo_Activated);
            this.mainWindow = mainWindow;
        }

        private void refreshTextBox() {
            Reader r = mainWindow.fingerprintReader;

            infoRichTextBox.Text = String.Empty;

            infoRichTextBox.Text += "Device Name: " + r.Description.Name.Remove(r.Description.Name.LastIndexOf(r.Description.SerialNumber)) + "\n";
            infoRichTextBox.Text += "Serial Number: " + r.Description.SerialNumber + "\n";
            infoRichTextBox.Text += "Technology: " + r.Description.Technology + "\n";
            infoRichTextBox.Text += "Can Capture: " + r.Capabilities.CanCapture.ToString() + "\n";
            infoRichTextBox.Text += "Can Stream: " + r.Capabilities.CanStream.ToString() + "\n";
            infoRichTextBox.Text += "Extract Features: " + r.Capabilities.ExtractFeatures.ToString() + "\n";
            infoRichTextBox.Text += "Can Match: " + r.Capabilities.CanMatch.ToString() + "\n";
            infoRichTextBox.Text += "Can Identify: " + r.Capabilities.CanIdentify.ToString() + "\n";
            infoRichTextBox.Text += "Has Fingerprint Storage: " + r.Capabilities.HasFingerprintStorage.ToString() + "\n";
            infoRichTextBox.Text += "Has Power Management: " + r.Capabilities.HasPowerManagement.ToString() + "\n";
            infoRichTextBox.Text += "PIV Compliant: " + r.Capabilities.PIVCompliant.ToString() + "\n";
            infoRichTextBox.Text += "Indicator Type: " + r.Capabilities.IndicatorType.ToString() + "\n";

            foreach (Int32 resolution in r.Capabilities.Resolutions) {
                if (resolution != 0) {
                    infoRichTextBox.Text += "Resolution: " + resolution.ToString() + "\n";
                }
            }
        }

        private void DeviceInfo_Activated(object sender, EventArgs e) {
            this.tableLayoutPanel1.Focus();
            this.refreshTextBox();
        }

        private void infoRichTextBox_Enter(object sender, EventArgs e) {
            this.tableLayoutPanel1.Focus();
        }

        private void resetBtn_Click(object sender, EventArgs e) {
            mainWindow.fingerprintReader.Reset();
            this.refreshTextBox();
        }

        private void calibrateBtn_Click(object sender, EventArgs e) {
            mainWindow.fingerprintReader.Calibrate();
            this.refreshTextBox();
        }
    }
}
