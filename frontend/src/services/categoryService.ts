import { api } from "./api";
import type { Category } from "../types/Category";
import type { CategoryWithSale } from "../types/CategoryWithSale";
import type { Product } from "../types/Product";

export const getCategories = async (): Promise<Category[]> =>
{
    const response = await api.get("/categories");
    return response.data;
};

export const getCategoriesWithSales = async (): Promise<CategoryWithSale[]> =>
{
    const response = await api.get("/categories/with-sales");
    return response.data;
};

export const getCategoryById = async (categoryId: number): Promise<Category> =>
{
    const response = await api.get(`/categories/${categoryId}`);
    return response.data;
};

export const getProductsByCategory = async (categoryId: number): Promise<Product[]> =>
{
    const response = await api.get(`/categories/${categoryId}/products`);
    return response.data;
};
