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
import Register from './pages/Register'
import Categories from './pages/Categories'
import ProtectedRoute from './components/ProtectedRoute'

function App()
{
  return (
    <Routes>
      <Route path='/' element={<Home />} />

      <Route path='/categories' element={<Categories />} />

      <Route path='/products' element={<Products />} />

      <Route path='/product/:id' element={<ProductDetails />} />

      <Route path='/cart' element={<ProtectedRoute><Cart /></ProtectedRoute>} />

      <Route path='/profile' element={<ProtectedRoute><Profile /></ProtectedRoute>} />

      <Route path='/profile/orders' element={<ProtectedRoute><Orders /></ProtectedRoute>} />

      <Route path='/profile/orders/:orderId' element={<ProtectedRoute><OrderDetails /></ProtectedRoute>} />

      <Route path='/login' element={<Login />} />

      <Route path='/register' element={<Register />} />

      <Route path='*' element={<NotFound />} />
    </Routes>
    /*
    <>
      <Routes>
        <Route path='/' element={<Home />} />

        <Route
          path='/profile'
          element={
            <ProtectedRoute>
              <Profile />
            </ProtectedRoute>
          }
        />

        <Route
          path='/profile/orders'
          element={
            <ProtectedRoute>
              <Orders />
            </ProtectedRoute>
          }
        />

        <Route
          path='/profile/orders/:orderId'
          element={
            <ProtectedRoute>
              <OrderDetails />
            </ProtectedRoute>
          }
        />

        <Route path='/category/:categoryName' element={<Products />} />
        <Route path='/product/:id' element={<ProductDetails />} />

        <Route
          path='/cart'
          element={
            <ProtectedRoute>
              <Cart />
            </ProtectedRoute>
          }
        />

        <Route path='/login' element={<Login />} />

        <Route path='/register' element={<Register />} />

        <Route path='*' element={<NotFound />} />
      </Routes>
    </>*/
  );
}

export default App
