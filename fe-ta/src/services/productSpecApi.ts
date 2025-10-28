import type {
  ProductSpecFilterRequest,
  ProductSpecRequest,
  ProductSpecResponse,
} from "@/models/ProductSpec";
import api from "./api";
import type { APIResponse } from "@/common/APIResponse";

export default {
  getProductSpecs: async (): Promise<ProductSpecResponse[]> => {
    const response = await api.get<APIResponse<ProductSpecResponse[]>>(`/spec`);
    return response.data;
  },
  getProductSpecByProductId: async (
    productId: number
  ): Promise<ProductSpecResponse[]> => {
    const response = await api.get<APIResponse<ProductSpecResponse[]>>(
      `/spec/product/${productId}`
    );
    return response.data;
  },
  createProductSpec: async (
    productId: number,
    request: ProductSpecRequest
  ): Promise<ProductSpecResponse> => {
    const response = await api.post<APIResponse<ProductSpecResponse>>(
      `/spec/product/${productId}`,
      request
    );
    return response.data;
  },
  updateProductSpec: async (
    productId: number,
    specId: number,
    request: ProductSpecRequest
  ): Promise<ProductSpecResponse> => {
    const response = await api.put<APIResponse<ProductSpecResponse>>(
      `/spec/${specId}/product/${productId}`,
      request
    );
    console.log("Update response:", response.data);
    return response.data;
  },
  deleteProductSpec: async (specId: number): Promise<void> => {
    await api.delete(`/spec/${specId}`);
  },
  filterProductSpecs: async (
    request: ProductSpecFilterRequest
  ): Promise<ProductSpecResponse[]> => {
    const response = await api.get<APIResponse<ProductSpecResponse[]>>(
      `/spec/filter`,
      { params: request }
    );
    return response.data;
  },
};
