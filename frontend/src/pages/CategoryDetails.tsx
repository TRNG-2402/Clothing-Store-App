import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";

import NavBar from "../components/NavBar";
import ProductCard from "../components/ProductCard";
import { getCategoryById, getProductsByCategory } from "../services/categoryService";
import type { Category } from "../types/Category";
import type { Product } from "../types/Product";
import styles from "./Categories.module.css";

export default function CategoryDetails()
{
    const { categoryId } = useParams<{ categoryId: string }>();
    const [category, setCategory] = useState<Category | null>(null);
    const [products, setProducts] = useState<Product[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() =>
    {
        const loadCategory = async () =>
        {
            const id = Number(categoryId);

            if (!categoryId || Number.isNaN(id))
            {
                setError("Invalid category.");
                setIsLoading(false);
                return;
            }

            try
            {
                setIsLoading(true);
                setError(null);

                const [categoryData, productData] = await Promise.all([
                    getCategoryById(id),
                    getProductsByCategory(id)
                ]);

                setCategory(categoryData);
                setProducts(productData);
            } catch (err)
            {
                console.error("Failed to load category", err);
                setError("Failed to load this category.");
            } finally
            {
                setIsLoading(false);
            }
        };

        loadCategory();
    }, [categoryId]);

    return (
        <div className={styles.page}>
            <NavBar />

            <Link to="/categories" className={styles.backLink}>
                Back to categories
            </Link>

            <h1 className={styles.header}>
                {category?.name ?? "Category"}
            </h1>

            {isLoading && <p className={styles.loading}>Loading products...</p>}

            {error && <p className={styles.error}>{error}</p>}

            {!isLoading && !error && products.length === 0 && (
                <p className={styles.emptyState}>No products found in this category.</p>
            )}

            {!isLoading && !error && products.length > 0 && (
                <div className={styles.productGrid}>
                    {products.map(product => (
                        <ProductCard key={product.id} product={product} />
                    ))}
                </div>
            )}
        </div>
    );
}
