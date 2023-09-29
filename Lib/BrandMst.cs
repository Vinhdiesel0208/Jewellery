using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib
{
    [Table("BrandMst")]
    public class BrandMst
    {
        [Key]
        [Column(TypeName = "nchar(10)")]

        public string Brand_ID { get; set; } // Khóa chính của bảng BrandMst

        [Required]
        [MaxLength(50)]
        public string Brand_Type { get; set; } // Type Of Brand (Asmi, D’damas, etc…)

        // Một danh sách các sản phẩm có quan hệ với BrandMst nếu có
    }
}
