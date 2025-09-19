import type { AdminUserResponse } from './AdminUser';
export interface LoginRequest{
    username: string;
    password: string;
}

export interface LoginResponse{
    accessToken: string;
    tokenType: string;
    expiresIn: number;
    adminUserResponse: AdminUserResponse;
}