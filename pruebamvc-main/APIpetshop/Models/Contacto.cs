using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIpetshop.Models
{
    public class Contacto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idContacto { get; set; }
        public string imagen { get; set; }

        [Required]
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }

        [Required]
        public string cedula { get; set; }

        //Probando
        public string userName { get; set; }

        public string password { get; set; }
    }
}
