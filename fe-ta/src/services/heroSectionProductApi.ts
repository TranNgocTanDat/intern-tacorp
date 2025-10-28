import type { APIResponse } from "@/common/APIResponse";
import type { HeroSectionProductFilterRequest, HeroSectionProductRequest, HeroSectionProductResponse } from "@/models/HeroSectionProduct";
import api from "./api";

export default {
    createHeroSectionProduct: async (request: HeroSectionProductRequest): Promise<HeroSectionProductResponse> => {
        const response = await api.post<APIResponse<HeroSectionProductResponse>>("/hero-section-products", request);
        console.log("Create response:", response.data);
        return response.data;
    },
    getAllHeroSectionProducts: async (): Promise<HeroSectionProductResponse[]> => {
        const response = await api.get<APIResponse<HeroSectionProductResponse[]>>("/hero-section-products");
        return response.data;
    },
    deleteHeroSectionProduct: async (id: number): Promise<void> => {
        await api.delete(`/hero-section-products/${id}`);
    },
    updateHeroSectionProduct: async (id: number, request: HeroSectionProductRequest): Promise<HeroSectionProductResponse> => {
        const response = await api.put<APIResponse<HeroSectionProductResponse>>(`/hero-section-products/${id}`, request);
        console.log("Update response:", response.data);
        return response.data;
    },
    filterHeroSectionProducts: async (request: HeroSectionProductFilterRequest): Promise<HeroSectionProductResponse[]> => {
        const response = await api.get<APIResponse<HeroSectionProductResponse[]>>("/hero-section-products/filter", { params: request });
        return response.data;
    }
}