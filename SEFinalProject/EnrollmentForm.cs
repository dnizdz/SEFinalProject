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
    public partial class EnrollmentForm : Form {
        private MainWindow mainWindow;

        public EnrollmentForm(MainWindow mainWindow) {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void EnrollmentForm_Activated(object sender, EventArgs e) {
            
        }
    }
}
