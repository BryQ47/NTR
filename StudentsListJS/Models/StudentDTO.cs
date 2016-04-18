using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsListJS.Models
{
    public class StudentDTO
    {
        public int IDStudent { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IndexNo { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public int IDGroup { get; set; }
        public string GroupName { get; set; }
        public byte[] Stamp { get; set; }

        //public StudentDTO(Student s)
        //{
        //    IDStudent = s.IDStudent;
        //    FirstName = s.FirstName;
        //    LastName = s.LastName;
        //    IndexNo = s.IndexNo;
        //    BirthDate = s.BirthDate;
        //    BirthPlace = s.BirthPlace;
        //    IDGroup = s.IDGroup;
        //    Stamp = s.Stamp;
        //}
    }
}