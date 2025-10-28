// src/store/adminStore.ts
import { defineStore } from "pinia";
import adminApi from "@/services/adminApi";
import type {
  AdminUserRequest,
  AdminUserResponse,
  AdminUserSearchRequest,
} from "@/models/AdminUser";

export const useAdminStore = defineStore("admin", {
  state: () => ({
    admins: [] as AdminUserResponse[],
    loading: false,
    selectedAdmin: null as AdminUserResponse | null,
  }),

  actions: {
    /** Load tất cả admin */
    async loadAdmins() {
      this.loading = true;
      try {
        this.admins = await adminApi.getAllAdmin();
        console.log("Admins loaded:", this.admins);
      } catch (err) {
        console.error("Error loading admins:", err);
      } finally {
        this.loading = false;
      }
    },

    /** Lấy admin theo ID và lưu vào selectedAdmin */
    async getAdminById(id: number) {
      this.loading = true;
      try {
        this.selectedAdmin = await adminApi.getAdminById(id);
      } catch (err) {
        console.error("Error getting admin by id:", err);
      } finally {
        this.loading = false;
      }
    },

    /** Tạo admin mới và cập nhật store */
    async createAdmin(request: AdminUserRequest) {
      this.loading = true;
      try {
        const newAdmin = await adminApi.createAdmin(request);
        this.admins.push(newAdmin); // cập nhật trực tiếp vào state
        return newAdmin;
      } catch (err) {
        console.error("Error creating admin:", err);
        throw err;
      } finally {
        this.loading = false;
      }
    },

    /** Cập nhật admin */
    async updateAdmin(id: number, request: AdminUserRequest) {
      this.loading = true;
      try {
        const updated = await adminApi.updateAdmin(id, request);
        const index = this.admins.findIndex((a) => a.id === id);
        if (index !== -1) this.admins[index] = updated; // update trực tiếp trong state
        if (this.selectedAdmin?.id === id) this.selectedAdmin = updated;
        return updated;
      } catch (err) {
        console.error("Error updating admin:", err);
        throw err;
      } finally {
        this.loading = false;
      }
    },

    /** Xoá admin */
    async deleteAdmin(id: number) {
      this.loading = true;
      try {
        await adminApi.deleteAdmin(id);
        this.admins = this.admins.filter((a) => a.id !== id);
        if (this.selectedAdmin?.id === id) this.selectedAdmin = null;
      } catch (err) {
        console.error("Error deleting admin:", err);
        throw err;
      } finally {
        this.loading = false;
      }
    },

    /** Tìm kiếm admin theo request */
    async searchAdmins(request: AdminUserSearchRequest) {
      this.loading = true;
      try {
        const results = await adminApi.searchAdminUsers(request);
        this.admins = results; // cập nhật luôn state để reactive
        return results;
      } catch (err) {
        console.error("Error searching admins:", err);
        return [];
      } finally {
        this.loading = false;
      }
    },
  },
});
