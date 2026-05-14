import { describe, it, expect } from "vitest"
import { render, screen } from '@testing-library/react'
import { createPath, MemoryRouter, Route, Routes } from "react-router-dom"
import ProductDetails from "../../pages/ProductDetails"




describe('<ProductDetails />', () =>
{
    // these will need to change when fakeProducts from ProductDetails.tsx is removed.
    // for now, these tests will be functioning on the assumption that fakeProducts exists
    it('shows the proper product based on the path', () =>
    {
        createPath
        render(
            <MemoryRouter initialEntries={["/1"]}>
                <Routes>
                    <Route path=":id" element={<ProductDetails />} />
                </Routes>
            </MemoryRouter>
        )

        expect(screen.getByText('Laptop')).toBeInTheDocument();
        expect(screen.getByText('$500.00')).toBeInTheDocument();
        expect(screen.getByText('Add To Bag')).toBeInTheDocument();
        expect(screen.getByText('Add To Bag')).toHaveRole('button');
    })

    it('shows "Product not found" when there are no products to display', () =>
    {
        render(
            <ProductDetails />
        )

        expect(screen.getByText('Product not found')).toBeInTheDocument();
    })


})