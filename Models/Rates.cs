using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiAcme.Models
{
    public partial class Rates
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int PostId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Daterate { get; set; }
        [Required]
        public string Noterate { get; set; }

        public virtual Authors Author { get; set; }
        public virtual Posts Post { get; set; }
    }
}
