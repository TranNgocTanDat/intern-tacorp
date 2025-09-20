import type {
  HeroSectionFilterRequest,
  HeroSectionRequest,
  HeroSectionResponse,
} from "@/models/HeroSection";
import api from "./api";
import type { APIResponse } from "@/common/APIResponse";

export default {
  createHeroSection: async (
    request: HeroSectionRequest
  ): Promise<HeroSectionResponse[]> => {
    const formData = new FormData();
    formData.append("Title", request.title || "");
    formData.append("PageHero", request.pageHero || "");
    formData.append("IsPublished", String(request.isPublished));
    if (request.publishFrom) {
      formData.append("PublishFrom", request.publishFrom);
    }
    if (request.publishTo) {
      formData.append("PublishTo", request.publishTo);
    }
    if (request.heroMediaFile) {
      formData.append("HeroMediaFile", request.heroMediaFile);
    }
    const response = await api.post<APIResponse<HeroSectionResponse[]>>(
      "/hero-section",
      formData,
      {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      }
    );
    return response.data;
  },
  getAllHeroSections: async (): Promise<HeroSectionResponse[]> => {
    const response = await api.get<APIResponse<HeroSectionResponse[]>>(
      "/hero-section"
    );
    return response.data;
  },
  getHeroSectionById: async (id: number): Promise<HeroSectionResponse> => {
    const response = await api.get<APIResponse<HeroSectionResponse>>(
      `/hero-section/${id}`
    );
    return response.data;
  },
  updateHeroSection: async (
    id: number,
    request: HeroSectionRequest
  ): Promise<HeroSectionResponse> => {
    const formData = new FormData();
    formData.append("Title", request.title || "");
    formData.append("PageHero", request.pageHero || "");
    formData.append("IsPublished", String(request.isPublished));
    if (request.publishFrom) {
      formData.append("PublishFrom", request.publishFrom);
    }
    if (request.publishTo) {
      formData.append("PublishTo", request.publishTo);
    }
    if (request.heroMediaFile) {
      formData.append("HeroMediaFile", request.heroMediaFile);
    }
    const response = await api.put<APIResponse<HeroSectionResponse>>(
      `/hero-section/${id}`,
      formData,
      {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      }
    );
    console.log("Update response:", response.data.heroMediaUrl);
    return response.data;
  },
  deleteHeroSection: async (id: number): Promise<void> => {
    await api.delete(`/hero-section/${id}`);
  },
  filterHeroSections: async (request: HeroSectionFilterRequest): Promise<HeroSectionResponse[]> => {
    const response = await api.get<APIResponse<HeroSectionResponse[]>>(
      `/hero-section/filter`,
      {
        params: request,
      }
    );
    return response.data;
  }
};
