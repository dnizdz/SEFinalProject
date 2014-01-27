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
        private EnrollmentControl enrollmentControl;

        public EnrollmentForm(MainWindow mainWindow) {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.enrollmentControl = null;
        }

        private void EnrollmentForm_Activated(object sender, EventArgs e) {
            if (enrollmentControl == null) {
                enrollmentControl = new EnrollmentControl(mainWindow.fingerprintReader, Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);
                enrollmentControl.BackColor = SystemColors.Window;
                enrollmentControl.Location = new Point(0, 0);
                enrollmentControl.Name = "enrollmentControl";
                enrollmentControl.Size = this.Size;
                enrollmentControl.TabIndex = 0;
                enrollmentControl.OnCancel += new EnrollmentControl.CancelEnrollment(this.enrollmentControl_OnCancel);
                enrollmentControl.OnDelete += new EnrollmentControl.DeleteEnrollment(this.enrollmentControl_OnDelete);
                enrollmentControl.OnCaptured += new EnrollmentControl.FingerprintCaptured(this.enrollmentControl_OnCaptured);
                enrollmentControl.OnEnroll += new EnrollmentControl.FinishEnrollment(this.enrollmentControl_OnEnroll);
                enrollmentControl.OnStartEnroll += new EnrollmentControl.StartEnrollment(this.enrollmentControl_OnStartEnroll);
            } else {
                enrollmentControl.Reader = mainWindow.fingerprintReader;
            }

            this.Controls.Add(enrollmentControl);
        }

        void enrollmentControl_OnStartEnroll(EnrollmentControl enrollmentControl, Constants.ResultCode result, int fingerPosition) {
            throw new NotImplementedException();
        }

        void enrollmentControl_OnEnroll(EnrollmentControl enrollmentControl, DataResult<Fmd> enrollmentResult, int fingerPosition) {
            throw new NotImplementedException();
        }

        void enrollmentControl_OnCaptured(EnrollmentControl enrollmentControl, CaptureResult captureResult, int fingerPosition) {
            throw new NotImplementedException();
        }

        void enrollmentControl_OnDelete(EnrollmentControl enrollmentControl, Constants.ResultCode result, int fingerPosition) {
            throw new NotImplementedException();
        }

        void enrollmentControl_OnCancel(EnrollmentControl enrollmentControl, Constants.ResultCode result, int fingerPosition) {
            throw new NotImplementedException();
        }
    }
}
