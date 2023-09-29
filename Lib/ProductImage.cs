using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class ProductImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ImagePath { get; set; }

        // Khóa ngoại để liên kết với sản phẩm
        public string? Prod_ID { get; set; }
        public ProdMst? Product { get; set; }
    }
}
