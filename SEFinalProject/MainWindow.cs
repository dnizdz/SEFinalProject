using DPCtlUruNet;
using DPUruNet;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
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
        public Thread imageCaptureAndDBProcessingThread { get; set; }
        private AdminPage adminPage;

        private const Int32 PROBABILITY_ONE = 0x7fffffff;

        public MainWindow() {
            InitializeComponent();
            InitializeVars();
        }

        public void InitializeVars() {
            this.pictureBox.Image = null;
            this.imageCaptureAndDBProcessingThread = null;
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

                imageCaptureAndDBProcessingThread = new Thread(ImageCaptureAndDBProcessingThread);
                imageCaptureAndDBProcessingThread.IsBackground = true;
                imageCaptureAndDBProcessingThread.Start();
            }

            try {

            } catch (MySqlException) {
                MessageBox.Show(this, "Error Connecting To Database!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void ImageCaptureAndDBProcessingThread() {
            while (true) {
                Fid fid = null;

                if (!CaptureFinger(ref fid)) break;
                if (fid == null) {
                    RefreshUIDelegate rid = new RefreshUIDelegate(RefreshUI);
                    this.Invoke(rid, new Object[] { null, null });

                    continue;
                }

                DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(fid, Constants.Formats.Fmd.ANSI);
                if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS) break;

                Fmd nowBeingScanned = resultConversion.Data;
                EmployeeData result = null;
                // foreach data in database, compare
                try {
                    MySqlConnection mySQLConnection = new MySqlConnection("SERVER=localhost;DATABASE=se;UID=root;PASSWORD=;");
                    mySQLConnection.Open();

                    MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM employee", mySQLConnection);
                    MySqlDataReader reader1 = cmd1.ExecuteReader();

                    while (reader1.Read()) {
                        Fmd currentChecked = JsonConvert.DeserializeObject<Fmd>(reader1.GetString("fmd"));
                        CompareResult compareResult = Comparison.Compare(nowBeingScanned, 0, currentChecked, 0);

                        if (compareResult.ResultCode != Constants.ResultCode.DP_SUCCESS) {
                            break;
                        }

                        if (compareResult.Score < PROBABILITY_ONE / 100000) {
                            result = new EmployeeData();

                            result.empID = reader1.GetInt64("empID");
                            result.name = reader1.GetString("name");
                            result.role = reader1.GetString("role");
                            result.fmd = currentChecked;

                            // Check Attendance
                            MySqlCommand cmd2 = new MySqlCommand();
                            cmd2.Connection = mySQLConnection;
                            cmd2.CommandText = "SELECT * FROM attendance WHERE empID = " + result.empID + " AND clockin BETWEEN \'" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00\' AND \'" + DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59\'";
                            MySqlDataReader reader2 = cmd2.ExecuteReader();

                            if (reader2.HasRows) {
                                // Has clocked in, but hasn't clocked out
                                Boolean hasClockedOut = false;
                                while (reader2.Read()) {
                                    if (reader2.IsDBNull(reader2.GetOrdinal("clockout"))) {
                                        hasClockedOut = true;
                                        break;
                                    }
                                }

                                if (!hasClockedOut) {
                                    MySqlCommand cmd3 = new MySqlCommand();
                                    cmd3.Connection = mySQLConnection;
                                    cmd3.CommandText = "UPDATE attendance SET clockout=@clockout WHERE clockin BETWEEN @clockinstart AND @clockinend";
                                    cmd3.Prepare();

                                    cmd3.Parameters.AddWithValue("@clockout", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                    cmd3.Parameters.AddWithValue("@clockinstart", DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                                    cmd3.Parameters.AddWithValue("@clockinend", DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");

                                    cmd3.ExecuteNonQuery();
                                }
                            } else {
                                // Has not clocked in
                                MySqlCommand cmd3 = new MySqlCommand();
                                cmd3.Connection = mySQLConnection;
                                cmd3.CommandText = "INSERT INTO attendance(empID, clockin, clockout) VALUES(@empID,@clockin,@clockout)";
                                cmd3.Prepare();

                                cmd3.Parameters.AddWithValue("@empID", result.empID);
                                cmd3.Parameters.AddWithValue("@clockin", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                cmd3.Parameters.AddWithValue("@clockout", "NULL");

                                cmd3.ExecuteNonQuery();
                            }

                            break;
                        }
                    }

                    mySQLConnection.Close();
                } catch (MySqlException) {
                    MessageBox.Show(this, "Error Retrieving Data From Database!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }

                foreach (Fid.Fiv fiv in fid.Views) {
                    Bitmap bmp = CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
                    RefreshUIDelegate rid = new RefreshUIDelegate(RefreshUI);
                    this.Invoke(rid, new Object[] { bmp, result });
                }
            }
        }

        private delegate void RefreshUIDelegate(Bitmap bmp, EmployeeData employeeData);
        private void RefreshUI(Bitmap bmp, EmployeeData employeeData) {
            if (bmp == null) {
                this.nameTextBox.Text = String.Empty;
                this.roleTextBox.Text = String.Empty;
                this.operationTextBox.Text = String.Empty;
                this.timeTextBox.Text = String.Empty;
            } else {
                if (employeeData != null) {
                    this.nameTextBox.Text = employeeData.name;
                    this.roleTextBox.Text = employeeData.role;
                    if (employeeData.clockOut.HasValue) {
                        this.operationTextBox.Text = "Clock Out";
                        this.timeTextBox.Text = ((DateTime)employeeData.clockOut).ToString("h:mm:ss tt");
                    } else if (employeeData.clockIn.HasValue) {
                        this.operationTextBox.Text = "Clock In";
                        this.timeTextBox.Text = ((DateTime)employeeData.clockOut).ToString("h:mm:ss tt");
                    }

                    if (employeeData.role.Equals("Administrator")) {
                        if (adminPage == null || adminPage.IsDisposed) {
                            adminPage = new AdminPage(this);
                        }
                        adminPage.Show();
                    }
                } else {
                    this.nameTextBox.Text = "Unknown";
                    this.roleTextBox.Text = "Unknown";
                    this.operationTextBox.Text = "Unknown";
                    this.timeTextBox.Text = DateTime.Now.ToString("h:mm:ss tt");
                }
            }

            pictureBox.Image = bmp;
            pictureBox.Refresh();
        }

        private Boolean CaptureFinger(ref Fid fid) {
            CaptureResult captureResult = fingerprintReader.Capture(Constants.Formats.Fid.ANSI, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, 2500, fingerprintReader.Capabilities.Resolutions.FirstOrDefault());
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

            this.imageCaptureAndDBProcessingThread.Abort();
        }

        private void MainWindow_Activated(object sender, EventArgs e) {
            this.pictureBox.Focus();
        }
    }
}
