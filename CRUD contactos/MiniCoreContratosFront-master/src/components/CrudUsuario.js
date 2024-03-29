// CrudUsuario.js
import React, { useEffect, useState } from "react";
import { addContacto, getContactoslist } from "../Services/connectionAPI";
import { deleteContacto } from "../Services/connectionAPI";
import { addContactoUsuario, getContactoslistUsuario } from "../Services/connectionAPI";
import { deleteContactoUsuario } from "../Services/connectionAPI";
import ContactosTable from '../components/ContactosTable';
import "../components/Css/CrudUsuario.css";
import { Link, useNavigate, useLocation } from "react-router-dom"; // Asegúrate de tener instalado react-router-dom
import ReactDOM from 'react-dom';

//import { render } from "@testing-library/react";

const CrudUsuario = () => {
  const navigate = useNavigate();
  const [contactos, setContactos] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [showForm2, setShowForm2] = useState(false);
  const [contactosUsuario, setContactosUsuario] = useState([]);

  // Agrega esta función dentro del componente CrudUsuario
  const abrirVentanaContactos = (cedulaUsuario) => {
    // Obtén las dimensiones de la pantalla
    const screenWidth = window.screen.width;
    const screenHeight = window.screen.height;

    // Calcula las coordenadas para centrar la ventana
    const left = (screenWidth - 300) / 2;  // 300 es el ancho de la ventana
    const top = (screenHeight - 600) / 2;  // 600 es la altura de la ventana

    // Abre la nueva ventana en la posición calculada
    const ventana = window.open("", "_blank", `width=300,height=600,left=${left},top=${top}`);

    // Renderiza el componente ContactosTable en la nueva ventana sin filtros
    // ReactDOM.render(<ContactosTable contactos={contactos} />, ventana.document.body);

    // Filtra los contactos por la cédula del usuario antes de renderizar
    const contactosFiltrados = contactosUsuario.filter(contacto => contacto.cedulaUsuario === cedulaUsuario);

    // Renderiza el componente ContactosTable en la nueva ventana con los contactos filtrados
    ReactDOM.render(<ContactosTable contactos={contactosFiltrados} />, ventana.document.body);
  };

  // POST *****************************************************************************
  const [newContacto, setNewContacto] = useState({
    imagen: "",
    nombre: "",
    direccion: "",
    telefono: "",
    cedula: "",
    userName: "",
    password: "",
  });

  const [newContactoUsuario, setNewContactoUsuario] = useState({
    codUnico: "",
    cedulaUsuario: "",
    nombre: "",
    telefono: "",
  });

  // Function to handle clicking on the "Agregar Usuario" button
  const handleShowForm = () => {
    setShowForm(true);
  };

  // Updated handleShowForm2 function to toggle showForm2
  const handleShowForm2 = (cedula) => {
    // Configura el código único automáticamente antes de mostrar el formulario
    // const codUnico = `${cedula}${nombre}${telefono}`;
    const cedulaUsuario = `${cedula}`;
    setNewContactoUsuario((prevContactoUsuario) => ({
      ...prevContactoUsuario,
      codUnico: "",
      cedulaUsuario: cedulaUsuario,
      nombre: "",
      telefono: "",
    }));
    setShowForm2(true);
  };

  const handleAddContacto = async (event) => {
    event.preventDefault();
    try {
      const response = await addContacto(newContacto); // Use the response from the API call
      // Si la agregación fue exitosa, actualizamos la lista de contactos para reflejar los cambios
      const updatedContactos = [...contactos, response]; // Add the response data to the contact list
      setContactos(updatedContactos);
      setNewContacto({
        imagen: "",
        nombre: "",
        direccion: "",
        telefono: "",
        cedula: "",
        userName: "",
        password: "",
      });
      window.location.reload();
      console.log("API Response:", response);
      console.log("Updated Contactos :", updatedContactos);
      console.log("New Contacto :", newContacto);

    } catch (error) {
      // Manejar el error, si es necesario
      console.error("Error al agregar el usuario:", error);
    }
  };

  const handleAddContactoUsuario = async (event) => {
    event.preventDefault();
    try {
      // Configura automáticamente el código único antes de agregar el contacto
      // const codUnico = `${newContactoUsuario.cedula}${newContactoUsuario.nombreUsuario}${newContactoUsuario.telefonoUsuario}`;
      //const codUnico = `${newContactoUsuario.cedula}`;

      // Concatena los valores para formar codUnico
      const codUnico = `${newContactoUsuario.cedulaUsuario}${newContactoUsuario.nombre}${newContactoUsuario.telefono}`;
      // Actualiza codUnico en el estado antes de enviarlo
      const contactoUsuarioConCodUnico = {
        ...newContactoUsuario,
        codUnico,
      };
      //const response = await addContactoUsuario(newContactoUsuario);
      const response = await addContactoUsuario(contactoUsuarioConCodUnico);

      // Si la agregación fue exitosa, actualizamos la lista de contactos para reflejar los cambios
      const updatedContactosUsuario = [...contactosUsuario, response];
      setContactosUsuario(updatedContactosUsuario);

      // Reinicia el estado del formulario
      setNewContactoUsuario({
        codUnico: "",
        cedulaUsuario: "",
        nombre: "",
        telefono: "",
      });

      window.location.reload();
      console.log("API Response:", response);
      console.log("Updated Contactos Usuario:", updatedContactosUsuario);
      console.log("New Contacto Usuario:", newContactoUsuario);


    } catch (error) {
      console.error("Error al agregar el contacto:", error);
    }
  };


  const handleChange = (event) => {
    const { name, value } = event.target;
    setNewContacto((prevContacto) => ({
      ...prevContacto,
      [name]: value,
    }));
  };

  const handleForm2Change = (event) => {
    const { name, value } = event.target;
    setNewContactoUsuario((prevContactoUsuario) => ({
      ...prevContactoUsuario,
      [name]: value,
    }));
  };

  // Función para manejar el clic en el botón de eliminar ******************************************
  const handleDeleteContacto = async (cedula) => {
    try {
      await deleteContacto(cedula);
      // Si la eliminación fue exitosa, actualizamos la lista de contactos para reflejar los cambios
      setContactos((prevContactos) =>
        prevContactos.filter((contacto) => contacto.cedula !== cedula)
      );
    } catch (error) {
      // Manejar el error, si es necesario
      console.error("Error al eliminar el contacto:", error);
    }
  };

  const logOut = async () => {
    // Realiza una solicitud para revocar el token de acceso
    try {
      // URL de cierre de sesión de Google
      const logoutUrl = "https://accounts.google.com/logout";

      // Abre una nueva ventana
      const popupWindow = window.open(
        logoutUrl,
        "_blank",
        "width=600,height=400"
      );

      // Cierra la ventana después de 2 segundos (ajusta según sea necesario)
      setTimeout(() => {
        if (popupWindow) {
          popupWindow.close();
          navigate('/');
        }
      }, 2000);
    } catch (error) {
      console.error("Error al cerrar sesión de Google:", error.message);
    }
  };

  useEffect(() => {
    const fetchData = async () => {
      const data = await getContactoslist();
      setContactos(data.listaContactos);
    };
    fetchData();
  }, []);

  // Luego, en useEffect, obtienes los contactos de la API y actualizas el estado
  useEffect(() => {
    const fetchData = async () => {
      const data = await getContactoslistUsuario();
      setContactosUsuario(data.listaContactosUsuario);
    };
    fetchData();
  }, []);

  return (
    <div className="crud-usuario-body">
      <button onClick={logOut}>Close Google Session</button>
      <h1>Tabla de Usuarios</h1>

      {showForm && (
        <form className="contact-form" onSubmit={handleAddContacto}>
          <div className="form-columns">
            <div className="form-column">
              <label>Imagen:</label>
              <input
                type="text"
                name="imagen"
                value={newContacto.imagen}
                onChange={handleChange}
                required
              />
            </div>
            <div className="form-column">
              <label>Nombre:</label>
              <input
                type="text"
                name="nombre"
                value={newContacto.nombre}
                onChange={handleChange}
                required
              />
            </div>
            <div className="form-column">
              <label>Dirección:</label>
              <input
                type="text"
                name="direccion"
                value={newContacto.direccion}
                onChange={handleChange}
                required
              />
            </div>
          </div>
          <div className="form-columns">
            <div className="form-column">
              <label>Teléfono:</label>
              <input
                type="text"
                name="telefono"
                value={newContacto.telefono}
                onChange={handleChange}
                required
              />
            </div>
            <div className="form-column">
              <label>Cédula:</label>
              <input
                type="text"
                name="cedula"
                value={newContacto.cedula}
                onChange={handleChange}
                required
              />
            </div>
            <div className="form-column">
              <label>Usuario:</label>
              <input
                type="text"
                name="userName"
                value={newContacto.userName}
                onChange={handleChange}
                required
              />
            </div>
          </div>
          <div className="form-columns">
            <div className="form-column">
              <label>Password:</label>
              <input
                type="password"
                name="password"
                value={newContacto.password}
                onChange={handleChange}
                required
              />
            </div>
          </div>
          <div className="form-buttons">
            <button type="submit">Agregar Usuario</button>
            <button onClick={() => setShowForm(false)}>Cancelar</button>
          </div>
        </form>
      )}

      {showForm2 && (
        <form className="contact-form" onSubmit={handleAddContactoUsuario}>
          <div className="form-columns">
            <div className="form-column">
              <label>Nombre de Contacto:</label>
              <input
                type="text"
                name="nombre"
                value={newContactoUsuario.nombre}
                onChange={handleForm2Change}
                required
              />
            </div>
            <div className="form-column">
              <label>Telefono de Contacto:</label>
              <input
                type="text"
                name="telefono"
                value={newContactoUsuario.telefono}
                onChange={handleForm2Change}
                required
              />
            </div>
            {/* <div className="form-column">
              <label>Codigo Unico:</label>
              <input
                type="text"
                name="codUnico"
                value={newContactoUsuario.codUnico}
                onChange={handleForm2Change}
                required
                readOnly
              />
            </div> */}
            <div className="form-column">
              <label>Cedula Usuario:</label>
              <input
                type="text"
                name="cedulaUsuario"
                value={newContactoUsuario.cedulaUsuario}
                onChange={handleForm2Change}
                required
                readOnly
              />
            </div>
          </div>
          <div className="form-buttons">
            <button type="submit">Agregar Contacto</button>
            <button onClick={() => setShowForm2(false)}>Cancelar</button>
          </div>
        </form>
      )}


      {!showForm && <button onClick={handleShowForm}>Agregar Usuario</button>}

      <div className="table-container">
        <table>
          <thead>
            <tr>
              <th>ID</th>
              <th>Nombre</th>
              <th>Dirección</th>
              <th>Teléfono</th>
              <th>Cédula</th>
              <th>Usuario</th>
              <th>Password</th>
              <th>Edicion</th>
              {/* Otras columnas si es necesario */}
            </tr>
          </thead>
          <tbody>
            {contactos.map((contacto) => (
              <tr key={contacto.idContacto}>
                <td>{contacto.idContacto}</td>
                <td>{contacto.nombre}</td>
                <td>{contacto.direccion}</td>
                <td>{contacto.telefono}</td>
                <td>{contacto.cedula}</td>
                <td>{contacto.userName}</td>
                <td>{contacto.password}</td>


                <button onClick={() => handleShowForm2(contacto.cedula)}>
                  Agregar Contacto
                </button>

                {/* <button onClick={abrirVentanaContactos}>Mostrar Contactos</button> */}
                <button onClick={() => abrirVentanaContactos(contacto.cedula)}>Mostrar Contactos</button>


                <button onClick={() => handleDeleteContacto(contacto.cedula)}>
                  Eliminar Usuario
                </button>
                {/* Otras celdas si es necesario */}
              </tr>
            ))}
          </tbody>
        </table>

      </div>
    </div>
  );
};

export { CrudUsuario };
