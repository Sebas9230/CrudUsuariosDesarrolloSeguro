using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIpetshop.Models
{
    public class ClienteCompra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idCompra { get; set; }

        [Required]
        public string codigoCompra { get; set; }

        [Required]
        public string nombreCliente { get; set; }

        [Required]
        public string valorCompra { get; set; }

        [Required]
        public string cedula { get; set; }
    }
}
