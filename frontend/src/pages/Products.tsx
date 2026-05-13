import React, { useEffect, useState } from 'react'
import { Link, useParams } from 'react-router-dom'
import NavBar from '../components/NavBar'
import styles from './Products.module.css'

type Product = {
  id: number;
  name: string;
  price: number;
  categoryName: string;
};

export default function Products()
{

  const [products, setProducts] = useState<Product[]>([]);

  const fakeProducts: Product[] = [
    { id: 1, name: "Laptop", price: 500, categoryName: "other" },
    { id: 2, name: "Phone", price: 1000, categoryName: "other" },
    { id: 3, name: "Shirt", price: 24.99, categoryName: "tops" },
    { id: 4, name: "Pants", price: 30, categoryName: "bottoms"}
  ];

  const getProductsByCategory = (categoryName: string): Product[] =>
  {
    return fakeProducts.filter(p => p.categoryName === categoryName);
  };

  const { categoryName } = useParams<{ categoryName: string }>();

  useEffect(() =>
  {
    if (!categoryName) return;

    const data = getProductsByCategory(categoryName);
    setProducts(data);
  }, [categoryName]);

 
  const [sortOrder, setSortOrder] = useState<string>(""); // "low" | "high"

  useEffect(() => {
  if (!categoryName) return;

  let data = fakeProducts.filter(
    p => p.categoryName === categoryName
  );

  if (sortOrder === "low") {
    data = data.sort((a, b) => a.price - b.price);
  }

  if (sortOrder === "high") {
    data = data.sort((a, b) => b.price - a.price);
  }

  setProducts([...data]); // spread = avoid mutation issues
}, [categoryName, sortOrder]);

  return (
    <div style={{ backgroundColor: "#f5f5f5" , minHeight: "100vh" }}>
      <NavBar />

   <div className={styles.header}>
  <h2 style={{ color: "#777"}}>{categoryName}</h2>

  <select onChange={(e) => setSortOrder(e.target.value)}>
    <option value="">Sort</option>
    <option value="low">Lowest Price</option>
    <option value="high">Highest Price</option>
  </select>
</div>

      <div className={styles.grid}>
  {products.map((p: Product) => (
    <div key={p.id} className={styles.card}>

      <div className={styles.title}>{p.name}</div>

      <div className={styles.price}>
        ${p.price.toFixed(2)}
      </div>

      <Link to={`'/product/${p.id}`} className={styles.viewButton}>
    View Product
  </Link>


    </div>
  ))}
</div>

     </div>)
}