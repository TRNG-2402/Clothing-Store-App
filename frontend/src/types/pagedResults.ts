export type PagedResult<T> = {
    items: T[];

    pageNumber: number;

    pageSize: number;

    totalCount: number;

    totalPages: number;
};