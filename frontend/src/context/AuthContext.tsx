import { createContext, useContext, useState, useEffect } from "react";
import type { LoginResponse, AuthUser } from "../types/Auth";

interface AuthContextType
{
    token: string | null;
    user: AuthUser | null;
    login: (data: LoginResponse) => void;
    logout: () => void;
    isAuthenticated: boolean;
}

const AuthContext = createContext<AuthContextType | null>(null);

export const AuthProvider = ({ children }: { children: React.ReactNode }) =>
{
    const [token, setToken] = useState<string | null>(null);
    const [user, setUser] = useState<AuthUser | null>(null);

    // restore session on refresh
    useEffect(() =>
    {
        const storedToken = localStorage.getItem("token");
        const storedUser = localStorage.getItem("user");

        if (storedToken && storedUser)
        {
            setToken(storedToken);
            setUser(JSON.parse(storedUser));
        }
    }, []);

    const login = (data: LoginResponse) =>
    {
        const { token, ...user } = data;

        setToken(token);
        setUser(user);

        localStorage.setItem("token", token);
        localStorage.setItem("user", JSON.stringify(user));
    };

    const logout = () =>
    {
        setToken(null);
        setUser(null);

        localStorage.removeItem("token");
        localStorage.removeItem("user");
    };

    return (
        <AuthContext.Provider
            value={{
                token,
                user,
                login,
                logout,
                isAuthenticated: !!token,
            }}
        >
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () =>
{
    const context = useContext(AuthContext);

    if (!context)
    {
        throw new Error("useAuth must be used within AuthProvider");
    }

    return context;
};