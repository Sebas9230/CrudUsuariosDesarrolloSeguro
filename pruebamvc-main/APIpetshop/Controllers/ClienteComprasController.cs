using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIpetshop.Models;
using System.Net;

namespace APIpetshop.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClienteComprasController : ControllerBase
    {

        private readonly ApplicationDBContext _db;
        protected ResultadoApi _resultadoApi;

        public ClienteComprasController(ApplicationDBContext db)
        {
            _db = db;
            _resultadoApi = new();
        }


        // GET: api/<ClienteComprasController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            //return Ok(Utils.Util.productos);
            //var contactos = await _db.contactos.ToListAsync();
            var clienteCompras = await _db.clienteCompras.ToListAsync();

            //if (contactos != null)
            if (clienteCompras != null)
            {
                //_resultadoApi.listaContactos = contactos;
                _resultadoApi.listaClienteCompras = clienteCompras;
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
        }

        // GET api/<ClienteComprasController>/5
        [HttpGet("{codigoCompra}")]
        public async Task<IActionResult> Get(string codigoCompra)
        {
            //Producto producto = Utils.Util.productos.Find(x => x.codigo.Equals(id));
            ClienteCompra clienteCompra = await _db.clienteCompras.FirstOrDefaultAsync(x => x.codigoCompra.Equals(codigoCompra));
            if (clienteCompra != null)
            {
                _resultadoApi.clienteCompra = clienteCompra;
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }

        }

        // POST api/<ClienteComprasController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClienteCompra clienteCompra)
        {
            //Producto producto1 = Utils.Util.productos.Find(x => x.codigo.Equals(producto.codigo));
            ClienteCompra clienteCompra1 = await _db.clienteCompras.FirstOrDefaultAsync(x => x.codigoCompra.Equals(clienteCompra.codigoCompra));
            if (clienteCompra1 == null)
            {
                await _db.clienteCompras.AddAsync(clienteCompra);
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

        // PUT api/<ClienteComprasController>/5
        [HttpPut("{codigoCompra}")]
        public async Task<IActionResult> Put(string codigoCompra, [FromBody] ClienteCompra clienteCompra)
        {
            //Producto producto1 = Utils.Util.productos.Find(x => x.codigo.Equals(id));
            ClienteCompra clienteCompra1 = await _db.clienteCompras.FirstOrDefaultAsync(x => x.codigoCompra.Equals(codigoCompra));

            if (clienteCompra1 != null)
            {
                clienteCompra1.codigoCompra = clienteCompra.codigoCompra != null ? clienteCompra.codigoCompra : clienteCompra1.codigoCompra;
                clienteCompra1.nombreCliente = clienteCompra.nombreCliente != null ? clienteCompra.nombreCliente : clienteCompra1.nombreCliente;
                clienteCompra1.valorCompra = clienteCompra.valorCompra != null ? clienteCompra.valorCompra : clienteCompra1.valorCompra;
                // Nuevos atributos
                // contacto1.imagen = contacto.imagen != null ? contacto.imagen : contacto1.imagen;
                // contacto1.userName = contacto.userName != null ? contacto.userName : contacto1.userName;
                // contacto1.password = contacto.password != null ? contacto.password : contacto1.password;
                _db.Update(clienteCompra1);
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

        // DELETE api/<ClienteComprasController>/5
        [HttpDelete("{codigoCompra}")]
        public async Task<IActionResult> Delete(string codigoCompra)
        {
            //Producto producto1 = Utils.Util.productos.Find(x => x.codigo.Equals(id));
            ClienteCompra clienteCompra1 = await _db.clienteCompras.FirstOrDefaultAsync(x => x.codigoCompra.Equals(codigoCompra));
            if (clienteCompra1 != null)
            {
                _db.clienteCompras.Remove(clienteCompra1);
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

