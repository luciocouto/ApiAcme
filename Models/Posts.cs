using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiAcme.Models
{
    public partial class Posts
    {
        public Posts()
        {
            Rates = new HashSet<Rates>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Descriptionpost { get; set; }
        [Required]
        public string Contentspost { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Datepost { get; set; }

        [Required]
        public virtual Authors Author { get; set; }

        [Required]
        public virtual ICollection<Rates> Rates { get; set; }
    }
}
