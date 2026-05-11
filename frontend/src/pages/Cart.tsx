import React from 'react'
import { Link } from 'react-router-dom'
import { useReducer } from "react";

type CartItem = {
  id: number;
  name: string;
  quantity: number;
  stockQuantity: number;
};

type CartAction =
  | { type: "DELETE_ITEM"; payload: number }
  | { type: "UPDATE_QUANTITY"; payload: { id: number; quantity: number } };


function cartReducer(state: CartItem[], action: CartAction): CartItem[]
{
  switch (action.type)
  {
    case "DELETE_ITEM":
      return state.filter(item => item.id !== action.payload);

    case "UPDATE_QUANTITY":
      return state.map(item =>
        item.id === action.payload.id
          ? { ...item, quantity: action.payload.quantity }
          : item
      );

    default:
      return state;
  }
}

//need to complete
export default function CartPage()
{


  return (
    <div>
      <h2>Cart</h2>

      {/* 

      {cartItems.map(item => (
        <CartItem key={item.id} item={item} dispatch={dispatch} />
      ))}
      
      */}


    </div>
  );
}