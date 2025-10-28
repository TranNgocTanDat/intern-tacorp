<script lang="ts" setup>
import type { ProductResponse } from "@/models/Product";
import productApi from "@/services/productApi";
import { onMounted, ref } from "vue";
import type { ProductMediaRequest } from "@/models/ProductMedia";
import productMediaAPi from "@/services/productMediaApi";
import ProductMediaForm from "./components/ProductMediaForm.vue";

const loading = ref(false);
const showAddModal = ref(false);
const showEditModal = ref(false);
const showDeleteModal = ref(false);
import type { ProductMediaResponse } from "@/models/ProductMedia";
import productMediaApi from "@/services/productMediaApi";
import ProductMediaList from "./components/ProductMediaList.vue";
const selectedProductMedia = ref<ProductMediaResponse | null>(null);
const selectedMediaId = ref<number | null>(null);

const productMediaRef = ref<any>(null);

const handleOpenAddModal = () => {
  showAddModal.value = true;
};



const products = ref<ProductResponse[]>([]);
// load categories
const loadProducts = async () => {
  try {
    const response = await productApi.getAllProducts();
    products.value = response;
  } catch (error) {
    console.error("Lỗi khi lấy danh mục:", error);
    products.value = [];
  }
};
onMounted(() => {
  loadProducts();
});

// call api tạo mới product
const handleCreateProductMedia = async (
  productId: number,
  request: ProductMediaRequest
) => {
  loading.value = true;
  try {
    await productMediaAPi.createProductMedia(productId, request);
    showAddModal.value = false;
    productMediaRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("Lỗi khi tạo product:", error);
    alert("Đã xảy ra lỗi khi tạo product.");
  } finally {
    loading.value = false;
  }
};
const handleOpenEditProduct = (media: ProductMediaResponse) => {
  selectedProductMedia.value = media;
  showEditModal.value = true;
};

// call api sửa product
const handleUpdateProductMedia = async (
  productId: number,
  request: ProductMediaRequest
) => {
  if (!selectedProductMedia.value) return;
  loading.value = true;
  try {
    await productMediaApi.updateProductMedia(
      selectedProductMedia.value.id,
      productId,
      request
    );
    showEditModal.value = false;
    selectedProductMedia.value = null;
    productMediaRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("Lỗi khi cập nhật product:", error);
    alert("Đã xảy ra lỗi khi cập nhật product.");
  } finally {
    loading.value = false;
  }
};

const handleOpenDeleteProduct = (id: number) => {
  selectedMediaId.value = id;
  showDeleteModal.value = true;
};

// call api xóa product
const handleDeleteProduct = async (id: number) => {
  loading.value = true;
  try {
    await productMediaAPi.deleteProductMedia(id);
    showDeleteModal.value = false;
    selectedMediaId.value = null;
    productMediaRef.value?.gridRef?.commitProxy("query");
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
    <ProductMediaForm
      :products="products"
      :visible="showAddModal"
      mode="create"
      @update:visible="showAddModal = $event"
      @submit-form="handleCreateProductMedia"
    />
    <ProductMediaForm
      :products="products"
      :visible="showEditModal"
      :initialData="selectedProductMedia"
      mode="update"
      @update:visible="showEditModal = $event"
      @submit-form="handleUpdateProductMedia"
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
            @click="handleDeleteProduct(selectedMediaId!)"
          >
            Có
          </el-button>
        </span>
      </template>
    </el-dialog>
    <ProductMediaList
      ref="productMediaRef"
      @edit-media="handleOpenEditProduct"
      @delete-media="handleOpenDeleteProduct"
    />
  </div>
</template>

<style lang="css" scoped></style>
