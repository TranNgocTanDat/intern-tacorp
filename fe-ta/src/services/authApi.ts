import type { APIResponse } from "@/common/APIResponse";
import type { LoginRequest, LoginResponse } from "@/models/Authentication";
import api from "./api";

export default {
    login: async (request: LoginRequest): Promise<LoginResponse> => {
        const response = await api.post<APIResponse<LoginResponse>>(
            "/auth/login",
            request
        );
        console.log(response);
        return response.data;
    }
}