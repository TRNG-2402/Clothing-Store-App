/*
import React from 'react'
import { Link, useNavigate } from 'react-router-dom'
import styles from "./NavBar.module.css"
import { useAuth } from '../context/AuthContext';
import cart from "../assets/cart.png";
import user from "../assets/user.png";

//changes link to specific categoryNames we'll use

export default function NavBar()
{
    return (


        <nav className={styles.navbar}>
            <div className={styles.topRow}>
                <div className={styles.logo}><Link to="/" className={styles.link}>Home</Link></div>

                <div className={styles.mainLinks}>
                <Link to="/category/tops" className={styles.link}>Tops</Link>
                <Link to="/category/bottoms" className={styles.link}>Bottoms</Link>
                <Link to="/category/outerware" className={styles.link}> Jackets/Coats</Link>
                <Link to="/category/other" className={styles.link}> Others</Link>
                </div>

                <div className={styles.actions}>
                    <input placeholder="Search..." />
                    <div className={styles.icons}><Link to="/profile" className={styles.link}><img src={user} width="25" height="25" alt="" /></Link>
                        <Link to="/cart" className={styles.link}><img src={cart} width="25" height="25" alt="" /></Link>
                    </div>
                </div>
            </div>
        </nav>


    )
}
*/
import React from 'react'
import { Link } from 'react-router-dom'
import styles from "./NavBar.module.css"

import cart from "../assets/cart.png";
import user from "../assets/user.png";

export default function NavBar()
{
    return (
        <nav className={styles.navbar}>
            <div className={styles.topRow}>

                {/* LEFT / LOGO */}
                <div className={styles.logo}>
                    <Link to="/" className={styles.link}>
                        Home
                    </Link>
                </div>

                {/* MAIN NAV */}
                <div className={styles.mainLinks}>
                    <Link to="/categories" className={styles.link}>
                        Categories
                    </Link>

                    <Link to="/products" className={styles.link}>
                        Products
                    </Link>
                </div>

                {/* RIGHT ACTIONS */}
                <div className={styles.actions}>
                    <input placeholder="Search..." />

                    <div className={styles.icons}>
                        <Link to="/profile" className={styles.link}>
                            <img src={user} width="25" height="25" alt="" />
                        </Link>

                        <Link to="/cart" className={styles.link}>
                            <img src={cart} width="25" height="25" alt="" />
                        </Link>
                    </div>
                </div>

            </div>
        </nav>
    )
}
