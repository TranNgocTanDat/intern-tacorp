import api from "./api";
import type { AdminUserRequest, AdminUserResponse, AdminUserSearchRequest } from "@/models/AdminUser";
import type { APIResponse } from "@/common/APIResponse";

export default {
  getAllAdmin: async (): Promise<AdminUserResponse[]> => {
    const response = await api.get<APIResponse<AdminUserResponse[]>>(
      "/admin-users"
    );
    return response.data;
  },
  getAdminById: async (id: number): Promise<AdminUserResponse> => {
    const response = await api.get<APIResponse<AdminUserResponse>>(
      `/admin-users/${id}`
    );
    return response.data;
  },
  createAdmin: async (
    request: AdminUserRequest
  ): Promise<AdminUserResponse> => {
    const response = await api.post<APIResponse<AdminUserResponse>>(
      "/admin-users",
      request
    );
    return response.data;
  },
  updateAdmin: async (
    id: number,
    request: AdminUserRequest
  ): Promise<AdminUserResponse> => {
    const response = await api.put<APIResponse<AdminUserResponse>>(
      `/admin-users/${id}`,
      request
    );
    return response.data;
  },
  deleteAdmin: async (id: number): Promise<void> => {
    console.log("Deleting admin with id:", id);
    await api.delete<void>(`/admin-users/${id}`);
  },
  searchAdminUsers: async (request: AdminUserSearchRequest): Promise<AdminUserResponse[]> => {
    const response = await api.get<APIResponse<AdminUserResponse[]>>(
      "/admin-users/search",
      { params: request }
    );
    console.log(response);
    return response.data;
  }
};
