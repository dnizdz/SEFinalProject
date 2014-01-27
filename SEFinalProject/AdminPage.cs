using DPCtlUruNet;
using DPUruNet;
using MySql.Data.MySqlClient;
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
    public partial class AdminPage : Form {
        private MainWindow mainWindow;
        private DateTime date;
        private DeviceInfo deviceInfo;
        private DataDeletionForm dataDeletionForm;
        private EnrollmentForm enrollmentForm;

        public AdminPage(MainWindow mainWindow) {
            InitializeComponent();

            this.Activated += new System.EventHandler(this.AdminPage_Activated);
            this.mainWindow = mainWindow;
            this.date = this.dateTimePicker.Value;
            this.deviceInfo = null;
            this.dataDeletionForm = null;
            this.enrollmentForm = null;
        }

        private void refreshBtn_Click(object sender, EventArgs e) {
            this.date = dateTimePicker.Value;
            this.dataGridView.Rows.Clear();

            try {
                MySqlConnection mySQLConnection = new MySqlConnection("SERVER=localhost;DATABASE=se;UID=root;PASSWORD=;");
                mySQLConnection.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = mySQLConnection;
                cmd.CommandText = "SELECT * FROM employee NATURAL JOIN attendance WHERE clockin BETWEEN \'" + date.ToString("yyyy-MM-dd") + " 00:00:00\' AND \'" + date.ToString("yyyy-MM-dd") + " 23:59:59\' ORDER BY empID ASC";

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    Int32 empID = reader.GetInt32("empID");
                    String name = reader.GetString("name");
                    String clockin = String.Empty;
                    String clockout = String.Empty;

                    if (!reader.IsDBNull(reader.GetOrdinal("clockin"))) {
                        clockin = DateTime.Parse(reader.GetString("clockin")).ToString("h:mm:ss tt");
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("clockout"))) {
                        clockout = DateTime.Parse(reader.GetString("clockout")).ToString("h:mm:ss tt");
                    }

                    dataGridView.Rows.Add(empID, name, clockin, clockout);
                }

                reader.Close();
                mySQLConnection.Close();
            } catch (MySqlException) {
                MessageBox.Show(this, "Error Retrieving Data From Database!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mainWindow.Close();
            }
        }

        private void AdminPage_Activated(object sender, EventArgs e) {
            this.groupBox1.Focus();
            this.refreshBtn.PerformClick();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) {
            this.refreshBtn.PerformClick();
        }

        private void deviceInfoBtn_Click(object sender, EventArgs e) {
            if (deviceInfo == null || deviceInfo.IsDisposed) {
                deviceInfo = new DeviceInfo(mainWindow);
            }

            deviceInfo.Show();
        }

        private void enrollBtn_Click(object sender, EventArgs e) {
            if (enrollmentForm == null || enrollmentForm.IsDisposed) {
                enrollmentForm = new EnrollmentForm(mainWindow);
            }

            enrollmentForm.Show();
        }

        private void deleteBtn_Click(object sender, EventArgs e) {
            if (dataDeletionForm == null || dataDeletionForm.IsDisposed) {
                dataDeletionForm = new DataDeletionForm(mainWindow);
            }

            dataDeletionForm.Show();
        }

        private void AdminPage_FormClosing(object sender, FormClosingEventArgs e) {
            mainWindow.isAdminPageOpen = false;
        }
    }
}
