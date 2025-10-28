import { defineStore } from "pinia";
import productApi from "@/services/productApi";
import type {
  ProductFilterRequest,
  ProductRequest,
  ProductResponse,
} from "@/models/Product";

export const useProductStore = defineStore("productStore", {
  state: () => ({
    products: [] as ProductResponse[],
    selectedProduct: null as ProductResponse | null,
    loading: false,
    error: null as string | null,
  }),

  actions: {
    async getAllProducts() {
      try {
        this.loading = true;
        const res = await productApi.getAllProducts();
        this.products = res;
      } catch (error: any) {
        this.error = error.message || "Không thể tải sản phẩm.";
        console.error("getAllProducts error:", error);
      } finally {
        this.loading = false;
      }
    },

    async filterProducts(request: ProductFilterRequest) {
      try {
        this.loading = true;
        const res = await productApi.filterProducts(request);
        this.products = res;
      } catch (error: any) {
        this.error = error.message || "Không thể lọc sản phẩm.";
        console.error("filterProducts error:", error);
      } finally {
        this.loading = false;
      }
    },

    async getProductById(id: number) {
      try {
        this.loading = true;
        const res = await productApi.getProductById(id);
        this.selectedProduct = res;
        return res;
      } catch (error: any) {
        this.error = error.message || "Không thể lấy chi tiết sản phẩm.";
        console.error("getProductById error:", error);
      } finally {
        this.loading = false;
      }
    },

    async getProductBySlug(slug: string) {
      try {
        this.loading = true;
        const res = await productApi.getProductBySlug(slug);
        console.log("Fetched product by slug:", res);
        this.selectedProduct = res;
        return res;
      } catch (error: any) {
        this.error = error.message || "Không thể lấy chi tiết sản phẩm.";
        console.error("getProductBySlug error:", error);
      } finally {
        this.loading = false;
      }
    },

    async createProduct(request: ProductRequest) {
      try {
        this.loading = true;
        const res = await productApi.createProduct(request);
        this.products.push(res);
        return res;
      } catch (error: any) {
        this.error = error.message || "Không thể tạo sản phẩm.";
        console.error("createProduct error:", error);
        throw error;
      } finally {
        this.loading = false;
      }
    },

    async updateProduct(id: number, request: ProductRequest) {
      try {
        this.loading = true;
        const res = await productApi.updateProduct(id, request);
        const index = this.products.findIndex((p) => p.id === id);
        if (index !== -1) this.products[index] = res;
        return res;
      } catch (error: any) {
        this.error = error.message || "Không thể cập nhật sản phẩm.";
        console.error("updateProduct error:", error);
        throw error;
      } finally {
        this.loading = false;
      }
    },

    async deleteProduct(id: number) {
      try {
        this.loading = true;
        await productApi.deleteProduct(id);
        this.products = this.products.filter((p) => p.id !== id);
      } catch (error: any) {
        this.error = error.message || "Không thể xóa sản phẩm.";
        console.error("deleteProduct error:", error);
      } finally {
        this.loading = false;
      }
    },
  },
});
