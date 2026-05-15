import { api } from "./api";
import type { Product } from "../types/Product";
import type { PagedResult } from "../types/pagedResults";
import type { ProductQueryParams } from "../types/productQueryParams";

export const getProducts = async (
    params: ProductQueryParams
): Promise<PagedResult<Product>> =>
{
    const response = await api.get("/products", {
        params
    });

    return response.data;
};