import { useEffect, useState } from "react";
import { orderService } from "../services/orderService";
import { useNavigate } from "react-router-dom";
import NavBar from "../components/NavBar";
import styles from "./Orders.module.css";

type OrderSummary = {
    id: number;
    createdAt: string;
    status: string;
    total: number;
    items: {
        productId: number;
        productName: string;
        quantity: number;
    }[];
};

export default function Orders() {
    const [orders, setOrders] = useState<OrderSummary[]>([]);
    const navigate = useNavigate();

    useEffect(() => {
        loadOrders();
    }, []);

    const loadOrders = async () => {
        try {
            const data = await orderService.getMyOrders();
            setOrders(data);
        } catch (err) {
            console.error("Failed to load orders", err);
        }
    };

    return (
        <>
            <NavBar />

            <div className={styles.container}>
                <h2 className={styles.title}>My Orders</h2>

                {orders.map(order => (
                    <div
                        key={order.id}
                        className={styles.orderCard}
                        onClick={() =>
                            navigate(`/profile/orders/${order.id}`)
                        }
                    >
                        <div className={styles.orderHeader}>
                            <h3>Order #{order.id}</h3>

                            <p>{order.status}</p>
                        </div>

                        <p>
                            Placed on{" "}
                            {new Date(order.createdAt).toLocaleDateString()}
                        </p>

                        <div className={styles.orderItems}>
                            {order.items.map(item => (
                                <div
                                    key={`${item.productId}-${item.quantity}`}
                                    className={styles.orderItem}
                                >
                                    <p>
                                        {item.productName} × {item.quantity}
                                    </p>
                                </div>
                            ))}
                        </div>

                        <p className={styles.total}>
                            Total: ${order.total.toFixed(2)}
                        </p>
                        
                    </div>
                ))}
                <button className={styles.back} onClick={() => navigate("/profile")}> Back to Profile </button>
            </div>
        </>
    );
}