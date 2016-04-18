using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StudentsList.Model
{
    class Student
    {
        public int StudentId { get; set; }
        [MaxLength(32)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        public string IndexNo { get; set; }
        [Required]
        public virtual Group Group { get; set; }
    }
}
