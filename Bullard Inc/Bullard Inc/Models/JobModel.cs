using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bullard_Inc.Models
{
    public class JobModel
    {
        public int employeeDay_Id { get; set; }
        public string job_ID { get; set; }
        public string project_ID { get; set; }
        public string activityCode { get; set; }
        public double hours { get; set; }
        public int mileage { get; set; }

        public string status { get; set; }
        public double lunch { get; set; }
        public string workPerformed { get; set; }

    }
}