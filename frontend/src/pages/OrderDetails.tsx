import { useEffect, useState } from "react";
import { useParams, useNavigate  } from "react-router-dom";
import { orderService } from "../services/orderService";
import NavBar from '../components/NavBar'
import styles from "./OrderDetails.module.css";

type Order = {
  id: number;
  status: string;
  createdAt: string;
  shippingAddress: string;
  total: number;
  items: {
    productId: number;
    productName: string;
    quantity: number;
    unitPrice: number;
  }[];
};

export default function OrderDetails() {
  const navigate = useNavigate();
  const { orderId } = useParams();
  const [order, setOrder] = useState<Order | null>(null);

  const handleCancel = async () => {
    if (!order) return;

    const confirmed = window.confirm(
      "Are you sure you want to cancel this order?"
    );

    if (!confirmed) return;

    try {
      await orderService.cancelOrder(order.id);

      setOrder({
        ...order,
        status: "Cancelled"
      });
    } catch (err) {
      console.error("Failed to cancel order", err);
    }
  };

  useEffect(() => {
    if (orderId) loadOrder(parseInt(orderId));
  }, [orderId]);

  const loadOrder = async (orderId: number) => {
    try {
      const data = await orderService.getOrderById(orderId);
      setOrder(data);
    } catch (err) {
      console.error(err);
    }
  };
  

  if (!order) return <p>Loading...</p>;



  return (
    <div className={styles.container}>
      <NavBar />
      <h2 className={styles.title}>Order #{order.id}</h2>
      <div className={styles.card}>
        <p className={styles.meta}>
          <span className={styles.status}>{order.status}</span>
        </p>

        <p className={styles.meta}>
          Placed on {new Date(order.createdAt).toLocaleString()}
        </p>

        <p className={styles.meta}>
          Shipping Address: {order.shippingAddress}
        </p>

        <div className={styles.section}>
          {order.items.map(item => (
            <div
              key={item.productId}
              className={styles.item}
            >
              <span>
                {item.productName} × {item.quantity}
              </span>

              <span>
                ${(item.unitPrice * item.quantity).toFixed(2)}
              </span>
            </div>
          ))}
        </div>


        <p className={styles.total}>
          Total: ${order.total.toFixed(2)}
        </p>

        {order.status === "Pending" && (
          <button
            onClick={handleCancel}
            style={{
              marginTop: "16px",
              padding: "10px 14px",
              backgroundColor: "#e74c3c",
              color: "white",
              border: "none",
              borderRadius: "6px",
              cursor: "pointer"
            }}
          >
            Cancel Order
          </button>
        )}

      </div>
      <button className={styles.back} onClick={() => navigate("/profile/orders")}> Back to Orders </button>
    </div>
  );
}