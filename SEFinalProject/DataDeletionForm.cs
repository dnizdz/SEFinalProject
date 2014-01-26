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

        public DataDeletionForm(MainWindow mainWindow) {
            InitializeComponent();
            this.Activated += new System.EventHandler(this.DataDeletionForm_Activated);
            this.mainWindow = mainWindow;
        }

        private void DataDeletionForm_Activated(object sender, EventArgs e) {
            
        }
    }
}
