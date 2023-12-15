// Inicio.js
import React, { useEffect, useState } from "react";
import { Link } from 'react-router-dom'; // Asegúrate de tener instalado react-router-dom
import "../components/Css/Inicio.css";
import { GoogleOAuthProvider, GoogleLogin } from '@react-oauth/google';
import axios from 'axios';
// import jwt from 'jsonwebtoken';
import { useLocation } from 'react-router-dom';



const Inicio = () => {

  const location = useLocation();

  async function logGoogle(authorizationCode) {
    try {
      const user = await exchangeAuthorizationCode(authorizationCode)

      console.log("user: ", user);
      // const response = await logInGoogle({
      //   uid: user.kid,
      //   name: user.given_name,
      //   surname: user.family_name,
      //   email: user.email,
      //   city: user.sub,
      //   fullName: user.name,
      //   birthdate: user.name,
      //   isAdmin: false,
      //   authorizationCode
      // });
      // console.log("response: ", response);
      // this.$router.push("/");

      //console.log("Inicio de sesión exitoso", user);
    } catch (error) {
      // await this.$swal({
      //   title: "¡Error when trying to log in!",
      //   icon: "error",
      //   showCancelButton: false,
      //   confirmButtonText: "OK",
      // });
      console.error("Inicio de sesión fallido", error);
    }
  }

  async function exchangeAuthorizationCode(authorizationCode) {
    // Realiza una solicitud para intercambiar el código de autorización por un token de acceso
    const data = {
      code: authorizationCode,
      client_id:
        "729320449938-0lpd8fran2mbogudp6sfn794snhf634u.apps.googleusercontent.com",
      client_secret: "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", // Asegúrate de tener tu cliente secreto configurado
      redirect_uri: "http://localhost:5173/auth",
      grant_type: "authorization_code",
    };

    const response = await axios.post(
      "https://oauth2.googleapis.com/token",
      data
    );

    // Accede al token de identificación desde la respuesta
    const idToken = response.data.id_token;

    // ************************IMPORTANTE DECODIFICAR DATOS USUARIO************************
    // Decodifica el token de identificación para obtener información del usuario
    // const decodedToken = jwt.decode(idToken);
    // console.log("Datos del usuario:", decodedToken);
    // return decodedToken;
  }

  async function signIn() {
    const params = new URLSearchParams();
    params.append(
      "client_id",
      "729320449938-0lpd8fran2mbogudp6sfn794snhf634u.apps.googleusercontent.com"
    );
    params.append("redirect_uri", "http://localhost:3000/crud");
    params.append("response_type", "code");
    params.append("scope", "openid email profile");
    window.location.href = `https://accounts.google.com/o/oauth2/v2/auth?${params.toString()}`;
  }

  useEffect(() => {
    
    const params = new URLSearchParams(location.search);    
    console.log("*****************this.props:" , params)
    const authorizationCode = params.get('code');
    // Accede al código de autorización desde los parámetros de la URL
    //const authorizationCode = this.$route.redirectedFrom.query.code; // view Route Revisar
 
    if (authorizationCode) {
      // Si hay un código de autorización, realiza acciones con él
      logGoogle(authorizationCode);
    }
  }, []);

  return (
    <div className="inicio-body">
      <header>
        <h1>Bienvenido a la Aplicación de Administración de Usuarios</h1>
        <p>Una plataforma fácil de usar para gestionar usuarios de manera eficiente.</p>
      </header>

      <main>
        <Link to="/login">
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

        <GoogleOAuthProvider clientId="729320449938-0lpd8fran2mbogudp6sfn794snhf634u.apps.googleusercontent.com">
            <GoogleLogin
                onSuccess={credentialResponse => {
                    console.log(credentialResponse);
                }}
                onError={() => {
                    console.log('Login Failed');
                }}
            />
        </GoogleOAuthProvider>

        <button onClick={signIn}>Google Emergency</button>


        
      </main>

      <footer>
        <p>¿Ya tienes una cuenta? <Link to="/login">Inicia sesión aquí</Link>.</p>
      </footer>
    </div>
  );
};

export default Inicio;