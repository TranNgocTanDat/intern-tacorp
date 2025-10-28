import type {
  ProductStorageFilterRequest,
  ProductStorageRequest,
  ProductStorageResponse,
} from "@/models/ProductStorage";
import api from "./api";
import type { APIResponse } from "@/common/APIResponse";


export default {
  createProductStorage: async (
    request: ProductStorageRequest
  ): Promise<ProductStorageResponse> => {
    const response = await api.post<APIResponse<ProductStorageResponse>>(
      "/product-storage",
      request
    );
    return response.data;
  },
  updateProductStorage: async (
    id: number,
    request: ProductStorageRequest
  ): Promise<ProductStorageResponse> => {
    const response = await api.put<APIResponse<ProductStorageResponse>>(
      `/product-storage/${id}`,
      request
    );
    return response.data;
  },
  deleteProductStorage: async (id: number): Promise<void> => {
    await api.delete(`/product-storage/${id}`);
  },
  getProductStorageByProductId: async (
    productId: number
  ): Promise<ProductStorageResponse> => {
    const response = await api.get<APIResponse<ProductStorageResponse>>(
      `/product-storage/product/${productId}`
    );
    return response.data;
  },
  getAllProductStorages: async (): Promise<ProductStorageResponse[]> => {
    const response = await api.get<APIResponse<ProductStorageResponse[]>>(
      "/product-storage"
    );
    return response.data;
  },
  FilterProductStorages: async (
    request: ProductStorageFilterRequest
  ): Promise<ProductStorageResponse[]> => {
    const response = await api.post<APIResponse<ProductStorageResponse[]>>(
      "/product-storage/filter",
      request
    );
    return response.data;
  },
};
