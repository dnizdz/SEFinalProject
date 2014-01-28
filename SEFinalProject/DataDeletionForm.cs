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
    public partial class DataDeletionForm : Form {
        private MainWindow mainWindow;
        private Boolean isFound;

        public DataDeletionForm(MainWindow mainWindow) {
            InitializeComponent();
            this.Activated += new System.EventHandler(this.DataDeletionForm_Activated);
            this.mainWindow = mainWindow;
            this.isFound = false;
            this.nameLabel.Text = String.Empty;
        }

        private void DataDeletionForm_Activated(object sender, EventArgs e) {
            this.idTextBox.Focus();
        }

        private void idTextBox_TextChanged(object sender, EventArgs e) {
            this.isFound = false;
            this.nameLabel.Text = String.Empty;

            try {
                MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=se;UID=root;PASSWORD=;");
                con.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM employee WHERE empID = \'" + this.idTextBox.Text + "\'";

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    this.nameLabel.Text = reader.GetString("name");
                }

                reader.Close();
                con.Close();

                this.isFound = true;
            } catch (MySqlException) {
                MessageBox.Show(this, "Error Retrieving Data From Database!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mainWindow.Close();
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e) {
            if (isFound) {
                try {
                    MySqlConnection con = new MySqlConnection("SERVER=localhost;DATABASE=se;UID=root;PASSWORD=;");
                    con.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "DELETE FROM employee WHERE empID = \'" + this.idTextBox.Text + "\'";

                    cmd.ExecuteNonQuery();

                    con.Close();

                    this.idTextBox.Text = String.Empty;
                    this.nameLabel.Text = "Data Successfully Deleted.";
                    this.deleteBtn.Enabled = false;
                } catch (MySqlException) {
                    MessageBox.Show(this, "Error Deleting Data From Database!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mainWindow.Close();
                }
            }
        }
    }
}
