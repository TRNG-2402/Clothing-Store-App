import React, { useEffect, useState } from "react";
import styles from "./Checkout.module.css";
import NavBar from "../components/NavBar";
import { useNavigate } from "react-router-dom";

type CartItem = {
    id: number;
    name: string;
    quantity: number;
    price: number;
    stockQuantity: number;
};

export default function CheckoutPage()
{
    const API = import.meta.env.VITE_API_BASE_URL;

    const [cartItems, setCartItems] = useState<CartItem[]>([]);

    const [, setOrderSuccess] = useState(false);

    const [form, setForm] = useState({
        name: "",
        address: ""
    });

    // -------------------------
    // GET REAL CART (FROM YOUR CONTROLLER)
    // -------------------------
    useEffect(() =>
    {
        const fetchCart = async () =>
        {
            const res = await fetch(`${API}/cart`, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem("token")}`
                }
            });

            const data = await res.json();
            console.log("CHECKOUT CART:", data);

            setCartItems(data);
        };

        fetchCart();
    }, []);

    // -------------------------
    // TOTALS (REAL DATA)
    // -------------------------
    const totalItems = cartItems.reduce(
        (sum, item) => sum + item.quantity,
        0
    );

    const total = cartItems.reduce(
        (sum, item) => sum + item.price * item.quantity,
        0
    );

    // -------------------------
    // FORM HANDLER
    // -------------------------
    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) =>
    {
        setForm({
            ...form,
            [e.target.name]: e.target.value
        });
    };

    // -------------------------
    // SUBMIT ORDER (placeholder for now)
    // -------------------------

    const navigate = useNavigate();

    const handleSubmit = async (e: React.FormEvent) =>
    {
        e.preventDefault();

        const payload = {
            shippingAddress: form.address,
            items: cartItems.map(item => ({
                productId: item.id,
                quantity: item.quantity,
                finalPrice: item.price.toFixed(2)
            }))
        };

        try
        {
            const response = await fetch(`${API}/orders/create`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${localStorage.getItem("token")}`
                },
                body: JSON.stringify(payload)
            });

            if (!response.ok) throw new Error("Order failed");

            // ✅ STEP 1: clear cart AFTER successful order
            await fetch(`${API}/cart`, {
                method: "DELETE",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${localStorage.getItem("token") ?? ""}`
                }
            });
            //console.log(localStorage.getItem("token"));

            // ✅ STEP 2: show success message
            setOrderSuccess(true);

            setTimeout(() =>
            {
                setOrderSuccess(false);
                navigate("/cart");
            }, 3000);

        } catch (error)
        {
            console.error(error);
        }
    };


    return (
        <div>
            <NavBar />

            <div className={styles.container}>
                <h1>Checkout</h1>

                {/* COLLAPSIBLE CART (REAL DATA) */}
                <details className={styles.dropdown}>
                    <summary className={styles.summary}>
                        View Order Items ({totalItems})
                    </summary>

                    <div className={styles.cartBox}>
                        {cartItems.map(item => (
                            <div key={item.id} className={styles.cartItem}>
                                <div className={styles.name}>{item.name}</div>

                                <div className={styles.qty}>Qty: {item.quantity}</div>

                                <div className={styles.price}>${item.price.toFixed(2)}</div>
                            </div>
                        ))}

                        <hr />

                        <p className={styles.total}>
                            Total: ${total.toFixed(2)}
                        </p>
                    </div>
                </details>

                {/* FORM */}
                <form onSubmit={handleSubmit} className={styles.form}>
                    <input
                        name="name"
                        placeholder="Full Name"
                        value={form.name}
                        onChange={handleChange}
                    />


                    <input
                        name="address"
                        placeholder="Shipping Address"
                        value={form.address}
                        onChange={handleChange}
                    />

                    <button type="submit" className={styles.submitBtn}>
                        Submit Order
                    </button>
                </form>
            </div>
        </div>
    );
}