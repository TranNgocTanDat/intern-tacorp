import type {
  ProductFilterRequest,
  ProductRequest,
  ProductResponse,
} from "@/models/Product";
import api from "./api";
import type { APIResponse } from "@/common/APIResponse";

export default {
  getAllProducts: async (): Promise<ProductResponse[]> => {
    const response = await api.get<APIResponse<ProductResponse[]>>(
      "/product/featured"
    );
    return response.data;
  },
  createProduct: async (request: ProductRequest): Promise<ProductResponse> => {
    const response = await api.post<APIResponse<ProductResponse>>(
      "/product",
      request
    );
    console.log("response create", response);
    return response.data;
  },
  updateProduct: async (
    id: number,
    request: ProductRequest
  ): Promise<ProductResponse> => {
    const response = await api.put<APIResponse<ProductResponse>>(
      `/product/${id}`,
      request
    );
    return response.data;
  },
  deleteProduct: async (id: number): Promise<void> => {
    await api.delete(`/product/${id}`);
  },
  getProductById: async (id: number): Promise<ProductResponse> => {
    const response = await api.get<APIResponse<ProductResponse>>(
      `/product/${id}`
    );
    return response.data;
  },
  getProductBySlug: async (slug: string): Promise<ProductResponse> => {
    const response = await api.get<APIResponse<ProductResponse>>(
      `/product/slug/${slug}`
    );
    return response.data;
  },
  filterProducts: async (
    request: ProductFilterRequest
  ): Promise<ProductResponse[]> => {
    const response = await api.get<APIResponse<ProductResponse[]>>(
      "/product/filter",
      { params: request }
    );
    return response.data;
  },
};
