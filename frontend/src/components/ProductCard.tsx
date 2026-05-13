import type { Product } from '../types/Product'
import styles from "./ProductCard.module.css"

interface ProductCardProps
{
    product: Product;
}

function ProductCard({ product }: ProductCardProps)
{
    return (
        <div className={styles.card}>
            <h3>{product.name}</h3>

            <p>${product.price.toFixed(2)}</p>

            <p>Stock: {product.stockQuantity}</p>
        </div>
    );
}

export default ProductCard;