import { defineStore } from "pinia";
import { ref } from "vue";
import type {
  ProductSpecRequest,
  ProductSpecResponse,
  ProductSpecFilterRequest,
} from "@/models/ProductSpec";
import productSpecApi from "@/services/productSpecApi";

export const useProductSpecStore = defineStore("productSpecStore", () => {
  // ===== STATE =====
  const specs = ref<ProductSpecResponse[]>([]);
  const loading = ref(false);
  const error = ref<string | null>(null);

  // ===== ACTIONS =====

  /** 🔹 Lấy tất cả ProductSpec */
  const fetchSpecs = async () => {
    loading.value = true;
    error.value = null;
    try {
      const res = await productSpecApi.getProductSpecs();
      specs.value = res;
    } catch (err: any) {
      error.value = err.message || "Không thể tải thông số kỹ thuật";
    } finally {
      loading.value = false;
    }
  };

  const getProductSpecsByProductId = async (productId: number) => {
    loading.value = true;
    error.value = null;
    try {
      const res = await productSpecApi.getProductSpecByProductId(productId);
      specs.value = res;
      return res;
    } catch (err: any) {
      error.value = err.message || "Không thể tải thông số kỹ thuật";
    } finally {
      loading.value = false;
    }
  };

  /** 🔹 Lọc ProductSpec theo điều kiện */
  const filterSpecs = async (filter: ProductSpecFilterRequest) => {
    loading.value = true;
    error.value = null;
    try {
      const res = await productSpecApi.filterProductSpecs(filter);
      specs.value = res;
    } catch (err: any) {
      error.value = err.message || "Lọc thất bại";
    } finally {
      loading.value = false;
    }
  };

  /** 🔹 Tạo ProductSpec mới */
  const createSpec = async (productId: number, request: ProductSpecRequest) => {
    try {
      const res = await productSpecApi.createProductSpec(productId, request);
      specs.value.push(res);
      return res;
    } catch (err: any) {
      error.value = err.message || "Tạo mới thất bại";
      throw err;
    }
  };

  /** 🔹 Cập nhật ProductSpec */
  const updateSpec = async (
    productId: number,
    specId: number,
    request: ProductSpecRequest
  ) => {
    try {
      const res = await productSpecApi.updateProductSpec(
        productId,
        specId,
        request
      );
      const index = specs.value.findIndex((s) => s.id === specId);
      if (index !== -1) specs.value[index] = res;
      return res;
    } catch (err: any) {
      error.value = err.message || "Cập nhật thất bại";
      throw err;
    }
  };

  /** 🔹 Xóa ProductSpec */
  const deleteSpec = async (specId: number) => {
    try {
      await productSpecApi.deleteProductSpec(specId);
      specs.value = specs.value.filter((s) => s.id !== specId);
    } catch (err: any) {
      error.value = err.message || "Xóa thất bại";
      throw err;
    }
  };

  // ===== RETURN =====
  return {
    specs,
    loading,
    error,
    fetchSpecs,
    getProductSpecsByProductId,
    filterSpecs,
    createSpec,
    updateSpec,
    deleteSpec,
  };
});
