// src/store/heroSectionStore.ts
import { defineStore } from "pinia";
import type {
  HeroSectionRequest,
  HeroSectionResponse,
  HeroSectionFilterRequest,
} from "@/models/HeroSection";
import heroSectionApi from "@/services/heroSectionApi";

export const useHeroSectionStore = defineStore("heroSection", {
  state: () => ({
    sections: [] as HeroSectionResponse[],
    selectedSection: null as HeroSectionResponse | null,
    loading: false,
  }),

  actions: {
    /** Load tất cả hero sections */
    async loadAll() {
      this.loading = true;
      try {
        this.sections = await heroSectionApi.getAllHeroSections();
      } catch (err) {
        console.error("Error loading hero sections:", err);
      } finally {
        this.loading = false;
      }
    },

    /** Lấy hero sections theo pageHero */
    async loadByPageHero(pageHero: string) {
      this.loading = true;
      try {
        const data = await heroSectionApi.getHeroSectionByPageHero(pageHero);
        this.sections = data;
      } catch (err) {
        console.error("Error loading hero sections by pageHero:", err);
      } finally {
        this.loading = false;
      }
    },

    /** Tạo mới hero section và thêm vào state */
    async create(request: HeroSectionRequest) {
      this.loading = true;
      try {
        const newSections = await heroSectionApi.createHeroSection(request);
        // Thêm vào state (API trả về mảng)
        this.sections.push(newSections);
        return newSections;
      } catch (err) {
        console.error("Error creating hero section:", err);
        throw err;
      } finally {
        this.loading = false;
      }
    },

    /** Cập nhật hero section */
    async update(id: number, request: HeroSectionRequest) {
      this.loading = true;
      try {
        const updated = await heroSectionApi.updateHeroSection(id, request);
        const index = this.sections.findIndex((s) => s.id === id);
        if (index !== -1) this.sections[index] = updated;
        if (this.selectedSection?.id === id) this.selectedSection = updated;
        return updated;
      } catch (err) {
        console.error("Error updating hero section:", err);
        throw err;
      } finally {
        this.loading = false;
      }
    },

    /** Xoá hero section */
    async delete(id: number) {
      this.loading = true;
      try {
        await heroSectionApi.deleteHeroSection(id);
        this.sections = this.sections.filter((s) => s.id !== id);
        if (this.selectedSection?.id === id) this.selectedSection = null;
      } catch (err) {
        console.error("Error deleting hero section:", err);
        throw err;
      } finally {
        this.loading = false;
      }
    },

    /** Filter hero sections */
    async filter(request: HeroSectionFilterRequest) {
      this.loading = true;
      try {
        const data = await heroSectionApi.filterHeroSections(request);
        this.sections = data;
        return data;
      } catch (err) {
        console.error("Error filtering hero sections:", err);
        return [];
      } finally {
        this.loading = false;
      }
    },

    /** Lấy một section và lưu vào selectedSection */
    selectSection(section: HeroSectionResponse) {
      this.selectedSection = section;
    },

    /** Reset selected section */
    clearSelection() {
      this.selectedSection = null;
    },
  },
});
