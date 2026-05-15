import type { Product } from '../types/Product'
import styles from "./ProductCard.module.css"
import { Link } from 'react-router-dom';


interface ProductCardProps
{
    product: Product;
}
 

function ProductCard({ product }: ProductCardProps)
{
    
    return (
       
        <Link to={`/product/${product.id}`} style={{ textDecoration: "none", color: "inherit" }}>
        <div className={styles.card}>

            <h3 className={styles.name}>
                {product.name}
                
            </h3>

            <div
                className={`${styles.priceSection} ${!product.hasActiveSale ? styles.centerPrice : ""
                    }`}
            >

                {product.hasActiveSale ? (
                    <>
                        <span className={styles.originalPrice}>
                            ${product.price.toFixed(2)}
                        </span>

                        <span className={styles.salePrice}>
                            ${product.finalPrice.toFixed(2)}
                        </span>

                        <span className={styles.saleTag}>
                            {product.discountPercentage}% OFF
                        </span>
                    </>
                ) : (
                    <span className={styles.normalPrice}>
                        ${product.price.toFixed(2)}
                    </span>
                )}

            </div>

            <p className={styles.stock}>
                Stock: {product.stockQuantity}
            </p>

        </div>
        </Link>
    );
}

export default ProductCard;