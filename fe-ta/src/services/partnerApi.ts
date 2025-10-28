import type { APIResponse } from "../common/APIResponse";
import type {
  PartnerFilterRequest,
  PartnerRequest,
  PartnerResponse,
} from "@/models/Partner";
import api from "./api";

function toFormData(request: PartnerRequest): FormData {
  const formData = new FormData();

  formData.append("name", request.name);
  if (request.logoFile) formData.append("logoFile", request.logoFile);
  if (request.imgDefaultFile) formData.append("imgDefaultFile", request.imgDefaultFile);
  if (request.imgHoverFile) formData.append("imgHoverFile", request.imgHoverFile);
  if (request.slug) formData.append("slug", request.slug);
  if (request.link) formData.append("link", request.link);
  if (request.orderIndex !== undefined)
    formData.append("orderIndex", request.orderIndex.toString());
  if (request.isActive !== undefined)
    formData.append("isActive", request.isActive.toString());
  if (request.note) formData.append("note", request.note);

  return formData;
}

export default {
  createPartner: async (request: PartnerRequest): Promise<PartnerResponse> => {
    const formData = toFormData(request);
    const response = await api.post<APIResponse<PartnerResponse>>(
      "/partners",
      formData,
      {
        headers: { "Content-Type": "multipart/form-data" },
      }
    );
    return response.data;
  },

  getPartners: async (): Promise<PartnerResponse[]> => {
    const response = await api.get<APIResponse<PartnerResponse[]>>("/partners");
    return response.data;
  },
  getPartnerById: async (id: number): Promise<PartnerResponse> => {
    const response = await api.get<APIResponse<PartnerResponse>>(
      `/partners/${id}`
    );
    return response.data;
  },

  updatePartner: async (
    id: number,
    request: PartnerRequest
  ): Promise<PartnerResponse> => {
    const formData = toFormData(request);
    const response = await api.put<APIResponse<PartnerResponse>>(
      `/partners/${id}`,
      formData,
      {
        headers: { "Content-Type": "multipart/form-data" },
      }
    );
    console.log("Update partner response:", response.data);
    return response.data;
  },

  deletePartner: async (id: number): Promise<void> => {
    await api.delete(`/partners/${id}`);
  },

  filterPartners: async (
    request: PartnerFilterRequest
  ): Promise<PartnerResponse[]> => {
    const response = await api.post<APIResponse<PartnerResponse[]>>(
      "/partners/filter",
      request
    );
    return response.data;
  },
};
