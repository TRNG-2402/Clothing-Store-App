export type CategoryWithSale = {
    categoryId: number;
    name: string;
    hasActiveSale: boolean;
    discountPercentage?: number;
};