import { api } from "./api";

export const getCategories = async () =>
{
    const response = await api.get("/categories");
    return response.data;
};

export const getCategoriesWithSales = async () =>
{
    const response = await api.get("/categories/with-sales");
    return response.data;
};