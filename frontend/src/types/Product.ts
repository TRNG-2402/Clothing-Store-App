export interface Product
{
    id: number,
    name: string,
    categoryId: number,
    description: string | null,
    price: number,
    finalPrice: number,
    hasActiveSale: boolean,
    discountPercentage?: number,
    stockQuantity: number
}