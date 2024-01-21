using APIpetshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIpetshop.Controllers
{
    

    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContactoUsuarioController : ControllerBase
    {
        private KmsService _kmsService;

        private readonly ApplicationDBContext _db;
        protected ResultadoApi _resultadoApi;

        public ContactoUsuarioController(ApplicationDBContext db)
        {
            _db = db;
            _resultadoApi = new();
        }


        // GET: api/<ContactoController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            //return Ok(Utils.Util.productos);
            var contactosUsuario = await _db.contactosUsuario.ToListAsync();


            if (contactosUsuario != null)
            {
                _resultadoApi.listaContactosUsuario = contactosUsuario;
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
        }

        //GET api/<ContactoController>/5
        [HttpGet("{cedulaUsuario}")]
        public async Task<IActionResult> Get(string cedulaUsuario)
        {
            //Producto producto = Utils.Util.productos.Find(x => x.codigo.Equals(id));
            ContactoUsuario contactoUsuario = await _db.contactosUsuario.FirstOrDefaultAsync(x => x.cedulaUsuario.Equals(cedulaUsuario));
            if (contactoUsuario != null)
            {
                _resultadoApi.contactoUsuario = contactoUsuario;
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }

        }

        [HttpGet("getByName/{nombreUsuario}")]
        public async Task<IActionResult> GetByNombre(string nombreUsuario)
        {
            // Inicializa la instancia de KmsService antes de usarla
            _kmsService = new KmsService();
            
            var decryptedName = _kmsService.DecryptText(nombreUsuario);  

            Contacto contacto = await _db.contactos.FirstOrDefaultAsync(x => x.nombre.Equals(decryptedName));
           
  
            string cedulaUsuario = contacto.cedula;
            //Producto producto = Utils.Util.productos.Find(x => x.codigo.Equals(id));
            List<ContactoUsuario> contactosUsuarios = await _db.contactosUsuario.Where(x => x.cedulaUsuario.Equals(cedulaUsuario)).ToListAsync();
            // Inicializa la instancia de KmsService antes de usarla
            _kmsService = new KmsService();

            string encryptedText = _kmsService.EncryptText(contactosUsuarios.ToString());

            if (contactosUsuarios != null)
            {
                _resultadoApi.texto = encryptedText;
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }

        }


        // POST api/<ContactoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactoUsuario contactoUsuario)
        {
            //Producto producto1 = Utils.Util.productos.Find(x => x.codigo.Equals(producto.codigo));
            ContactoUsuario contactoUsuario1 = await _db.contactosUsuario.FirstOrDefaultAsync(x => x.codUnico.Equals(contactoUsuario.codUnico));
            if (contactoUsuario1 == null)
            {
                await _db.contactosUsuario.AddAsync(contactoUsuario);
                await _db.SaveChangesAsync();
                //Utils.Util.productos.Add(producto);
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
        }

        // PUT api/<ContactoController>/5
        [HttpPut("{cedulaUsuario}")]
        public async Task<IActionResult> Put(string cedulaUsuario, [FromBody] ContactoUsuario contactoUsuario)
        {
            //Producto producto1 = Utils.Util.productos.Find(x => x.codigo.Equals(id));
            ContactoUsuario contactoUsuario1 = await _db.contactosUsuario.FirstOrDefaultAsync(x => x.cedulaUsuario.Equals(cedulaUsuario));

            if (contactoUsuario1 != null)
            {
                contactoUsuario1.nombre = contactoUsuario.nombre != null ? contactoUsuario.nombre : contactoUsuario1.nombre;
                contactoUsuario1.telefono = contactoUsuario.telefono != null ? contactoUsuario.telefono : contactoUsuario1.telefono;
                _db.Update(contactoUsuario1);
                await _db.SaveChangesAsync();
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
        }

        // DELETE api/<ContactoController>/5
        [HttpDelete("{cedulaUsuario}")]
        public async Task<IActionResult> Delete(string cedulaUsuario)
        {
            //Producto producto1 = Utils.Util.productos.Find(x => x.codigo.Equals(id));
            ContactoUsuario contactoUsuario1 = await _db.contactosUsuario.FirstOrDefaultAsync(x => x.cedulaUsuario.Equals(cedulaUsuario));
            if (contactoUsuario1 != null)
            {
                _db.contactosUsuario.Remove(contactoUsuario1);
                await _db.SaveChangesAsync();
                //Utils.Util.productos.Remove(producto1);
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
        }
    }
}
