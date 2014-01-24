using DPCtlUruNet;
using DPUruNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEFinalProject {
    public partial class MainWindow : Form {
        private Reader fingerprintReader;


        public MainWindow() {
            InitializeComponent();
            // InitializeVars();
        }

        public void InitializeVars() {
            
        }

        private void MainWindow_Load(Object sender, EventArgs e) {
            ReaderCollection readers = ReaderCollection.GetReaders();
            if (readers.Count == 0) {
                // Fingerprint Reader Not Found
                MessageBox.Show(this, "Fingerprint Device Not Found!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            } else {
                // Fingerprint Reader Found
                fingerprintReader = readers.FirstOrDefault();
                Constants.ResultCode result = fingerprintReader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);

                if (result != Constants.ResultCode.DP_SUCCESS) {
                    // Error Opening Connection To Device
                    MessageBox.Show(this, "Error Connecting To Fingerprint Device!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e) {
            if (fingerprintReader != null) {
                fingerprintReader.Dispose();
            }
        }
    }
}
