import React from 'react'
import { Link } from 'react-router-dom' 
import styles from "./NavBar.module.css" 

//changes link to specific categoryNames we'll use

export default function NavBar()
{
    return (
        <nav className={styles.css}>
            <Link to="/" className={styles.link}>Home</Link>

           <Link to="/products/tops" className={styles.link}>Tops</Link>
            <Link to="/products/bottoms" className={styles.link}>Bottoms</Link>
            <Link to="/products/outerwear" className={styles.link}>Jackets/Coats</Link>

            <Link to="/profile" className={styles.link}>Profile</Link>
            
            <Link to="/cart" className={styles.link}>Cart</Link>


            <Link to="/login" className={styles.link}>Login</Link>
            <Link to="/login" className={styles.link}>Logout</Link>
        </nav>
    )
}
