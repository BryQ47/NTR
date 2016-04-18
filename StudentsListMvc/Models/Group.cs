namespace StudentsListMvc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Group
    {
        public Group()
        {
            Students = new HashSet<Student>();
        }

        [Key]
        public int IDGroup { get; set; }

        [Required]
        [StringLength(16)]
        [DisplayName("Nazwa")]
        public string Name { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] Stamp { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
