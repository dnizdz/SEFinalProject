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
        public AdminPage() {
            InitializeComponent();
            this.Activated += AdminPage_Activated;
        }

        void AdminPage_Activated(object sender, EventArgs e) {
            this.groupBox1.Focus();
        }
    }
}
