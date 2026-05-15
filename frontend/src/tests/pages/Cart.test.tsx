import { describe, it, expect, beforeEach } from "vitest"
import { act, render, screen } from '@testing-library/react'
import { MemoryRouter } from "react-router-dom"
import CartPage from "../../pages/Cart"

describe('<CartPage/>', () =>
{
    // again, tests need to change when test items removed from Cart.tsx

    it('renders the entire cart page with multiple elements', () => 
    {
        const cartItems = [
            { id: 1, name: "Item A", quantity: 1, price: 12.00, stockQuantity: 5 },
            { id: 2, name: "Item B", quantity: 2, price: 8.50, stockQuantity: 10 }
        ];

        var quantity = 0;
        var price = 0;
        cartItems.forEach(item =>
        {
            quantity += item.quantity;
            price += item.quantity * item.price;
        });

        render(
            <MemoryRouter>
                <CartPage />
            </MemoryRouter>
        )


        //navbar elements here

        //item elements
        expect(screen.getByText('Shopping Bag')).toBeInTheDocument();

        expect(screen.getByText(`In Bag (${cartItems.length})`)).toBeInTheDocument();

        //assuming one element has been added
        for (var i = 0; i < cartItems.length; i++)
        {
            expect(screen.getByText(`${cartItems[i].name}`)).toBeInTheDocument();
            expect(screen.getByText(`$${cartItems[i].price.toFixed(2)}`)).toBeInTheDocument();
            expect(screen.getAllByText('Delete')[i]).toBeInTheDocument();
            expect(screen.getAllByText('Delete')[i]).toHaveRole('button');

            expect(screen.getAllByRole('combobox')[i]).toBeInTheDocument();

            for (var j = 1; j < cartItems[i].stockQuantity; j++)
            {
                expect(screen.getAllByRole('combobox')[i].childElementCount === cartItems[i].stockQuantity).toBeTruthy()
            }
        }

        //summary elements
        expect(screen.getByText('Order Summary')).toBeInTheDocument();
        expect(screen.getByText('Items')).toBeInTheDocument();
        expect(screen.getByText('Items').nextElementSibling).toHaveTextContent(`${quantity}`);

        expect(screen.getByText('Subtotal')).toBeInTheDocument();
        expect(screen.getByText('Subtotal').nextElementSibling?.childNodes.item(0)).toHaveTextContent(`$${price.toFixed(2)}`);

        expect(screen.getByText('Checkout')).toBeInTheDocument();
        expect(screen.getByText('Checkout')).toHaveRole('button');
    })

    it('deletes an item after a Delete button press and then updates the total items and price as well as removing the item from the dom', () =>
    {
        const cartItems = [
            { id: 1, name: "Item A", quantity: 1, price: 12.00, stockQuantity: 5 },
            { id: 2, name: "Item B", quantity: 2, price: 8.50, stockQuantity: 10 }
        ];

        var oldTotal = 0;
        var oldPrice = 0
        var priceOfDeletedItem = 0
        cartItems.forEach(item =>
        {
            oldTotal += item.quantity;
            oldPrice += item.quantity * item.price;
            if (item.id === 2)
            {
                priceOfDeletedItem = item.quantity * item.price;
            }
        });


        render(
            <MemoryRouter>
                <CartPage />
            </MemoryRouter>
        )

        //checking that price, item is correct
        var deleteButton = screen.getAllByRole('button')[0];
        var quantity = (screen.getAllByRole('combobox')[0] as HTMLSelectElement).value;

        expect(quantity === `${cartItems[0].quantity}`).toBeTruthy();
        expect(screen.getByText('Items').nextElementSibling).toHaveTextContent(`${oldTotal}`);
        expect(screen.getByText('Subtotal').nextElementSibling).toHaveTextContent(`${oldPrice}`);

        act(() =>
        {
            deleteButton.click();
        })

        //checking that price, item changed
        var newTotal = screen.getByText('Items').nextElementSibling?.textContent
        var newPrice = screen.getByText('Subtotal').nextElementSibling?.textContent

        expect(newTotal === `${oldTotal - Number.parseInt(quantity)}`).toBeTruthy();
        expect(newPrice === `${oldPrice - priceOfDeletedItem}`);

        //check that item has disappeared from user perspective
        expect(deleteButton.checkVisibility).toBeFalsy();

    })

    /*it('goes to checkout', () => {

    })*/
})
