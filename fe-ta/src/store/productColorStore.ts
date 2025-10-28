import { defineStore } from "pinia";
import { ref } from "vue";
import type {
  ProductColorRequest,
  ProductColorResponse,
  ProductColorFilterRequest,
} from "@/models/ProductColor";
import productColorApi from "@/services/productColorApi";

export const useProductColorStore = defineStore("productColor", () => {
  // ===== STATE =====
  const productColors = ref<ProductColorResponse[]>([]);
  const selectedproductColor = ref<ProductColorResponse | null>(null);
  const loading = ref(false);
  const error = ref<string | null>(null);

  // ===== ACTIONS =====

  // Lấy tất cả màu
  const getAllProductColors = async () => {
    loading.value = true;
    error.value = null;
    try {
      const data = await productColorApi.getAllProductColors();
      productColors.value = data;
    } catch (err: any) {
      error.value = err.message || "Lỗi khi lấy danh sách màu sản phẩm";
    } finally {
      loading.value = false;
    }
  };

  // Lấy theo ProductId
  const getProductColorByProductId = async (productId: number) => {
    loading.value = true;
    error.value = null;
    try {
      const data = await productColorApi.getProductColorByProductId(productId);
      productColors.value = data;
    } catch (err: any) {
      error.value = err.message || "Lỗi khi lấy màu theo sản phẩm";
    } finally {
      loading.value = false;
    }
  };

  // Lấy theo ProductId
  const getProductColorByProductIdAndColorId = async (
    productId: number,
    colorId: number
  ) => {
    loading.value = true;
    error.value = null;
    try {
      const data = await productColorApi.getProductColorByProductIdAndColorId(
        productId,
        colorId
      );
      productColors.value = data;
    } catch (err: any) {
      error.value = err.message || "Lỗi khi lấy màu theo sản phẩm";
    } finally {
      loading.value = false;
    }
  };

  // Tạo mới
  const createProductColor = async (request: ProductColorRequest) => {
    loading.value = true;
    error.value = null;
    try {
      const data = await productColorApi.createProductColor(request);
      productColors.value.push(data);
      return data;
    } catch (err: any) {
      error.value = err.message || "Lỗi khi tạo màu sản phẩm";
      throw err;
    } finally {
      loading.value = false;
    }
  };

  // Cập nhật
  const updateProductColor = async (
    id: number,
    request: ProductColorRequest
  ) => {
    loading.value = true;
    error.value = null;
    try {
      const data = await productColorApi.updateProductColor(id, request);
      const index = productColors.value.findIndex((c) => c.id === id);
      if (index !== -1) productColors.value[index] = data;
      return data;
    } catch (err: any) {
      error.value = err.message || "Lỗi khi cập nhật màu sản phẩm";
      throw err;
    } finally {
      loading.value = false;
    }
  };

  // Xóa
  const deleteProductColor = async (id: number) => {
    loading.value = true;
    error.value = null;
    try {
      await productColorApi.deleteProductColor(id);
      productColors.value = productColors.value.filter((c) => c.id !== id);
    } catch (err: any) {
      error.value = err.message || "Lỗi khi xóa màu sản phẩm";
      throw err;
    } finally {
      loading.value = false;
    }
  };

  // Lọc theo điều kiện
  const filterProductColors = async (request: ProductColorFilterRequest) => {
    loading.value = true;
    error.value = null;
    try {
      const data = await productColorApi.filterProductColors(request);
      productColors.value = data;
    } catch (err: any) {
      error.value = err.message || "Lỗi khi lọc màu sản phẩm";
    } finally {
      loading.value = false;
    }
  };

  // ===== RETURN =====
  return {
    productColors,
    selectedproductColor,
    loading,
    error,
    getAllProductColors,
    getProductColorByProductId,
    getProductColorByProductIdAndColorId,
    createProductColor,
    updateProductColor,
    deleteProductColor,
    filterProductColors,
  };
});
