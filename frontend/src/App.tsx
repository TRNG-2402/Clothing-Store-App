import './App.css'
import { Route, Routes } from 'react-router-dom'

import Home from './pages/Home'
import NotFound from './pages/NotFound'
import Cart from './pages/Cart'
import Login from './pages/Login'
import OrderDetails from './pages/OrderDetails'
import Orders from './pages/Orders'
import ProductDetails from './pages/ProductDetails'
import Products from './pages/Products'
import Profile from './pages/Profile'


function App() {

  return (
    <>
      

      <Routes>
        <Route path='/' element={<Home />} />

        <Route path='/profile' element={<Profile />} /> 
        <Route path='/profile/orders' element={<Orders />} />
        <Route path='/profile/orders/:orderId' element={<OrderDetails />} />

        <Route path='/category/:categoryName' element={<Products />} />
        <Route path='/product/:id' element={<ProductDetails />} />

        <Route path='/cart' element={<Cart />} />

        <Route path='/login' element={<Login />} />

        <Route path='*' element={<NotFound />} />
      </Routes>

  

    </>
  )
}

export default App
