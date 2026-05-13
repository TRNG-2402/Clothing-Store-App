import { useEffect, useState } from "react";

import styles from "../components/Products.module.css";

import type { Product } from "../types/Product";
import type { Category } from "../types/Category";

import { getProducts } from "../services/productService";
import { getCategories } from "../services/categoryService";

import ProductCard from "../components/ProductCard";
import PaginationControls from "../components/PaginationControls";
import ProductFilters from "../components/ProductFilters";

function Products()
{
    const [products, setProducts] = useState<Product[]>([]);
    const [categories, setCategories] = useState<Category[]>([]);

    const [pageNumber, setPageNumber] = useState(1);
    const [totalPages, setTotalPages] = useState(1);

    // Filters
    const [search, setSearch] = useState("");
    const [categoryId, setCategoryId] = useState<number | "">("");
    const [minPrice, setMinPrice] = useState("");
    const [maxPrice, setMaxPrice] = useState("");

    // Trigger re-fetch
    const [queryTrigger, setQueryTrigger] = useState(0);

    const pageSize = 12;

    // -------------------------
    // FETCH PRODUCTS
    // -------------------------
    useEffect(() =>
    {
        fetchProducts();
    }, [pageNumber, queryTrigger]);

    const fetchProducts = async () =>
    {
        try
        {
            const data = await getProducts({
                pageNumber,
                pageSize,
                search,
                categoryId: categoryId === "" ? undefined : categoryId,
                minPrice: minPrice === "" ? undefined : Number(minPrice),
                maxPrice: maxPrice === "" ? undefined : Number(maxPrice)
            });

            setProducts(data.items);
            setTotalPages(data.totalPages);

            if (data.totalPages > 0 && pageNumber > data.totalPages)
            {
                setPageNumber(1);
            }
        } catch (error)
        {
            console.error("Failed to fetch products", error);
        }
    };

    // -------------------------
    // FETCH CATEGORIES
    // -------------------------
    useEffect(() =>
    {
        fetchCategories();
    }, []);

    const fetchCategories = async () =>
    {
        try
        {
            const data = await getCategories();
            setCategories(data);
        } catch (error)
        {
            console.error("Failed to fetch categories", error);
        }
    };

    const safeTotalPages = Math.max(totalPages, 1);

    return (
        <div className={styles.page}>
            <h1 className={styles.header}>Products</h1>

            {/* FILTERS */}
            <ProductFilters
                search={search}
                setSearch={setSearch}
                categoryId={categoryId}
                setCategoryId={setCategoryId}
                minPrice={minPrice}
                setMinPrice={setMinPrice}
                maxPrice={maxPrice}
                setMaxPrice={setMaxPrice}
                categories={categories}
                onSearch={() =>
                {
                    setPageNumber(1);
                    setQueryTrigger(prev => prev + 1);
                }}
            />

            {/* GRID */}
            <div className={styles.productGrid}>
                {products.map(product => (
                    <ProductCard
                        key={product.productId}
                        product={product}
                    />
                ))}
            </div>

            {/* PAGINATION */}
            <div className={styles.pagination}>
                <PaginationControls
                    pageNumber={pageNumber}
                    totalPages={safeTotalPages}
                    onPageChange={setPageNumber}
                />
            </div>
        </div>
    );
}

export default Products;