

import './App.css'

import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

import Footer from './Components/layout/Footer'
import NavBar from './Components/layout/NavBar'
import Home from './Components/pages/Home';
import Operations from './Components/pages/Operations';
import Products from './Components/pages/Products';
import AddProductForm from './Components/form/AddProductForm';
import AddOperationForm from './Components/form/AddOperationForm';
import EditProductForm from './Components/form/EditProductForm';

function App() {


  return (


    <Router>

      <NavBar />

      <Routes>

        <Route path='/' element={<Home />}></Route>
        <Route path='/operations' element={<Operations />}></Route>
        <Route path='/products' element={<Products />}></Route>
        <Route path='/products/newProduct' element={<AddProductForm />}></Route>
        <Route path='/operations/newOperation/:stockId' element={<AddOperationForm />}></Route>
        
        <Route path='/products/editProduct/:productId' element={<EditProductForm />}></Route>




      </Routes>

      <Footer />

    </Router>
  )
}

export default App
