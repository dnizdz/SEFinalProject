using DPUruNet;
using System;
//ini johan yang comment
namespace SEFinalProject {
    class EmployeeData {
        public EmployeeData() {
            this.empID = 0;
            this.name = String.Empty;
            this.role = String.Empty;
            this.fmd = null;
            this.clockIn = null;
            this.clockOut = null;
        }

        public Int64 empID { get; set; }
        public String name { get; set; }
        public String role { get; set; }
        public Fmd fmd { get; set; }
        public DateTime? clockIn { get; set; }
        public DateTime? clockOut { get; set; }
    }
}
