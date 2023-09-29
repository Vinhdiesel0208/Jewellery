using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib
{
    [Table("CatMst")]

    public class CatMst
    {
        [Key]
        [Column(TypeName = "nchar(10)")]
        public string Cat_ID { get; set; } // Khóa chính của bảng CatMst

        [Required]
        [MaxLength(50)]
        public string Cat_Name { get; set; } // Name Of Category

       
    }
}
