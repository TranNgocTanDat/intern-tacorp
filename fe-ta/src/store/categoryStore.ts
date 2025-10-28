import type {
  CategoryFilterRequest,
  CategoryRequest,
  CategoryResponse,
} from "@/models/Category";
import categoryApi from "@/services/categoryApi";
import { defineStore } from "pinia";

export const useCategoryStore = defineStore("category", {
  state: () => ({
    categoriesParent: [] as CategoryResponse[], // cho header
    categoriesChildren: [] as CategoryResponse[], // cho carousel
    selectedCategory: null as CategoryResponse | null,
    loading: false,
  }),

  actions: {
    async getAllCategories() {
      this.loading = true;
      try {
        this.categoriesParent = await categoryApi.getCategories();
      } catch (error) {
        console.error("Lỗi khi lấy danh sách danh mục:", error);
        throw error;
      } finally {
        this.loading = false;
      }
    },

    async getAllCategoriesWithDetails() {
      this.loading = true;
      try {
        this.categoriesParent = await categoryApi.getCategoriesWithDetails();
        console.log("Lấy danh mục chi tiết thành công:", this.categoriesParent);
      } catch (error: any) {
        console.error("Lỗi khi lấy danh mục chi tiết:", error);
      } finally {
        this.loading = false;
      }
    },

    async getCategoryById(id: number) {
      this.loading = true;
      try {
        this.selectedCategory = await categoryApi.getCategoryById(id);
      } catch (error: any) {
        console.error(`Lỗi khi lấy danh mục ID=${id}:`, error);
      } finally {
        this.loading = false;
      }
    },

    async addCategory(request: CategoryRequest) {
      this.loading = true;
      try {
        const newCategory = await categoryApi.addCategory(request);
        this.categoriesParent.push(newCategory);
        return newCategory;
      } catch (error: any) {
        console.error("Lỗi khi thêm danh mục:", error);
        throw error;
      } finally {
        this.loading = false;
      }
    },

    async updateCategory(id: number, request: CategoryRequest) {
      this.loading = true;
      try {
        const updatedCategory = await categoryApi.updateCategory(id, request);
        const index = this.categoriesParent.findIndex((c) => c.id === id);
        if (index !== -1) this.categoriesParent[index] = updatedCategory;
        return updatedCategory;
      } catch (error: any) {
        console.error(`Lỗi khi cập nhật danh mục ID=${id}:`, error);
        throw error;
      } finally {
        this.loading = false;
      }
    },

    async deleteCategory(id: number) {
      this.loading = true;
      try {
        await categoryApi.deleteCategory(id);
        this.categoriesParent = this.categoriesParent.filter((c) => c.id !== id);
      } catch (error: any) {
        console.error(`Lỗi khi xóa danh mục ID=${id}:`, error);
        throw error;
      } finally {
        this.loading = false;
      }
    },

    async filterCategories(request: CategoryFilterRequest) {
      this.loading = true;
      try {
        this.categoriesParent = await categoryApi.filterCategories(request);
      } catch (error: any) {
        console.error("Lỗi khi lọc danh mục:", error);
      } finally {
        this.loading = false;
      }
    },

    async getCategoryChildren() {
      this.loading = true;
      try {
        this.categoriesChildren = await categoryApi.getCategoryChildren();
        console.log("Lấy danh mục con thành công:", this.categoriesChildren);
      } catch (error: any) {
        console.error("Lỗi khi lấy danh mục con:", error);
      } finally {
        this.loading = false;
      }
    },
  },
});
