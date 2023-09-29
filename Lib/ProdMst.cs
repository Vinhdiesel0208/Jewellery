    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Reflection;

    namespace Lib
    {
        [Table("ProdMst")]
        public class ProdMst
        {
            [Key]
            [Column(TypeName = "nchar(10)")]
            public string Prod_ID { get; set; } // Khóa chính của bảng ProdMst

            [Required]
            [MaxLength(50)]
            public string Prod_Name { get; set; }

            [Required]
            [Column(TypeName = "nchar(10)")]
            public string Type_Id { get; set; }

            [Required]
            [Column(TypeName = "nchar(10)")]
            public string Cat_ID { get; set; }

            [Required]
            [Column(TypeName = "nchar(10)")]
            public string Brand_ID { get; set; }

            [Required]
            public decimal Price { get; set; }
          public int Image_id { get; set; }

        

        [Required]
            public string Detail { get; set; }
            [Required]
            public int Weight { get; set; }
            [Required]
            public int Quantity { get; set; }

            // Thêm trường Gender_Id để liên kết với bảng GenderMst
            [Required]
            [Column(TypeName = "nchar(10)")]
            public string Gender_Id { get; set; }

            

            public virtual CatMst? CatMst { get; set; }
            public virtual BrandMst? BrandMst { get; set; }
            public virtual TypeMst? TypeMst { get; set; }   
            public virtual Gender? Gender { get; set; }
            public virtual ProductImage? ProductImage { get; set; }
    }
    }
