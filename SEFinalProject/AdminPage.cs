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
        private DateTime? date;
        private DeviceInfo deviceInfo;
        private DataDeletionForm dataDeletionForm;

        public AdminPage(MainWindow mainWindow) {
            InitializeComponent();
            this.Activated += new System.EventHandler(this.AdminPage_Activated);
            this.mainWindow = mainWindow;
            this.deviceInfo = null;
            this.dataDeletionForm = null;
        }

        private void AdminPage_Activated(object sender, EventArgs e) {
            this.groupBox1.Focus();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) {
            this.date = dateTimePicker1.Value;
        }

        private void deviceInfoBtn_Click(object sender, EventArgs e) {
            if (deviceInfo == null || deviceInfo.IsDisposed) {
                deviceInfo = new DeviceInfo(mainWindow);
            }

            deviceInfo.Show();
        }

        private void enrollBtn_Click(object sender, EventArgs e) {

        }

        private void deleteBtn_Click(object sender, EventArgs e) {
            if (dataDeletionForm == null || dataDeletionForm.IsDisposed) {
                dataDeletionForm = new DataDeletionForm(mainWindow);
            }

            dataDeletionForm.Show();
        }
    }
}
