import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import styles from "./Categories.module.css";

import { getCategories } from "../services/categoryService";
import type { Category } from "../types/Category";
import NavBar from "../components/NavBar";

function Categories()
{
    const [categories, setCategories] = useState<Category[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();

    useEffect(() =>
    {
        fetchCategories();
    }, []);

    const fetchCategories = async () =>
    {
        try
        {
            setIsLoading(true);
            setError(null);
            const data = await getCategories();
            setCategories(data);
        } catch (err)
        {
            console.error("Failed to load categories", err);
            setError("Failed to load categories.");
        } finally
        {
            setIsLoading(false);
        }
    };

    const handleClick = (categoryId: number) =>
    {
        navigate(`/categories/${categoryId}`);
    };

    return (
        <div className={styles.page}>
            <NavBar />

            <h1 className={styles.header}>Categories</h1>

            {isLoading && <p className={styles.loading}>Loading categories...</p>}

            {error && <p className={styles.error}>{error}</p>}

            <div className={styles.grid}>
                {categories.map(cat => (
                    <div
                        key={cat.categoryId}
                        className={styles.card}
                        onClick={() => handleClick(cat.categoryId)}
                    >
                        <h2 className={styles.name}>{cat.name}</h2>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default Categories;