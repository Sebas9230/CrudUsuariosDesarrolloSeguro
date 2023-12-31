import { Routes,Route } from 'react-router-dom';
import Inicio from "./components/Inicio";
import { CrudUsuario } from "./components/CrudUsuario";

function App() {
  return (
    <div className='App'>
          <Routes>
            <Route path='/' element={<Inicio></Inicio>}></Route>
            <Route path='/crud' element={<CrudUsuario></CrudUsuario>}></Route>              
          </Routes>
    </div>
  );
}

export default App;
