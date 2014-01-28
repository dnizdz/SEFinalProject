using DPUruNet;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SEFinalProject {
    public partial class EnrollmentForm : Form {
        private MainWindow mainWindow;
        private Thread enrollThread { get; set; }
        private Fmd fmd;
        private Int32 trialCount;

        public EnrollmentForm(MainWindow mainWindow) {
            InitializeComponent();
            this.Disposed += new System.EventHandler(this.EnrollmentForm_Disposed);
            this.mainWindow = mainWindow;
            this.enrollThread = null;
            this.fmd = null;
            this.trialCount = 0;
        }

        void EnrollmentForm_Disposed(object sender, EventArgs e) {
            if (this.enrollThread != null) this.enrollThread.Abort();
        }

        private void RefreshUI() {
            if (this.nameTextBox.Text.Length != 0 && this.roleComboBox.SelectedItem != null && this.roleComboBox.SelectedItem.ToString().Length != 0) {
                if (enrollThread == null) {
                    enrollThread = new Thread(EnrollThread);
                    enrollThread.IsBackground = true;
                    enrollThread.Start();
                }
            } else {
                this.saveBtn.Enabled = false;
                this.commandLabel.Text = String.Empty;
                this.pictureBox.Image = null;
            }
        }

        private void EnrollThread() {
            UpdateCommandTextAndPictureBoxDelegate u = new UpdateCommandTextAndPictureBoxDelegate(UpdateCommandTextAndPictureBox);
            this.Invoke(u, new Object[] { null, "Place a finger on the reader." });

            trialCount = 0;
            while (true) {
                DataResult<Fmd> enrollmentResult = Enrollment.CreateEnrollmentFmd(Constants.Formats.Fmd.ANSI, CaptureAndExtractFmd());

                if (enrollmentResult.ResultCode == Constants.ResultCode.DP_SUCCESS) {
                    this.Invoke(u, new Object[] { null, "The Fingerprint Data Has Been Captured." });
                    fmd = enrollmentResult.Data;

                    EnableSaveBtnDelegate e = new EnableSaveBtnDelegate(EnableSaveBtn);
                    this.Invoke(e, new Object[] { true });

                    break;
                }
            }
        }

        private delegate void EnableSaveBtnDelegate(Boolean state);
        private void EnableSaveBtn(Boolean state) {
            this.saveBtn.Enabled = state;
        }

        private IEnumerable<Fmd> CaptureAndExtractFmd() {
            while (true) {
                DataResult<Fmd> conversionResult;

                Fid fid = null;
                if (!mainWindow.CaptureFinger(ref fid)) break;
                if (fid == null) continue;

                trialCount++;
                conversionResult = FeatureExtraction.CreateFmdFromFid(fid, Constants.Formats.Fmd.ANSI);

                Bitmap bmp = mainWindow.CreateBitmap(fid.Views.FirstOrDefault().RawImage, fid.Views.FirstOrDefault().Width, fid.Views.FirstOrDefault().Height);
                UpdateCommandTextAndPictureBoxDelegate u1 = new UpdateCommandTextAndPictureBoxDelegate(UpdateCommandTextAndPictureBox);
                this.Invoke(u1, new Object[] { bmp, "Please Tap Again." });

                if (conversionResult.ResultCode != Constants.ResultCode.DP_SUCCESS) break;

                if (trialCount >= 8) {
                    bmp = mainWindow.CreateBitmap(fid.Views.FirstOrDefault().RawImage, fid.Views.FirstOrDefault().Width, fid.Views.FirstOrDefault().Height);
                    UpdateCommandTextAndPictureBoxDelegate u2 = new UpdateCommandTextAndPictureBoxDelegate(UpdateCommandTextAndPictureBox);
                    this.Invoke(u2, new Object[] { bmp, "Fingerprint Is Inconsistent. Please Try Again." });

                    trialCount = 0;
                    break;
                }

                yield return conversionResult.Data;
            }
        }

        private void EnrollmentForm_Activated(object sender, EventArgs e) {
            this.nameTextBox.Focus();
            RefreshUI();
        }

        private delegate void UpdateCommandTextAndPictureBoxDelegate(Bitmap bmp, String text);
        private void UpdateCommandTextAndPictureBox(Bitmap bmp, String text) {
            this.pictureBox.Image = bmp;
            this.pictureBox.Refresh();

            this.commandLabel.Text = text;
        }

        private void nameTextBox_Leave(object sender, EventArgs e) {
            this.RefreshUI();
        }

        private void roleComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            this.RefreshUI();
        }

        private void saveBtn_Click(object sender, EventArgs e) {
            try {
                MySqlConnection mySQLConnection = new MySqlConnection("SERVER=localhost;DATABASE=se;UID=root;PASSWORD=;");
                mySQLConnection.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = mySQLConnection;
                cmd.CommandText = "INSERT INTO employee(name, role, fmd) VALUES(@name, @role, @fmd)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@name", this.nameTextBox.Text);
                cmd.Parameters.AddWithValue("@role", this.roleComboBox.SelectedItem);
                cmd.Parameters.AddWithValue("@fmd", Fmd.SerializeXml(this.fmd));

                cmd.ExecuteNonQuery();

                mySQLConnection.Close();

                this.commandLabel.Text = "Data Saved.";
                this.saveBtn.Enabled = false;
            } catch (MySqlException) {
                MessageBox.Show(this, "Error Saving Data To Database!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}
