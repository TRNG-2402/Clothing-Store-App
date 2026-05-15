import { api } from "./api";

export const orderService = {

    async getMyOrders() {
        const res = await api.get("/Orders/my-orders");
        return res.data;
    },

    async getOrderById(id: number) {
        const res = await api.get(`/Orders/${id}`);
        return res.data;
    },

    async cancelOrder(orderId: number) {
        return api.patch(`/Orders/${orderId}`, {
            status: 4
        });
    }

};