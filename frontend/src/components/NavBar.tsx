import { Link } from 'react-router-dom'
import styles from "./NavBar.module.css"

import cart from "../assets/cart.png";
import user from "../assets/user.png";

export default function NavBar() {
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
          {/* <input placeholder="Search..." /> */}

          <div className={styles.icons}>
            <Link to="/profile" className={styles.link}>
              <img src={user} width="25" height="25" alt="" />
            </Link>

            <Link to="/cart" className={styles.link}>
              <img src={cart} width="25" height="25" alt="" />
            </Link>
          </div>

        </div> {/* ✅ THIS WAS MISSING */}

      </div>
    </nav>
  );
}