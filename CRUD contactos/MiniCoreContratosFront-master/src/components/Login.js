import React from 'react';
import { GoogleOAuthProvider, GoogleLogin } from '@react-oauth/google';
import { useNavigate } from 'react-router-dom';

function Login() {
    const navigate = useNavigate();

    return (
        <GoogleOAuthProvider clientId="729320449938-0lpd8fran2mbogudp6sfn794snhf634u.apps.googleusercontent.com">
            <GoogleLogin
                onSuccess={credentialResponse => {
                    console.log(credentialResponse);
                    // Redirige al usuario a '/crud' después de un inicio de sesión exitoso
                    navigate('/crud');
                }}
                onError={() => {
                    console.log('Login Failed');
                }}
            />
        </GoogleOAuthProvider>
    );
}

export { Login };
