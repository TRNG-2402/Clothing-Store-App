import React, { useReducer } from "react";
import { Link } from "react-router-dom";
import NavBar from "../components/NavBar";
import styles from "./Cart.module.css";

type CartItem = {
  id: number;
  name: string;
  quantity: number;
  price: number;
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
      return state.filter(item => item.id !== Number(action.payload));

    case "UPDATE_QUANTITY":
      return state.map(item =>
      {
        if (item.id === action.payload.id)
        {
          const newQty = Math.max(
            1,
            Math.min(action.payload.quantity, item.stockQuantity)
          );
          return { ...item, quantity: newQty };
        }
        return item;
      });

    default:
      return state;
  }
}



export default function CartPage()
{
  const [cartItems, dispatch] = useReducer(cartReducer, [
    { id: 1, name: "Item A", quantity: 1, price: 12.00, stockQuantity: 5 },
    { id: 2, name: "Item B", quantity: 2, price: 8.50, stockQuantity: 10 }
  ]);

  const subtotal = cartItems.reduce(
    (sum, item) => sum + item.price * item.quantity,
    0
  );

  const totalItems = cartItems.reduce(
    (sum, item) => sum + item.quantity,
    0
  );

  return (
    <div>
      <NavBar />

      <div className={styles.container}>
        {/* LEFT */}
        <div className={styles.left}>
          <h2 className={styles.title}>Shopping Bag</h2>

          <div className={styles.tabs}>
            <span className={styles.activeTab}>In Bag ({cartItems.length})</span>
          </div>



          {cartItems.map(item => (
            <div key={item.id} className={styles.cartItem}>

              {/* LEFT */}
              <div className={styles.itemDetails}>
                <div className={styles.itemName}>{item.name}</div>

                <div className={styles.priceRow}>
                  <span className={styles.oldPrice}>
                    ${item.price.toFixed(2)}
                  </span>
                </div>

                <div className={styles.actions}>
                  <span><button

                    onClick={() =>
                    {
                      console.log("deleting", item.id); // 👈 keep this to verify
                      dispatch({ type: "DELETE_ITEM", payload: item.id });
                    }}
                  >
                    Delete
                  </button>
                  </span>
                </div>

              </div>

              {/* RIGHT (GROUPED) */}
              <div className={styles.itemRight}>



                <div className={styles.controls}>
                  <p>Quantity</p>

                  <select
                    className={styles.select}
                    value={item.quantity}
                    onChange={(e) =>
                      dispatch({
                        type: "UPDATE_QUANTITY",
                        payload: {
                          id: item.id,
                          quantity: Number(e.target.value)
                        }
                      })
                    }
                  >
                    {[...Array(item.stockQuantity)].map((_, i) => (
                      <option key={i + 1} value={i + 1}>
                        {i + 1}
                      </option>
                    ))}
                  </select>
                </div>

              </div>
            </div>
          ))}
        </div>




        {/* RIGHT */}
        <div className={styles.right}>
          <h3 className={styles.summaryTitle}>Order Summary</h3>

          <div className={styles.summaryRow}>
            <span>Items</span>
            <span>{totalItems}</span>
          </div>

          {/*
          <div className={styles.summaryRow}>
            <span>Promotions</span>
            <span>$0</span>
          </div>
          */}

          <div className={styles.summaryRow}>
            <strong>Subtotal</strong>
            <strong><span>${subtotal.toFixed(2)}</span></strong>
          </div>

          <button className={styles.checkoutBtn}>
            Checkout
          </button>
        </div>
      </div>

    </div>
  );
}