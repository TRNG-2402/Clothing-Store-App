export type ProductQueryParams = {
    search?: string;

    categoryId?: number;

    minPrice?: number;

    maxPrice?: number;

    sortBy?: string;

    descending?: boolean;

    pageNumber?: number;

    pageSize?: number;
};