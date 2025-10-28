import type { APIResponse } from './../common/APIResponse';
import type { CategoryFilterRequest, CategoryRequest, CategoryResponse } from "@/models/Category";
import api from "./api";

export default {
    getCategories: async (): Promise<CategoryResponse[]> => {
        const response = await api.get<APIResponse<CategoryResponse[]>>("/categories");
        console.log(response.data);
        return response.data;
    },
    getCategoriesWithDetails: async (): Promise<CategoryResponse[]> => {
        const response = await api.get<APIResponse<CategoryResponse[]>>("/categories/with-details");
        return response.data;
    },
    getCategoryById: async (id: number): Promise<CategoryResponse> => {
        const response = await api.get<APIResponse<CategoryResponse>>(`/categories/${id}/with-details`);
        return response.data;
    },
    addCategory: async (request: CategoryRequest): Promise<CategoryResponse> => {
        const response = await api.post<APIResponse<CategoryResponse>>("/categories", request);
        return response.data;
    },
    updateCategory: async (id: number, request: CategoryRequest): Promise<CategoryResponse> => {
        const response = await api.put<APIResponse<CategoryResponse>>(`/categories/${id}`, request);
        return response.data;
    },
    deleteCategory: async (id: number): Promise<void> => {
        await api.delete(`/categories/${id}`);
    },
    filterCategories: async (request: CategoryFilterRequest): Promise<CategoryResponse[]> => {
        const response = await api.get<APIResponse<CategoryResponse[]>>(`/categories/filter`, {
            params:  request 
        });
        console.log(response.data);
        return response.data;
    },
    getCategoryChildren: async (): Promise<CategoryResponse[]> => {
        const response = await api.get<APIResponse<CategoryResponse[]>>("/categories/children");
        return response.data;
    }

};
