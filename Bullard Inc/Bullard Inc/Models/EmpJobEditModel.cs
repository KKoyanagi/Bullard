using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bullard_Inc.Models
{
    public class EmpJobEditModel
    {
        public string day { get; set; }
        public string jobNumber { get; set; }
        public string status { get; set; }
        public double hours { get; set; }
        public int miles { get; set; }
        public double lunch { get; set; }
        public string workPreferred { get; set; }

    }
}