export interface AdminUserRequest {
  username: string;
  password: string; 
  fullName?: string;
  email?: string;
  phone?: string;
  isActive?: boolean;
}

export interface AdminUserResponse {
  id: number;
  username: string;
  fullName?: string;
  email?: string;
  phone?: string;
  isActive: boolean;
  role: string;
}

export interface AdminUserSearchRequest {
  username?: string;
  fullName?: string;
  email?: string;
  phone?: string;
  isActive?: boolean;
}