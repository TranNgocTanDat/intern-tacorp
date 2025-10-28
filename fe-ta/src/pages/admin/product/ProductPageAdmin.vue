<script lang="ts" setup>
import type { ProductRequest, ProductResponse } from "@/models/Product";
import productApi from "@/services/productApi";
import { onMounted, ref } from "vue";
import ProductForm from "./components/ProductFrom.vue";
import type { CategoryResponse } from "@/models/Category";
import categoryApi from "@/services/categoryApi";
import ProductList from "./components/ProductList.vue";

const loading = ref(false);
const showAddModal = ref(false);
const showEditModal = ref(false);
const showDeleteModal = ref(false);
const selectedProduct = ref<ProductResponse | null>(null);
const selectedProductId = ref<number | null>(null);

const productsRef = ref<any>(null);

const handleOpenAddModal = () => {
  showAddModal.value = true;
};

const categoriesChild = ref<CategoryResponse[]>([]);
// load categories
const loadCategoriesChild = async () => {
  try {
    const response = await categoryApi.getCategoryChildren();
    categoriesChild.value = response;
  } catch (error) {
    console.error("Lỗi khi lấy danh mục:", error);
    categoriesChild.value = [];
  }
};
onMounted(() => {
  loadCategoriesChild();
});

// call api tạo mới product
const handleCreateProduct = async (request: ProductRequest) => {
  loading.value = true;
  try {
    await productApi.createProduct(request);
    showAddModal.value = false;
    productsRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("Lỗi khi tạo product:", error);
    alert("Đã xảy ra lỗi khi tạo product.");
  } finally {
    loading.value = false;
  }
};

const handleOpenEditProduct = (product: ProductResponse) => {
  selectedProduct.value = product;
  showEditModal.value = true;
};

// call api sửa product
const handleUpdateProduct = async (request: ProductRequest) => {
  if (!selectedProduct.value) return;
  loading.value = true;
  try {
    await productApi.updateProduct(selectedProduct.value.id, request);
    showEditModal.value = false;
    selectedProduct.value = null;
    productsRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("Lỗi khi cập nhật product:", error);
    alert("Đã xảy ra lỗi khi cập nhật product.");
  } finally {
    loading.value = false;
  }
};

const handleOpenDeleteProduct = (id: number) => {
  selectedProductId.value = id;
  showDeleteModal.value = true;
};

// call api xóa product
const handleDeleteProduct = async (id: number) => {
  loading.value = true;
  try {
    await productApi.deleteProduct(id);
    showDeleteModal.value = false;
    selectedProductId.value = null;
    productsRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("Lỗi khi xoá product:", error);
    alert("Đã xảy ra lỗi khi xoá product.");
  } finally {
    loading.value = false;
  }
};
</script>

<template>
  <div class="management-page">
    <div class="page-top">
      <h1 class="title-page">Product</h1>

      <!-- Nút mở dialog -->
      <el-button class="btn-add" type="primary" @click="handleOpenAddModal"
        >Thêm mới Product</el-button
      >
    </div>
    <ProductForm
      :categoriesChild="categoriesChild"
      :visible="showAddModal"
      mode="create"
      @update:visible="showAddModal = $event"
      @submit-form="handleCreateProduct"
    />
    <ProductForm
      :categoriesChild="categoriesChild"
      :visible="showEditModal"
      mode="update"
      :initialData="selectedProduct"
      @update:visible="showEditModal = $event"
      @submit-form="handleUpdateProduct"
    />
    <el-dialog
      v-model="showDeleteModal"
      title="Xác nhận xoá"
      width="400px"
      :close-on-click-modal="false"
      :close-on-press-escape="false"
    >
      <span> Bạn có muốn xoá sản phẩm này không? </span>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="showDeleteModal = false">Không</el-button>
          <el-button
            type="danger"
            @click="handleDeleteProduct(selectedProductId!)"
          >
            Có
          </el-button>
        </span>
      </template>
    </el-dialog>
    <ProductList
      ref="productsRef"
      @edit-product="handleOpenEditProduct"
      @delete-product="handleOpenDeleteProduct"
    />
  </div>
</template>

<style lang="css" scoped></style>
