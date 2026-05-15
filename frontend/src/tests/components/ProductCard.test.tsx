import { describe, it, expect } from "vitest"
import { render, screen } from '@testing-library/react'
import { MemoryRouter } from "react-router-dom"
import ProductCard from "../../components/ProductCard"
import type { Product } from "../../types/Product"

const mProduct: Product = {
    id: 1,
    name: 'thisisaname',
    categoryId: 2,
    description: 'thisisadescription',
    price: 3.00,
    finalPrice: 4.00,
    hasActiveSale: false,
    discountPercentage: undefined,
    stockQuantity: 5
}

describe('<ProductCard />', () =>
{
    it('renders the name, price and stock of the product', () =>
    {
        render(
            <MemoryRouter>
                <ProductCard product={mProduct} />
            </MemoryRouter>
        )

        expect(screen.getByText('thisisaname')).toBeInTheDocument();
        expect(screen.getByText('$3.00')).toBeInTheDocument();
        expect(screen.getByText('Stock: 5')).toBeInTheDocument();
    })
})