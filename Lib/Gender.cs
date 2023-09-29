    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Lib
    {
        [Table("Gender")]
        public class Gender
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            [Column(TypeName = "nchar(10)")]

            public string Gender_Id { get; set; }
            [Required]
            public string Gender_Name { get; set; }
        }
    }
