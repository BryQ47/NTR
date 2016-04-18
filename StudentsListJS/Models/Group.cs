namespace StudentsListJS.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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

        public Group Copy()
        {
            return (Group)this.MemberwiseClone();
        }
    }
}
