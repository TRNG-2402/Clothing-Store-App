export interface Product
{
    productId: number,
    name: string,
    categoryId: number,
    description: string | null,
    price: number,
    finalPrice: number,
    hasActiveSale: boolean,
    discountPercentage?: number,
    stockQuantity: number
}