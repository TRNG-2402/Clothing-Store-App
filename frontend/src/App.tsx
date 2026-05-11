import React from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from './assets/vite.svg'
import heroImg from './assets/hero.png'
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
      <section id="center">
        <div className="hero">
          <img src={heroImg} className="base" width="170" height="179" alt="" />
          <img src={reactLogo} className="framework" alt="React logo" />
          <img src={viteLogo} className="vite" alt="Vite logo" />
        </div>
        <div>
          <h1>Get started</h1>
        </div>
      </section>

      <Routes>
        <Route path='/' element={<Home />} />

        <Route path='/profile' element={<Profile />} /> 
        <Route path='/profile/orders' element={<Orders />} />
        <Route path='/profile/orders/:orderId' element={<OrderDetails />} />

        <Route path='/products/:categoryId' element={<Products />} />
        <Route path='/products/:productId' element={<ProductDetails />} />

        <Route path='/cart' element={<Cart />} />

        <Route path='/login' element={<Login />} />

        <Route path='*' element={<NotFound />} />
      </Routes>

  

    </>
  )
}

export default App
