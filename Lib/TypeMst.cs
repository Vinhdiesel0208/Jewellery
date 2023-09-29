using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    [Table("tbType")]
    public class TypeMst
    {
        [Key]
        [Column(TypeName = "nchar(10)")]

        public string? Type_Id { get; set; }
        [Required]
        public string? Type_Name { get; set; }
    }
}
