import { describe, it, expect, vi, beforeEach, afterEach } from "vitest"
import { render, screen } from '@testing-library/react'
import { MemoryRouter, Route, Routes } from "react-router-dom"
import ProductDetails from "../../pages/ProductDetails"

beforeEach(() =>
{
    vi.stubGlobal('fetch', vi.fn().mockResolvedValue({
        ok: true,
        json: async () => ({
            id: 1,
            name: "Laptop",
            categoryId: 2,
            description: "description",
            price: 500.00,
            finalPrice: 500,
            hasActiveSale: false,
            stockQuantity: 5
        })
    }))
})
afterEach(() =>
{
    vi.restoreAllMocks();
})



describe('<ProductDetails />', () => 
{

    it('shows the proper product based on the path', async () =>
    {




        await render(
            <MemoryRouter initialEntries={["/1"]}>
                <Routes>
                    <Route path=":id" element={<ProductDetails />} />
                </Routes>
            </MemoryRouter>
        )


        expect(await screen.findByText('Laptop')).toBeInTheDocument();
        expect(await screen.findByText('$500.00')).toBeInTheDocument();
        expect(screen.getByText('Add To Cart')).toBeInTheDocument();
        expect(screen.getByText('Add To Cart')).toHaveRole('button');

    })

    it('shows "Product not found" when there are no products to display', () =>
    {
        render(
            <MemoryRouter initialEntries={["/2"]}>
                <Routes>
                    <Route path=":id" element={<ProductDetails />} />
                </Routes>
            </MemoryRouter>
        )

        expect(screen.getByText('Product not found')).toBeInTheDocument();
    })


})