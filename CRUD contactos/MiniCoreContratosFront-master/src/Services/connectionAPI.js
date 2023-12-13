import axios from "axios";

export async function addContacto(contactoData) {
    try {
        const response = await axios.post("http://localhost:5179/api/v1/Contacto", contactoData) ;
      // Aquí puedes manejar la respuesta si lo necesitas.
      // Por ejemplo, si la API devuelve algún mensaje, puedes retornarlo aquí.
      return response.data;
    } catch (error) {
      // Aquí puedes manejar los errores, si ocurre alguno.
      // Por ejemplo, si la API devuelve un error 404 (recurso no encontrado) o 500 (error interno del servidor).
      // Puedes retornar o lanzar un error personalizado para manejarlo en el componente que llama a esta función.
      throw error;
    }
  }

  //Usando Fetch
// export async function addContacto(contactoData) {
//     try {
//       const response = await fetch("http://localhost:5179/api/v1/Contacto", {
//         method: "POST",
//         headers: {
//           "Content-Type": "application/json",
//         },
//         body: JSON.stringify(contactoData),
//       });
//       if (!response.ok) {
//         throw new Error("Network response was not ok");
//       }
//       return response.json();
//     } catch (error) {
//       throw error;
//     }
//   }
  


export async function  getContactoslist()
{
    let response = await axios.get("http://localhost:5179/api/v1/Contacto");
    let contactos= response.data;
    return contactos;   
}

export async function deleteContacto(cedula) {
    try {
      const response = await axios.delete(`http://localhost:5179/api/v1/Contacto/${cedula}`);
      // Aquí puedes manejar la respuesta si lo necesitas.
      // Por ejemplo, si la API devuelve algún mensaje, puedes retornarlo aquí.
      return response.data;
    } catch (error) {
      // Aquí puedes manejar los errores, si ocurre alguno.
      // Por ejemplo, si la API devuelve un error 404 (recurso no encontrado) o 500 (error interno del servidor).
      // Puedes retornar o lanzar un error personalizado para manejarlo en el componente que llama a esta función.
      throw error;
    }
  }
  