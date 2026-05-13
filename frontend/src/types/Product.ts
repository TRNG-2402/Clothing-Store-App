export interface Product
{
    productId: number,
    name: string,
    categoryId: number,
    description: string | null,
    price: number,
    stockQuantity: number
}