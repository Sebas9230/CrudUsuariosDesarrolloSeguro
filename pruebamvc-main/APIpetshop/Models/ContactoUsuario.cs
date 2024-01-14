using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIpetshop.Models
{
    public class ContactoUsuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idContacto { get; set; }

        [Required]
        public string codUnico { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }

        [Required]
        public string cedulaUsuario { get; set; }
    }
}
