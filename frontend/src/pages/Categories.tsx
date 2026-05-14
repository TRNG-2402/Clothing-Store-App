/*
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import styles from "./Categories.module.css";

import { getCategoriesWithSales } from "../services/categoryService";
import type { CategoryWithSale } from "../types/CategoryWithSale";

export default function Categories()
{
    const [categories, setCategories] = useState<CategoryWithSale[]>([]);
    const [loading, setLoading] = useState(true);

    const navigate = useNavigate();

    useEffect(() =>
    {
        const loadCategories = async () =>
        {
            try
            {
                const data = await getCategoriesWithSales();
                setCategories(data);
            } catch (err)
            {
                console.error("Failed to load categories", err);
            } finally
            {
                setLoading(false);
            }
        };

        loadCategories();
    }, []);

    if (loading)
    {
        return <div className={styles.loading}>Loading categories...</div>;
    }

    return (
        <div className={styles.page}>
            <h1 className={styles.title}>Categories</h1>

            <div className={styles.grid}>
                {categories.map((cat) => (
                    <div
                        key={cat.categoryId}
                        className={styles.card}
                        onClick={() =>
                            navigate(`/products?categoryId=${cat.categoryId}`)
                        }
                    >
                        <h3 className={styles.name}>{cat.name}</h3>

                        {cat.hasActiveSale && (
                            <div className={styles.saleBadge}>
                                {cat.discountPercentage}% OFF
                            </div>
                        )}
                    </div>
                ))}
            </div>
        </div>
    );
}
*/
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import styles from "./Categories.module.css";

import { getCategoriesWithSales } from "../services/categoryService";
import type { CategoryWithSale } from "../types/CategoryWithSale";
import NavBar from "../components/NavBar";

function Categories()
{
    const [categories, setCategories] = useState<CategoryWithSale[]>([]);
    const navigate = useNavigate();

    useEffect(() =>
    {
        fetchCategories();
    }, []);

    const fetchCategories = async () =>
    {
        try
        {
            const data = await getCategoriesWithSales();
            setCategories(data);
        } catch (err)
        {
            console.error("Failed to load categories", err);
        }
    };

    const handleClick = (categoryId: number) =>
    {
        navigate(`/products?categoryId=${categoryId}`);
    };

    return (
        <div className={styles.page}>
            <NavBar />

            <h1 className={styles.header}>Categories</h1>

            <div className={styles.grid}>
                {categories.map(cat => (
                    <div
                        key={cat.categoryId}
                        className={styles.card}
                        onClick={() => handleClick(cat.categoryId)}
                    >
                        <h2>{cat.name}</h2>

                        {cat.hasActiveSale && (
                            <div className={styles.saleBadge}>
                                {cat.discountPercentage}% OFF
                            </div>
                        )}
                    </div>
                ))}
            </div>
        </div>
    );
}

export default Categories;