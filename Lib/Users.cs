using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib
{

    [Table("User")]
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        //[Required]
        //public string Username { get; set; }

        [Required]
        [RegularExpression(@"^\b[A-Za-z0-9._%+-]+@gmail\.com\b$", ErrorMessage = "Email wrong")]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Pass wrong")]
        public string Password { get; set; }

        
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$", ErrorMessage = "Phone wrong")]        
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        public bool Role { get; set; }
        [Required]
        public string FullName { get; set; }

    }

}

