import { useEffect, useRef, useState } from "react";
import { useSearchParams } from "react-router-dom";

import styles from "../components/Products.module.css";

import type { Product } from "../types/Product";
import type { Category } from "../types/Category";

import { getProducts } from "../services/productService";
import { getCategories } from "../services/categoryService";

import ProductCard from "../components/ProductCard";
import PaginationControls from "../components/PaginationControls";
import ProductFilters from "../components/ProductFilters";
import NavBar from "../components/NavBar";

function Products() {
    const [products, setProducts] = useState<Product[]>([]);
    const [categories, setCategories] = useState<Category[]>([]);
    const [totalPages, setTotalPages] = useState(1);

    const [searchParams] = useSearchParams();
    const urlCategoryId = searchParams.get("categoryId");

    const pageSize = 12;

    // -------------------------
    // SINGLE SOURCE OF TRUTH
    // -------------------------
    const [query, setQuery] = useState({
        pageNumber: 1,
        search: "",
        categoryId: "" as number | "",
        minPrice: "",
        maxPrice: ""
    });

    // -------------------------
    // RACE CONDITION GUARD
    // -------------------------
    const requestIdRef = useRef(0);

    // -------------------------
    // DEBUG LOG (STATE)
    // -------------------------
    useEffect(() => {
        console.log("QUERY UPDATED:", query);
    }, [query]);

    // -------------------------
    // LOAD CATEGORIES
    // -------------------------
    useEffect(() => {
        getCategories().then(setCategories);
    }, []);

    // -------------------------
    // APPLY URL CATEGORY ONCE
    // -------------------------
    useEffect(() => {
        setQuery(prev => ({
            ...prev,
            categoryId: urlCategoryId ? Number(urlCategoryId) : "",
            pageNumber: 1
        }));
    }, [urlCategoryId]);

    // -------------------------
    // FETCH PRODUCTS (SAFE)
    // -------------------------
    useEffect(() => {
        const fetch = async () => {
            const currentRequest = ++requestIdRef.current;

            console.log("FETCH START:", {
                requestId: currentRequest,
                query
            });

            const data = await getProducts({
                pageNumber: query.pageNumber,
                pageSize,
                search: query.search,
                categoryId:
                    query.categoryId === "" ? undefined : query.categoryId,
                minPrice:
                    query.minPrice === "" ? undefined : Number(query.minPrice),
                maxPrice:
                    query.maxPrice === "" ? undefined : Number(query.maxPrice)
            });

            // -------------------------
            // RACE CONDITION CHECK
            // -------------------------
            if (currentRequest !== requestIdRef.current) {
                console.log("STALE RESPONSE IGNORED:", currentRequest);
                return;
            }

            console.log("FETCH SUCCESS:", {
                requestId: currentRequest,
                items: data.items.length
            });

            setProducts(data.items);
            setTotalPages(data.totalPages);
        };

        fetch();
    }, [query]);

    // -------------------------
    // UPDATE QUERY HELPER
    // -------------------------
    const updateQuery = (updates: Partial<typeof query>) => {
        setQuery(prev => ({
            ...prev,
            ...updates
        }));
    };

    return (
        <div className={styles.page}>
            <NavBar />

            <h1 className={styles.header}>Products</h1>

            <ProductFilters
                search={query.search}
                setSearch={(val: string) =>
                    updateQuery({ search: val, pageNumber: 1 })
                }
                categoryId={query.categoryId}
                setCategoryId={(val: number | "") =>
                    updateQuery({ categoryId: val, pageNumber: 1 })
                }
                minPrice={query.minPrice}
                setMinPrice={(val: string) =>
                    updateQuery({ minPrice: val, pageNumber: 1 })
                }
                maxPrice={query.maxPrice}
                setMaxPrice={(val: string) =>
                    updateQuery({ maxPrice: val, pageNumber: 1 })
                }
                categories={categories}
            />

            <div className={styles.productGrid}>
                {products.map(p => (
                    <ProductCard key={p.productId} product={p} />
                ))}
            </div>

            <PaginationControls
                pageNumber={query.pageNumber}
                totalPages={Math.max(totalPages, 1)}
                onPageChange={(page: number) =>
                    updateQuery({ pageNumber: page })
                }
            />
        </div>
    );
}

export default Products;