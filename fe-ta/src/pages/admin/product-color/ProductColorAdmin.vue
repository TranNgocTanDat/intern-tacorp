<script lang="ts" setup>
import { ref, onMounted } from "vue";
import type {
  ProductColorRequest,
  ProductColorResponse,
} from "@/models/ProductColor";
import { useProductColorStore } from "@/store/productColorStore";
import ProductColorForm from "./components/ProductColorFrom.vue";
import ProductColorList from "./components/ProductColorList.vue";
import { useProductStore } from "@/store/productStore";

// 🏪 Store
const productColorStore = useProductColorStore();
const productStore = useProductStore();

// ⚙️ State
const loading = ref(false);
const selectedProductColor = ref<ProductColorResponse | null>(null);
const selectedProductColorId = ref<number | null>(null);

const showAddModal = ref(false);
const showEditModal = ref(false);
const showDeleteModal = ref(false);

const productColorListRef = ref<any>(null);

// 🟢 Fetch dữ liệu ban đầu
onMounted(async () => {
  loading.value = true;
  await productColorStore.getAllProductColors();
  loading.value = false;
});
onMounted(async () => {
  await productStore.getAllProducts();
});

// 🟢 Tạo ProductColor
const handleCreateProductColor = async (request: ProductColorRequest) => {
  loading.value = true;
  try {
    await productColorStore.createProductColor(request);
    showAddModal.value = false;
    productColorListRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("❌ Lỗi khi tạo màu sản phẩm:", error);
    alert("Đã xảy ra lỗi khi tạo màu sản phẩm.");
  } finally {
    loading.value = false;
  }
};

// 🟡 Mở dialog sửa
const handleOpenEditProductColor = (productColor: ProductColorResponse) => {
  selectedProductColor.value = { ...productColor };
  showEditModal.value = true;
};

// 🟠 Cập nhật ProductColor
const handleEditProductColor = async (request: ProductColorRequest) => {
  if (!selectedProductColor.value) return;
  loading.value = true;
  try {
    await productColorStore.updateProductColor(
      selectedProductColor.value.id,
      request
    );
    showEditModal.value = false;
    productColorListRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("❌ Lỗi khi sửa màu sản phẩm:", error);
    alert("Đã xảy ra lỗi khi sửa màu sản phẩm.");
  } finally {
    loading.value = false;
  }
};

// 🔴 Mở dialog xoá
const handleOpenDeleteProductColor = (id: number) => {
  selectedProductColorId.value = id;
  showDeleteModal.value = true;
};

// ⚫ Xoá ProductColor
const handleDeleteProductColor = async () => {
  if (!selectedProductColorId.value) return;
  loading.value = true;
  try {
    await productColorStore.deleteProductColor(selectedProductColorId.value);
    showDeleteModal.value = false;
    selectedProductColorId.value = null;
    productColorListRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("❌ Lỗi khi xoá màu sản phẩm:", error);
    alert("Đã xảy ra lỗi khi xoá màu sản phẩm.");
  } finally {
    loading.value = false;
  }
};
</script>

<template>
  <div class="management-page">
    <div class="page-top">
      <h1 class="title-page">Quản lý màu sản phẩm</h1>
      <el-button type="primary" class="btn-add" @click="showAddModal = true">
        + Thêm mới màu
      </el-button>
    </div>

    <!-- 🟢 Form thêm -->
    <ProductColorForm
      :visible="showAddModal"
      mode="create"
      @update:visible="showAddModal = $event"
      :products="productStore.products"
      @submit-form="handleCreateProductColor"
    />

    <!-- 🟠 Form sửa -->
    <ProductColorForm
      :visible="showEditModal"
      mode="update"
      :products="productStore.products"
      :initialData="selectedProductColor"
      @update:visible="showEditModal = $event"
      @submit-form="handleEditProductColor"
    />

    <!-- 🔴 Modal xác nhận xóa -->
    <div
      v-if="showDeleteModal"
      style="
        background-color: rgba(255, 255, 255, 0.6);
        position: fixed;
        inset: 0;
        display: flex;
        justify-content: center;
        align-items: center;
        z-index: 1000;
      "
    >
      <div
        style="
          width: 400px;
          height: 250px;
          background-color: white;
          padding: 20px;
          border-radius: 8px;
          box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
          display: flex;
          flex-direction: column;
          justify-content: center;
          align-items: center;
        "
      >
        <h2 style="margin-bottom: 20px">
          Bạn có chắc muốn xoá màu sản phẩm này?
        </h2>
        <div style="margin-top: 20px">
          <button
            class="px-4 py-2 bg-black text-white rounded mr-4"
            @click="handleDeleteProductColor"
          >
            Có
          </button>
          <button
            class="px-4 py-2 bg-black text-white rounded"
            @click="showDeleteModal = false"
          >
            Không
          </button>
        </div>
      </div>
    </div>

    <!-- 📋 Danh sách ProductColor -->
    <ProductColorList
      ref="productColorListRef"
      :loading="productColorStore.loading"
      @edit-color="handleOpenEditProductColor"
      @delete-color="handleOpenDeleteProductColor"
    />
  </div>
</template>

<style scoped>
.management-page {
  padding: 20px;
}
.page-top {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 16px;
}
.title-page {
  font-size: 24px;
  font-weight: 600;
}
</style>
