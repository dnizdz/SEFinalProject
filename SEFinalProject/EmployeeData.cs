﻿using DPUruNet;
using System;

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

        public Int32 empID { get; set; }
        public String name { get; set; }
        public String role { get; set; }
        public Fmd fmd { get; set; }
        public DateTime? clockIn { get; set; }
        public DateTime? clockOut { get; set; }
    }
}
