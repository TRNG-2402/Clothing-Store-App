import { data, Link } from "react-router-dom";
import NavBar from "../components/NavBar";
import styles from "./Cart.module.css";
import React, { useEffect, useState } from "react";

type CartItem = {
  id: number;
  name: string;
  quantity: number;
  price: number;
  stockQuantity: number;
};

export default function CartPage() {
  const [cartItems, setCartItems] = useState<CartItem[]>([]);

  const API = import.meta.env.VITE_API_BASE_URL;

  // -------------------------
  // GET CART FROM API
  // -------------------------
  useEffect(() => {
    const fetchCart = async () => {
      const res = await fetch(`${API}/cart`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("token")}`
        }
      });
      

      const data = await res.json();
      setCartItems(data);
    };

    fetchCart();
  }, []);


  // -------------------------
  // DELETE (backend sync)
  // -------------------------
  const handleDelete = async (productId: number) => {
    await fetch(`${API}/cart/items/${productId}`, {
      method: "DELETE",
      headers: {
        Authorization: `Bearer ${localStorage.getItem("token")}`
      }
    });

    setCartItems(prev =>
      prev.filter(item => item.id !== productId)
    );
  };

  // -------------------------
  // UPDATE QTY (backend sync)
  // -------------------------
  const handleUpdate = async (productId: number, quantity: number) => {
   // console.log("UPDATE CALL:", productId, typeof productId);
    await fetch(`${API}/cart/item`, {
      method: "PATCH",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("token")}`
      },
      body: JSON.stringify({
        productId,
        quantity
      })
    });


    setCartItems(prev =>
      prev.map(item =>
        item.id === productId
          ? { ...item, quantity }
          : item
      )
    );
  };

  // -------------------------
  // YOUR ORIGINAL CALCULATIONS (UNCHANGED)
  // -------------------------
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
        <div className={styles.left}>
          <h2 className={styles.title}>Shopping Cart</h2>

          <div className={styles.tabs}>
            <span className={styles.activeTab}>
              In Cart ({cartItems.length})
            </span>
          </div>

          {cartItems.map(item => (
            <div key={item.id} className={styles.cartItem}>

              <div className={styles.itemDetails}>
                <div className={styles.itemName}>
                  {item.name}
                </div>

                <div className={styles.priceRow}>
                  <span className={styles.oldPrice}>
                    ${item.price.toFixed(2)}
                  </span>
                </div>

                <div className={styles.actions}>
                  <button
                    onClick={() => handleDelete(item.id)}
                  >
                    Delete
                  </button>
                </div>
              </div>

              <div >
                <div className={styles.controls}>
                  <p>Quantity: {item.quantity}</p>

                  <select
                    value={item.quantity}
                    onChange={(e) =>
                      handleUpdate(
                        item.id,
                        Number(e.target.value)
                      )
                    }
                  >
                    {Array.from(
                      { length: item.stockQuantity },
                      (_, i) => i + 1
                    ).map(num => (
                      <option key={num} value={num}>
                        {num}
                      </option>
                    ))}
                  </select>
                </div>
              </div>

            </div>
          ))}
        </div>

        <div className={styles.right}>
          <h3 className={styles.summaryTitle}>Order Summary</h3>

          <div className={styles.summaryRow}>
            <span>Items</span>
            <span>{totalItems}</span>
          </div>

          <div className={styles.summaryRow}>
            <strong>Subtotal</strong>
            <strong>${subtotal.toFixed(2)}</strong>
          </div>

          <button className={styles.checkoutBtn}>
            Checkout
          </button>
        </div>
      </div>
    </div>
  );
}