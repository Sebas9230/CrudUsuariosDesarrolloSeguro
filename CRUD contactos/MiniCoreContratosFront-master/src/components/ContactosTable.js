import React from 'react';
import "../components/Css/CrudUsuario.css"; // Asegúrate de tener la ruta correcta

const ContactosTable = ({ contactos }) => {
  return (
    <div className="table-container">
      <div>
        <h1>Contactos</h1>
      </div>
      <table className="crud-usuario-table">
        <thead>
          <tr>
            <th>Nombre de contacto</th>
            <th>Teléfono de contacto</th>
          </tr>
        </thead>
        <tbody>
          {contactos.map((contacto, index) => (
            <tr key={index}>
              <td>{contacto.nombre}</td>
              <td>{contacto.telefono}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ContactosTable;
