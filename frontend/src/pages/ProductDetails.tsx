import React, { useEffect, useState } from 'react'
import { Link, useParams } from 'react-router-dom'
import NavBar from '../components/NavBar'
import styles from './ProductDetails.module.css'
import type { Product } from '../types/Product'


console.log("ProductDetails PAGE RENDERED");


export default function ProductDetails()
{
  const { id } = useParams<{ id: string }>();

 const [product, setProduct] = useState<Product | null>(null);
 const [quantity, setQuantity] = useState(1);

 const [message, setMessage] = useState<string | null>(null);

useEffect(() => {
  const fetchProduct = async () => {
    try {
      const res = await fetch(
  `${import.meta.env.VITE_API_BASE_URL}/products/${id}`
);

      if (!res.ok) {
        console.error("API error:", res.status);
        setProduct(null);
        return;
      }

      const data = await res.json();
      console.log("PRODUCT FROM API:", data);

      setProduct(data);
    } catch (err) {
      console.error("Fetch failed:", err);
      setProduct(null);
    }
  };

  fetchProduct();
}, [id]);


useEffect(() => {
  if (product) {
    setQuantity(1);
  }
}, [product]);

  if (!product) return <div>Product not found</div>;



   const handleAddToCart = async () => {
  try {
    const res = await fetch(
      `${import.meta.env.VITE_API_BASE_URL}/cart/items`,
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("token")}`
          
        },
        body: JSON.stringify({
          productId: product.id,
          quantity
        })
      }
    );

    if (!res.ok) {
      setMessage("Failed to add item");
      setTimeout(() => setMessage(null), 2000);
      return;
    }

    setMessage("Item added to cart!");

    setTimeout(() => {
      setMessage(null);
    }, 3000);

  } catch (err) {
    console.error(err);
    setMessage("Something went wrong");

    setTimeout(() => setMessage(null), 2000);
  }
};



  return (
    <div><NavBar/>
    <div className={styles.page}>
      
      <div className={styles.container}>


        <h1 className={styles.title}>
          {product.name}
        </h1>

        <div className={styles.priceRow}>
          <span className={styles.newPrice}>
            ${product.finalPrice.toFixed(2)}
          </span>
        </div>



        <div className={styles.quantityRow}>
  <label>Quantity:</label>

  <select
    value={quantity}
    onChange={(e) => setQuantity(Number(e.target.value))}
  >
    {product.stockQuantity > 0 &&
      Array.from({ length: product.stockQuantity }, (_, i) => i + 1).map(
        (num) => (
          <option key={num} value={num}>
            {num}
          </option>
        )
      )}
  </select>
</div>


        <div className={styles.buttonRow}>
          <button
            className={styles.primaryButton}
            onClick={handleAddToCart}
          >
            Add To Cart
          </button>
        </div>

        {message && (
  <div className={styles.toast}>
    {message}
  </div>
)}

      </div>
    </div>
    </div>
  );
}