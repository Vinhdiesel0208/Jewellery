using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib
{
    [Table("ContactDetails")]
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [StringLength(250)]
        [Required(ErrorMessage = "Name is required")]
        public string Name { set; get; }

        [StringLength(50)]
        [DisplayName("Phone")]
        public string Phone { set; get; }

        [StringLength(250)]
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { set; get; }

        [StringLength(250)]
        [DisplayName("Address")]
        public string Address { set; get; }

        [StringLength(250)]
        [DisplayName("Message")]
        public string Message { set; get; }

        public DateTime DateSent { set; get; } = DateTime.Now;
    }
}
