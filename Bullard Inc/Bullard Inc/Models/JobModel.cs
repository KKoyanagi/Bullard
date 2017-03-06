﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bullard_Inc.Models
{
    public class JobModel
    {
        public int EmployeeDay_Id { get; set; }
        public int Job_Id { get; set; }
        public int Project_Id { get; set; }
        public int ActivityCode { get; set; }
        public double Hours { get; set; }
        public int Mileage { get; set; }

        //public string status { get; set; }
        public double Lunch { get; set; }
        //public string workPerformed { get; set; }

    }
}