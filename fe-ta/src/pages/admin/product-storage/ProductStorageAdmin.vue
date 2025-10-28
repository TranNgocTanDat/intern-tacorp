<script lang="ts" setup>
import { ref, onMounted } from "vue";
import type {
  ProductStorageRequest,
  ProductStorageResponse,
} from "@/models/ProductStorage";
import { useProductStorageStore } from "@/store/productStorageStore";
import ProductStorageForm from "./components/ProductStorageForm.vue";
import ProductStorageList from "./components/ProductStorageList.vue";
import { useProductStore } from "@/store/productStore";

// 🏪 Store
const productStorageStore = useProductStorageStore();
const productStore = useProductStore();

// ⚙️ State
const loading = ref(false);
const selectedProductStorage = ref<ProductStorageResponse | null>(null);
const selectedProductStorageId = ref<number | null>(null);

const showAddModal = ref(false);
const showEditModal = ref(false);
const showDeleteModal = ref(false);

const productStorageListRef = ref<any>(null);

// 🟢 Fetch dữ liệu ban đầu
onMounted(async () => {
  loading.value = true;
  await productStorageStore.getAllProductStorages();
  await productStore.getAllProducts();
  loading.value = false;
});

// 🟢 Tạo ProductStorage
const handleCreateProductStorage = async (request: ProductStorageRequest) => {
  loading.value = true;
  try {
    await productStorageStore.createProductStorage(request);
    showAddModal.value = false;
    productStorageListRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("❌ Lỗi khi tạo dung lượng sản phẩm:", error);
    alert("Đã xảy ra lỗi khi tạo dung lượng sản phẩm.");
  } finally {
    loading.value = false;
  }
};

// 🟡 Mở dialog sửa
const handleOpenEditProductStorage = (productStorage: ProductStorageResponse) => {
  selectedProductStorage.value = { ...productStorage };
  showEditModal.value = true;
};

// 🟠 Cập nhật ProductStorage
const handleEditProductStorage = async (request: ProductStorageRequest) => {
  if (!selectedProductStorage.value) return;
  loading.value = true;
  try {
    await productStorageStore.updateProductStorage(
      selectedProductStorage.value.id,
      request
    );
    showEditModal.value = false;
    productStorageListRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("❌ Lỗi khi sửa dung lượng sản phẩm:", error);
    alert("Đã xảy ra lỗi khi sửa dung lượng sản phẩm.");
  } finally {
    loading.value = false;
  }
};

// 🔴 Mở dialog xoá
const handleOpenDeleteProductStorage = (id: number) => {
  selectedProductStorageId.value = id;
  showDeleteModal.value = true;
};

// ⚫ Xoá ProductStorage
const handleDeleteProductStorage = async () => {
  if (!selectedProductStorageId.value) return;
  loading.value = true;
  try {
    await productStorageStore.deleteProductStorage(selectedProductStorageId.value);
    showDeleteModal.value = false;
    selectedProductStorageId.value = null;
    productStorageListRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("❌ Lỗi khi xoá dung lượng sản phẩm:", error);
    alert("Đã xảy ra lỗi khi xoá dung lượng sản phẩm.");
  } finally {
    loading.value = false;
  }
};
</script>

<template>
  <div class="management-page">
    <div class="page-top">
      <h1 class="title-page">Quản lý dung lượng sản phẩm</h1>
      <el-button type="primary" class="btn-add" @click="showAddModal = true">
        + Thêm mới dung lượng
      </el-button>
    </div>

    <!-- 🟢 Form thêm -->
    <ProductStorageForm
      :visible="showAddModal"
      mode="create"
      @update:visible="showAddModal = $event"
      :products="productStore.products"
      @submit-form="handleCreateProductStorage"
    />

    <!-- 🟠 Form sửa -->
    <ProductStorageForm
      :visible="showEditModal"
      mode="update"
      :products="productStore.products"
      :initialData="selectedProductStorage"
      @update:visible="showEditModal = $event"
      @submit-form="handleEditProductStorage"
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
          Bạn có chắc muốn xoá dung lượng sản phẩm này?
        </h2>
        <div style="margin-top: 20px">
          <button
            class="px-4 py-2 bg-black text-white rounded mr-4"
            @click="handleDeleteProductStorage"
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

    <!-- 📋 Danh sách ProductStorage -->
    <ProductStorageList
      ref="productStorageListRef"
      :loading="productStorageStore.loading"
      @edit-storage="handleOpenEditProductStorage"
      @delete-storage="handleOpenDeleteProductStorage"
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
