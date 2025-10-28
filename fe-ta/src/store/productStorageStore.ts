import { defineStore } from "pinia";
import { ref } from "vue";
import type {
  ProductStorageRequest,
  ProductStorageResponse,
  ProductStorageFilterRequest,
} from "@/models/ProductStorage";
import productStorageApi from "@/services/productStorageApi";

export const useProductStorageStore = defineStore("productStorage", () => {
  // ===== STATE =====
  const productStorages = ref<ProductStorageResponse[]>([]);
  const productStorage = ref<ProductStorageResponse | null>(null);
  const loading = ref(false);
  const error = ref<string | null>(null);

  // ===== ACTIONS =====

  // Lấy tất cả dung lượng
  const getAllProductStorages = async () => {
    loading.value = true;
    error.value = null;
    try {
      const data = await productStorageApi.getAllProductStorages();
      productStorages.value = data;
    } catch (err: any) {
      error.value = err.message || "Lỗi khi lấy danh sách dung lượng sản phẩm";
    } finally {
      loading.value = false;
    }
  };

  // Lấy dung lượng theo productId
  const getProductStorageByProductId = async (productId: number) => {
    loading.value = true;
    error.value = null;
    try {
      const data = await productStorageApi.getProductStorageByProductId(productId);
      productStorage.value = data;
    } catch (err: any) {
      error.value = err.message || "Lỗi khi lấy dung lượng theo sản phẩm";
    } finally {
      loading.value = false;
    }
  };

  // Tạo mới
  const createProductStorage = async (request: ProductStorageRequest) => {
    loading.value = true;
    error.value = null;
    try {
      const data = await productStorageApi.createProductStorage(request);
      productStorages.value.push(data);
      return data;
    } catch (err: any) {
      error.value = err.message || "Lỗi khi tạo dung lượng sản phẩm";
      throw err;
    } finally {
      loading.value = false;
    }
  };

  // Cập nhật
  const updateProductStorage = async (id: number, request: ProductStorageRequest) => {
    loading.value = true;
    error.value = null;
    try {
      const data = await productStorageApi.updateProductStorage(id, request);
      const index = productStorages.value.findIndex((s) => s.id === id);
      if (index !== -1) productStorages.value[index] = data;
      return data;
    } catch (err: any) {
      error.value = err.message || "Lỗi khi cập nhật dung lượng sản phẩm";
      throw err;
    } finally {
      loading.value = false;
    }
  };

  // Xóa
  const deleteProductStorage = async (id: number) => {
    loading.value = true;
    error.value = null;
    try {
      await productStorageApi.deleteProductStorage(id);
      productStorages.value = productStorages.value.filter((s) => s.id !== id);
    } catch (err: any) {
      error.value = err.message || "Lỗi khi xóa dung lượng sản phẩm";
      throw err;
    } finally {
      loading.value = false;
    }
  };

  // Lọc dung lượng theo điều kiện
  const filterProductStorages = async (request: ProductStorageFilterRequest) => {
    loading.value = true;
    error.value = null;
    try {
      const data = await productStorageApi.FilterProductStorages(request);
      productStorages.value = data;
    } catch (err: any) {
      error.value = err.message || "Lỗi khi lọc dung lượng sản phẩm";
    } finally {
      loading.value = false;
    }
  };

  // ===== RETURN =====
  return {
    productStorages,
    productStorage,
    loading,
    error,
    getAllProductStorages,
    getProductStorageByProductId,
    createProductStorage,
    updateProductStorage,
    deleteProductStorage,
    filterProductStorages,
  };
});
