import React from 'react'
import { Link, useParams } from 'react-router-dom'
import NavBar from '../components/NavBar'
import styles from './ProductDetails.module.css'


type Product = {
  id: number;
  name: string;
  price: number;
  categoryName: string;
};

const fakeProducts: Product[] = [
  { id: 1, name: "Laptop", price: 500, categoryName: "other" },
  { id: 2, name: "Phone", price: 1000, categoryName: "other" },
  { id: 3, name: "Shirt", price: 24.99, categoryName: "tops" },
  { id: 4, name: "Pants", price: 30, categoryName: "bottoms" }
];

export default function ProductDetails()
{
  const { id } = useParams<{ id: string }>();

  const product = fakeProducts.find(
    p => p.id === Number(id)
  );

  if (!product) return <div>Product not found</div>;

  return (
    <div className={styles.page}>
      <div className={styles.container}>


        <h1 className={styles.title}>
          {product.name}
        </h1>

        <div className={styles.priceRow}>
          <span className={styles.newPrice}>
            ${product.price.toFixed(2)}
          </span>
        </div>




        <div className={styles.buttonRow}>
          <button className={styles.primaryButton}>
            Add To Bag
          </button>
        </div>

      </div>
    </div>
  );
}