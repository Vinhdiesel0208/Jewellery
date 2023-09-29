using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Blogs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? Summary { get; set; }
        public string? Content1 { get; set; }
        public string? Content2 { get; set; }
        public string? Content3 { get; set; }
        public string? Author { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime UpdateDay { get; set; }
        public string? Photo { get; set; }
    }
}
