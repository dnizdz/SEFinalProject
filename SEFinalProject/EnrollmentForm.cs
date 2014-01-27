using DPCtlUruNet;
using DPUruNet;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
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
    public partial class EnrollmentForm : Form {
        private MainWindow mainWindow;

        public EnrollmentForm(MainWindow mainWindow) {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void RefreshUI() {
            if (this.nameTextBox.Text.Length != 0 && this.roleComboBox.SelectedText.Length != 0) {

            } else {
                this.commandLabel.Text = String.Empty;
                this.pictureBox.Image = null;
            }
        }

        private void EnrollmentForm_Activated(object sender, EventArgs e) {
            this.nameTextBox.Focus();
            this.saveBtn.Enabled = false;
            this.commandLabel.Text = String.Empty;
        }

        private delegate void UpdateCommandTextDelegate(String text);
        private void UpdateCommandText(String text) {
            this.commandLabel.Text = text;
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e) {
            this.RefreshUI();
        }

        private void roleComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            this.RefreshUI();
        }

        private void saveBtn_Click(object sender, EventArgs e) {
            try {
                MySqlConnection mySQLConnection = new MySqlConnection("SERVER=localhost;DATABASE=se;UID=root;PASSWORD=;");
                mySQLConnection.Open();


            } catch (MySqlException) {
                MessageBox.Show(this, "Error Saving Data To Database!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}
