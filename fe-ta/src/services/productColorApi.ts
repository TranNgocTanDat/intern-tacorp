import type {
  ProductColorFilterRequest,
  ProductColorRequest,
  ProductColorResponse,
} from "@/models/ProductColor";
import api from "./api";
import type { APIResponse } from "@/common/APIResponse";

export default {
  createProductColor: async (
    request: ProductColorRequest
  ): Promise<ProductColorResponse> => {
    const response = await api.post<APIResponse<ProductColorResponse>>(
      "/product-color",
      request
    );
    return response.data;
  },
  getAllProductColors: async (): Promise<ProductColorResponse[]> => {
    const response = await api.get<APIResponse<ProductColorResponse[]>>(
      "/product-color"
    );
    return response.data;
  },
  getProductColorByProductId: async (
    productId: number
  ): Promise<ProductColorResponse[]> => {
    const response = await api.get<APIResponse<ProductColorResponse[]>>(
      `/product-color/product/${productId}`
    );
    return response.data;
  },
  getProductColorByProductIdAndColorId: async (
    productId: number,
    colorId: number
  ): Promise<ProductColorResponse[]> => {
    const response = await api.get<APIResponse<ProductColorResponse[]>>(
      `/product-color/${colorId}/product/${productId}`
    );
    return response.data;
  },
  updateProductColor: async (
    id: number,
    request: ProductColorRequest
  ): Promise<ProductColorResponse> => {
    const response = await api.put<APIResponse<ProductColorResponse>>(
      `/product-color/${id}`,
      request
    );
    return response.data;
  },
  deleteProductColor: async (id: number): Promise<void> => {
    await api.delete(`/product-color/${id}`);
  },
  filterProductColors: async (
    request: ProductColorFilterRequest
  ): Promise<ProductColorResponse[]> => {
    const response = await api.get<APIResponse<ProductColorResponse[]>>(
      "/product-color/filter",
      { params: request }
    );
    return response.data;
  },
};
