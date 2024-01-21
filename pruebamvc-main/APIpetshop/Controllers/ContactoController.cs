using APIpetshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIpetshop.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContactoController : ControllerBase
    {

        private readonly ApplicationDBContext _db;
        protected ResultadoApi _resultadoApi;

        public ContactoController(ApplicationDBContext db)
        {
            _db = db;
            _resultadoApi = new();
        }


        // GET: api/<ContactoController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            //return Ok(Utils.Util.productos);
            var contactos = await _db.contactos.ToListAsync();


            if (contactos != null)
            {
                _resultadoApi.listaContactos = contactos;
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
        }

        // GET api/<ContactoController>/5
        [HttpGet("{cedula}")]
        public async Task<IActionResult> Get(string cedula)
        {
            //Producto producto = Utils.Util.productos.Find(x => x.codigo.Equals(id));
            Contacto contacto = await _db.contactos.FirstOrDefaultAsync(x => x.cedula.Equals(cedula));
            if (contacto != null)
            {
                _resultadoApi.contacto = contacto;
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
        public async Task<IActionResult> Post([FromBody] Contacto contacto)
        {
            //Producto producto1 = Utils.Util.productos.Find(x => x.codigo.Equals(producto.codigo));
            Contacto contacto1 = await _db.contactos.FirstOrDefaultAsync(x => x.cedula.Equals(contacto.cedula));
            if (contacto1 == null)
            {
                await _db.contactos.AddAsync(contacto);
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
        [HttpPut("{cedula}")]
        public async Task<IActionResult> Put(string cedula, [FromBody] Contacto contacto)
        {
            //Producto producto1 = Utils.Util.productos.Find(x => x.codigo.Equals(id));
            Contacto contacto1 = await _db.contactos.FirstOrDefaultAsync(x => x.cedula.Equals(cedula));

            if (contacto1 != null)
            {
                contacto1.nombre = contacto.nombre != null ? contacto.nombre : contacto1.nombre;
                contacto1.direccion = contacto.direccion != null ? contacto.direccion : contacto1.direccion;
                contacto1.telefono = contacto.telefono != null ? contacto.telefono : contacto1.telefono;
                // Nuevos atributos
                contacto1.imagen = contacto.imagen != null ? contacto.imagen : contacto1.imagen;
                contacto1.userName = contacto.userName != null ? contacto.userName : contacto1.userName;
                contacto1.password = contacto.password != null ? contacto.password : contacto1.password;
                _db.Update(contacto1);
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
        [HttpDelete("{cedula}")]
        public async Task<IActionResult> Delete(string cedula)
        {
            //Producto producto1 = Utils.Util.productos.Find(x => x.codigo.Equals(id));
            Contacto contacto1 = await _db.contactos.FirstOrDefaultAsync(x => x.cedula.Equals(cedula));
            if (contacto1 != null)
            {
                _db.contactos.Remove(contacto1);
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
