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

        public AdminPage(MainWindow mainWindow) {
            InitializeComponent();
            this.Activated += new System.EventHandler(this.AdminPage_Activated);
            this.mainWindow = mainWindow;
        }

        private void AdminPage_Activated(object sender, EventArgs e) {
            this.groupBox1.Focus();
        }
    }
}
