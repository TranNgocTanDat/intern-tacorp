import type {
  PartnerFilterRequest,
  PartnerRequest,
  PartnerResponse,
} from "@/models/Partner";
import partnerApi from "@/services/partnerApi";
import { defineStore } from "pinia";

export const usePartnerStore = defineStore("partner", {
  state: () => ({
    partners: [] as PartnerResponse[],
    selectedPartner: null as PartnerResponse | null,
    loading: false,
    error: null as string | null,
  }),

  actions: {
    async fetchPartners() {
      this.loading = true;
      this.error = null;
      try {
        const data = await partnerApi.getPartners();
        this.partners = data;
      } catch (err: any) {
        this.error = err.message || "Lỗi khi lấy danh sách đối tác";
      } finally {
        this.loading = false;
      }
    },

    async fetchPartnerById(id: number) {
      this.loading = true;
      this.error = null;
      try {
        const data = await partnerApi.getPartnerById(id);
        this.selectedPartner = data;
        return data;
      } catch (err: any) {
        this.error = err.message || "Lỗi khi lấy thông tin đối tác";
      } finally {
        this.loading = false;
      }
    },
    async createPartner(request: PartnerRequest) {
      this.loading = true;
      this.error = null;
      try {
        const newPartner = await partnerApi.createPartner(request);
        this.partners.push(newPartner);
        this.selectedPartner = newPartner; // 👈 Gán luôn nếu muốn chọn sau khi tạo
        return newPartner;
      } catch (err: any) {
        this.error = err.message || "Lỗi khi tạo đối tác";
        throw err;
      } finally {
        this.loading = false;
      }
    },

    async updatePartner(id: number, request: PartnerRequest) {
      this.loading = true;
      this.error = null;
      try {
        const updatedPartner = await partnerApi.updatePartner(id, request);
        const index = this.partners.findIndex((p) => p.id === id);
        if (index !== -1) {
          this.partners[index] = updatedPartner;
          this.selectedPartner = updatedPartner; // 👈 Cập nhật luôn nếu muốn chọn sau khi sửa
        }
        return updatedPartner;
      } catch (err: any) {
        this.error = err.message || "Lỗi khi cập nhật đối tác";
        throw err;
      } finally {
        this.loading = false;
      }
    },

    async deletePartner(id: number) {
      this.loading = true;
      this.error = null;
      try {
        await partnerApi.deletePartner(id);
        this.partners = this.partners.filter((p) => p.id !== id);
        if (this.selectedPartner?.id === id) {
          this.selectedPartner = null; // 👈 Bỏ chọn nếu đối tác bị xóa
        }
      } catch (err: any) {
        this.error = err.message || "Lỗi khi xóa đối tác";
        throw err;
      } finally {
        this.loading = false;
      }
    },

    async filterPartners(request: PartnerFilterRequest) {
      this.loading = true;
      this.error = null;
      try {
        const data = await partnerApi.filterPartners(request);
        this.partners = data;
      } catch (err: any) {
        this.error = err.message || "Lỗi khi lọc danh sách đối tác";
      } finally {
        this.loading = false;
      }
    },

    setSelectedPartner(partner: PartnerResponse | null) {
      this.selectedPartner = partner;
    },
  },
});
