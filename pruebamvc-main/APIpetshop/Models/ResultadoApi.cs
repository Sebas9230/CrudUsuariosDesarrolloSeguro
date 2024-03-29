﻿using System;
namespace APIpetshop.Models
{
	public class ResultadoApi
	{
		
            public string httpResponseCode { get; set; }

			public List<Producto> listaProductos { get; set; }

            public Producto producto { get; set; }

            public List<Contacto> listaContactos{ get; set; }

            public Contacto contacto{ get; set; }

            public List<ContactoUsuario> listaContactosUsuario{ get; set; }

            public ContactoUsuario contactoUsuario{ get; set; }

            public List<ClienteCompra> listaClienteCompras { get; set; }

            public ClienteCompra clienteCompra { get; set; }

        public string texto { get; set; }
    }
}

