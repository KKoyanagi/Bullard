using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace API.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int User_Id { get; set; }
        [ForeignKey("Employee")]
        public int Emp_Id { get; set; }
        [StringLength(50)]
        public string Username { get; set; }
        [StringLength(256)]
        private string HashedPass { get; set; }
        [StringLength(128)]
        public string Salt { get; set; }
    }
}
