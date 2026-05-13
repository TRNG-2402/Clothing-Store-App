import type { Category } from "../types/Category";
import SearchBar from "../components/SearchBar";
import styles from "./ProductFilters.module.css";

interface ProductFilterProps
{
    search: string;
    setSearch: (v: string) => void;

    categoryId: number | "";
    setCategoryId: (v: number | "") => void;

    minPrice: string;
    setMinPrice: (v: string) => void;

    maxPrice: string;
    setMaxPrice: (v: string) => void;

    categories: Category[];

    onSearch: () => void;
}

export default function ProductFilters({
    search,
    setSearch,
    categoryId,
    setCategoryId,
    minPrice,
    setMinPrice,
    maxPrice,
    setMaxPrice,
    categories,
    onSearch
}: ProductFilterProps)
{
    return (
        <div className={styles.filters}>

            <SearchBar
                value={search}
                onSearchChange={setSearch}
            />

            <select
                value={categoryId}
                onChange={(e) =>
                    setCategoryId(
                        e.target.value === ""
                            ? ""
                            : Number(e.target.value)
                    )
                }
            >
                <option value="">All Categories</option>
                {categories.map((cat) => (
                    <option
                        key={cat.categoryId}
                        value={cat.categoryId}
                    >
                        {cat.name}
                    </option>
                ))}
            </select>

            <div className={styles.row}>
                <input
                    type="number"
                    min="0"
                    placeholder="Min Price"
                    value={minPrice}
                    onChange={(e) =>
                        setMinPrice(e.target.value)
                    }
                />

                <input
                    type="number"
                    min="0"
                    placeholder="Max Price"
                    value={maxPrice}
                    onChange={(e) =>
                        setMaxPrice(e.target.value)
                    }
                />
            </div>

            <button
                className={styles.searchButton}
                onClick={onSearch}
            >
                Search
            </button>

        </div>
    );
}