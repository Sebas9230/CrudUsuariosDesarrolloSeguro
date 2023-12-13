using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIpetshop.Models
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idProducto { get; set; }
        [Required]
        public string codeNumber { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string price { get; set; }
        public string quantity { get; set; }
        public string picture { get; set; }

    }

}

