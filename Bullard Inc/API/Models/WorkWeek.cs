using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class WorkWeek
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Week_Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0}:MM/dd/yyyy", ApplyFormatInEditMode =true)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0}:MM/dd/yyyy", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

    }
}
