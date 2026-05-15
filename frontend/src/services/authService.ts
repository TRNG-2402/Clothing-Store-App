import { api } from "./api";
import type { LoginRequest, LoginResponse, CreateUserRequest, UserResponse } from "../types/Auth";


export const authService = {

    async login(creds: LoginRequest): Promise<LoginResponse>
    {
        const response = await api.post<LoginResponse>('/Auth/login', creds);
        return response.data;
    },

    async register(creds: CreateUserRequest): Promise<UserResponse>
    {
        const response = await api.post<UserResponse>('/auth/register', creds);
        return response.data;
    }
}