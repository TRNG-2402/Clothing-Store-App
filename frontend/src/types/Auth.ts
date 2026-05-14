export interface LoginRequest
{
    email: string;
    password: string;
}

export type UserRole = 'User' | 'Admin';

export interface LoginResponse
{
    id: number;
    token: string;
    email: string;
    role: UserRole;
}

export type AuthUser = Omit<LoginResponse, 'token'>;

export interface CreateUserRequest
{
    name: string;
    email: string;
    password: string;
}

export interface UserResponse // Response for registering a new user
{
    id: number;
    email: string;
    name?: string;
}