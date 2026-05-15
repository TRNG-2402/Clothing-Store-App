import { describe, it, expect, vi, beforeEach, afterEach } from "vitest"
import { render, screen } from '@testing-library/react'
import { MemoryRouter, Route, Routes } from "react-router-dom"
import ProductDetails from "../../pages/ProductDetails"
import { useState } from "react"


afterEach(() =>
{
    vi.restoreAllMocks();
})



describe('<ProductDetails />', () =>
{
    // these will need to change when fakeProducts from ProductDetails.tsx is removed.
    // for now, these tests will be functioning on the assumption that fakeProducts exists
    it('shows the proper product based on the path', async () =>
    {


        vi.stubGlobal('fetch', vi.fn().mockResolvedValue({
            ok: true,
            json: async () => ({
                id: 1,
                name: "Laptop",
                price: 500.00,
                categoryName: "categoryName",
                stockQuantity: 5
            })
        }))

        render(
            <MemoryRouter initialEntries={["/1"]}>
                <Routes>
                    <Route path=":id" element={<ProductDetails />} />
                </Routes>
            </MemoryRouter>
        )


        expect(await screen.findByText('Laptop')).toBeInTheDocument();
        expect(screen.getByText('$500.00')).toBeInTheDocument();
        expect(screen.getByText('Add To Bag')).toBeInTheDocument();
        expect(screen.getByText('Add To Bag')).toHaveRole('button');

    })

    it('shows "Product not found" when there are no products to display', () =>
    {
        render(
            <MemoryRouter initialEntries={["/1"]}>
                <Routes>
                    <Route path=":id" element={<ProductDetails />} />
                </Routes>
            </MemoryRouter>
        )

        expect(screen.getByText('Product not found')).toBeInTheDocument();
    })


})