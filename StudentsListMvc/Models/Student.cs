namespace StudentsListMvc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Student
    {
        [Key]
        public int IDStudent { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Imie")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Nazwisko")]
        public string LastName { get; set; }

        [Required]
        [StringLength(10)]
        [DisplayName("Indeks")]
        public string IndexNo { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Data urodzenia")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }

        [StringLength(32)]
        [DisplayName("Miejsce urodzenia")]
        public string BirthPlace { get; set; }

        public int IDGroup { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] Stamp { get; set; }

        [ForeignKey("IDGroup")]
        public virtual Group Group { get; set; }
    }
}
