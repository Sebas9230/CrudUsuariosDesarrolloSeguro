// Inicio.js
import React from 'react';
import { Link } from 'react-router-dom'; // Asegúrate de tener instalado react-router-dom
import "../components/Css/Inicio.css";

const Inicio = () => {
  return (
    <div className="inicio-body">
      <header>
        <h1>Bienvenido a la Aplicación de Administración de Usuarios</h1>
        <p>Una plataforma fácil de usar para gestionar usuarios de manera eficiente.</p>
      </header>

      <main>
        <Link to="/crud">
          <div className="card">
            Ir a CRUD
          </div>
        </Link>

        <div className="description">
          <p>Descubre las funciones avanzadas de administración de usuarios.</p>
        </div>

        <div className="form-container">
          {/* Agrega un formulario de registro o inicio de sesión si es necesario */}
          {/* Ejemplo de formulario de registro */}
          {/* <form>
            <h2>Regístrate</h2>
            {/* Agrega campos de formulario según tus necesidades */}
            {/* <input type="text" placeholder="Nombre" />
            <input type="email" placeholder="Correo electrónico" />
            <input type="password" placeholder="Contraseña" />
            <button type="submit">Registrarse</button>
          </form> */}
        </div>
      </main>

      <footer>
        <p>¿Ya tienes una cuenta? <Link to="/login">Inicia sesión aquí</Link>.</p>
      </footer>
    </div>
  );
};

export default Inicio;