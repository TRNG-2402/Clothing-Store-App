import { api } from "./api";
import type { UserProfile } from "../types/User";

export const userService = {
    async getMe(): Promise<UserProfile>
    {
        const response = await api.get<UserProfile>('/users/me');
        return response.data;
    }
};