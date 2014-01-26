using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEFinalProject {
    class EmployeeData {
        public EmployeeData() {
            this.id = 0;
            this.name = String.Empty;
            this.role = String.Empty;
            this.clockIn = null;
            this.clockOut = null;
        }

        public Int64 id { get; set; }
        public String name { get; set; }
        public String role { get; set; }
        public DateTime? clockIn { get; set; }
        public DateTime? clockOut { get; set; }
    }
}
