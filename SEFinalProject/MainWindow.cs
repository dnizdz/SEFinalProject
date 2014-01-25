using DPCtlUruNet;
using DPUruNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEFinalProject {
    public partial class MainWindow : Form {
        public Reader fingerprintReader { get; set; }
        private Thread imageCaptureThread;
        private AdminPage adminPage;

        public MainWindow() {
            InitializeComponent();
            InitializeVars();
        }

        public void InitializeVars() {
            this.pictureBox.Image = null;
            this.imageCaptureThread = null;
            this.adminPage = null;
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
                    fingerprintReader.Dispose();
                    this.Close();
                }

                imageCaptureThread = new Thread(CaptureThread);
                imageCaptureThread.IsBackground = true;
                imageCaptureThread.Start();
            }
        }

        private void CaptureThread() {
            while (true) {
                Fid fid = null;

                if (!CaptureFinger(ref fid)) break;
                if (fid == null) {
                    RefreshUIDelegate rid = new RefreshUIDelegate(RefreshUI);
                    this.Invoke(rid, new Object[] { null });

                    continue;
                }

                foreach (Fid.Fiv fiv in fid.Views) {
                    Bitmap bmp = CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
                    RefreshUIDelegate rid = new RefreshUIDelegate(RefreshUI);
                    this.Invoke(rid, new Object[] { bmp });
                }
            }
        }

        private delegate void RefreshUIDelegate(Bitmap bmp);
        private void RefreshUI(Bitmap bmp) {
            if (bmp == null) {
                this.nameTextBox.Text = "";
                this.roleTextBox.Text = "";
                this.operationTextBox.Text = "";
                this.timeTextBox.Text = "";
            } else {
                /*
                this.nameTextBox.Text = "Unknown";
                this.roleTextBox.Text = "Unknown";
                this.operationTextBox.Text = "Unknown";
                this.timeTextBox.Text = DateTime.Now.ToString("h:mm:ss tt");
                */

                if (adminPage == null || adminPage.IsDisposed) {
                    adminPage = new AdminPage(this);
                }

                adminPage.Show();
            }

            pictureBox.Image = bmp;
            pictureBox.Refresh();
        }

        private Boolean CaptureFinger(ref Fid fid) {
            CaptureResult captureResult = fingerprintReader.Capture(Constants.Formats.Fid.ANSI, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, 5000, fingerprintReader.Capabilities.Resolutions.FirstOrDefault());
            if (captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS) {
                return false;
            }

            if (captureResult.Quality == Constants.CaptureQuality.DP_QUALITY_CANCELED) {
                return false;
            }

            if ((captureResult.Quality == Constants.CaptureQuality.DP_QUALITY_NO_FINGER || captureResult.Quality == Constants.CaptureQuality.DP_QUALITY_TIMED_OUT)) {
                return true;
            }

            if ((captureResult.Quality == Constants.CaptureQuality.DP_QUALITY_FAKE_FINGER)) {
                return true;
            }

            fid = captureResult.Data;

            return true;
        }

        private Bitmap CreateBitmap(Byte[] bytes, Int32 width, Int32 height) {
            Byte[] rgbBytes = new byte[bytes.Length * 3];

            for (Int32 i = 0; i <= bytes.Length - 1; i++) {
                rgbBytes[(i * 3)] = bytes[i];
                rgbBytes[(i * 3) + 1] = bytes[i];
                rgbBytes[(i * 3) + 2] = bytes[i];
            }
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            for (int i = 0; i <= bmp.Height - 1; i++) {
                IntPtr p = new IntPtr(data.Scan0.ToInt32() + data.Stride * i);
                System.Runtime.InteropServices.Marshal.Copy(rgbBytes, i * bmp.Width * 3, p, bmp.Width * 3);
            }

            bmp.UnlockBits(data);

            return bmp;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e) {
            if (fingerprintReader != null) {
                fingerprintReader.Dispose();
            }

            this.imageCaptureThread.Abort();
        }

        private void MainWindow_Activated(object sender, EventArgs e) {
            this.pictureBox.Focus();
        }
    }
}
