// Inicio.js
import React, { useEffect, useState } from "react";
import { Link, useNavigate, useLocation } from "react-router-dom"; // Asegúrate de tener instalado react-router-dom
import "../components/Css/Inicio.css";
//Para google
import { GoogleOAuthProvider, GoogleLogin } from "@react-oauth/google";
import { jwtDecode } from 'jwt-decode';

const Inicio = () => {
  const navigate = useNavigate();
  const location = useLocation();

  async function logGoogle(authorizationCode) {
    try {
      const user = jwtDecode(authorizationCode.credential);

      console.log("user: ", user);
      navigate('/crud');
    } catch (error) {
      console.error("Inicio de sesión fallido", error);
    }
  }

  useEffect(() => {
    
  }, []);

  return (
    <div className="inicio-body">
      <header>
        <h1>Bienvenido a la Aplicación de Administración de Usuarios</h1>
        <p>
          Una plataforma fácil de usar para gestionar usuarios de manera
          eficiente.
        </p>
      </header>

      <main>
        <Link to="/login">
          <div className="card">Ir a CRUD</div>
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

        <GoogleOAuthProvider clientId="729320449938-0lpd8fran2mbogudp6sfn794snhf634u.apps.googleusercontent.com">
            <GoogleLogin
                onSuccess={credentialResponse => {
                    console.log(credentialResponse);
                    logGoogle(credentialResponse)
                }}
                onError={() => {
                    console.log('Login Failed');
                }}
            />
        </GoogleOAuthProvider>
      </main>

      <footer>
        <p>
          ¿Ya tienes una cuenta? <Link to="/login">Inicia sesión aquí</Link>.
        </p>
      </footer>
    </div>
  );
};

export default Inicio;
