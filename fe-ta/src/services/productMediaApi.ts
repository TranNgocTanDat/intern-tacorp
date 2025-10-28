import type {
  ProductMediaFilterRequest,
  ProductMediaRequest,
  ProductMediaResponse,
} from "@/models/ProductMedia";
import api from "./api";
import type { APIResponse } from "@/common/APIResponse";

export default {
  getAllProductMedia: async (): Promise<ProductMediaResponse[]> => {
    const response = await api.get<APIResponse<ProductMediaResponse[]>>(
      "/media"
    );
    console.log(response.data);
    return response.data;
  },
  createProductMedia: async (
    productId: number,
    request: ProductMediaRequest
  ): Promise<ProductMediaResponse> => {
    const formData = new FormData();
    formData.append("mediaFileUrl", request.mediaFileUrl || "");
    if (request.colorId !== undefined && request.colorId !== null) {
      formData.append("colorId", String(request.colorId));
    }
    formData.append("mediaType", request.mediaType || "");
    formData.append("descriptionMedia", request.descriptionMedia || "");
    formData.append("isPrimary", String(request.isPrimary || false));
    formData.append("orderIndex", String(request.orderIndex || 0));
    if (request.note !== undefined) {
      formData.append("note", request.note);
    }
    const response = await api.post<APIResponse<ProductMediaResponse>>(
      `/media/product/${productId}`,
      formData
    );
    return response.data;
  },
  updateProductMedia: async (
    mediaId: number,
    productId: number,
    request: ProductMediaRequest
  ): Promise<ProductMediaResponse> => {
    const formData = new FormData();
    formData.append("mediaFileUrl", request.mediaFileUrl || "");
    if (request.colorId !== undefined && request.colorId !== null) {
      formData.append("colorId", String(request.colorId));
    }
    formData.append("mediaType", request.mediaType || "");
    formData.append("descriptionMedia", request.descriptionMedia || "");
    formData.append("isPrimary", String(request.isPrimary || false));
    formData.append("orderIndex", String(request.orderIndex || 0));
    if (request.note !== undefined) {
      formData.append("note", request.note);
    }
    const response = await api.put<APIResponse<ProductMediaResponse>>(
      `/media/${mediaId}/product/${productId}`,
      formData
    );
    console.log("update success", response.data);
    return response.data;
  },
  deleteProductMedia: async (mediaId: number): Promise<void> => {
    await api.delete(`/media/${mediaId}`);
  },
  filterProductMedia: async (
    request: ProductMediaFilterRequest
  ): Promise<ProductMediaResponse[]> => {
    const response = await api.get<APIResponse<ProductMediaResponse[]>>(
      `/media/filter`,
      { params: request }
    );
    return response.data;
  },
};
