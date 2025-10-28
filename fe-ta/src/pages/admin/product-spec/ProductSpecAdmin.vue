<script lang="ts" setup>
import type { ProductResponse } from "@/models/Product";
import productApi from "@/services/productApi";
import { onMounted, ref } from "vue";

import type {
  ProductSpecRequest,
  ProductSpecResponse,
} from "@/models/ProductSpec";
import productSpecApi from "@/services/productSpecApi";
import ProductSpecForm from "./components/ProductSpecForm.vue";
import ProductSpecList from "./components/ProductSpecList.vue";

const loading = ref(false);
const showAddModal = ref(false);
const showEditModal = ref(false);
const showDeleteModal = ref(false);

const selectedProductSpec = ref<ProductSpecResponse | null>(null);
const selectedSpecId = ref<number | null>(null);

const productSpecRef = ref<any>(null);

const handleOpenAddModal = () => {
  showAddModal.value = true;
};

const products = ref<ProductResponse[]>([]);

// load products
const loadProducts = async () => {
  try {
    const response = await productApi.getAllProducts();
    products.value = response;
  } catch (error) {
    console.error("Lỗi khi lấy danh sách product:", error);
    products.value = [];
  }
};
onMounted(() => {
  loadProducts();
});

// call api tạo mới product spec
const handleCreateProductSpec = async (
  productId: number,
  request: ProductSpecRequest
) => {
  loading.value = true;
  try {
    await productSpecApi.createProductSpec(productId, request);
    showAddModal.value = false;
    productSpecRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("Lỗi khi tạo product spec:", error);
    alert("Đã xảy ra lỗi khi tạo product spec.");
  } finally {
    loading.value = false;
  }
};

const handleOpenEditProductSpec = (spec: ProductSpecResponse) => {
  selectedProductSpec.value = spec;
  showEditModal.value = true;
};

// call api sửa product spec
const handleUpdateProductSpec = async (
  productId: number,
  request: ProductSpecRequest
) => {
  if (!selectedProductSpec.value) return;
  loading.value = true;
  try {
    await productSpecApi.updateProductSpec(
      productId,
      selectedProductSpec.value.id,
      request
    );
    showEditModal.value = false;
    selectedProductSpec.value = null;
    productSpecRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("Lỗi khi cập nhật product spec:", error);
    alert("Đã xảy ra lỗi khi cập nhật product spec.");
  } finally {
    loading.value = false;
  }
};

const handleOpenDeleteProductSpec = (id: number) => {
  selectedSpecId.value = id;
  showDeleteModal.value = true;
};

// call api xóa product spec
const handleDeleteProductSpec = async (id: number) => {
  loading.value = true;
  try {
    await productSpecApi.deleteProductSpec(id);
    showDeleteModal.value = false;
    selectedSpecId.value = null;
    productSpecRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("Lỗi khi xoá product spec:", error);
    alert("Đã xảy ra lỗi khi xoá product spec.");
  } finally {
    loading.value = false;
  }
};
</script>

<template>
  <div class="management-page">
    <div class="page-top">
      <h1 class="title-page">Product Spec</h1>

      <!-- Nút mở dialog -->
      <el-button class="btn-add" type="primary" @click="handleOpenAddModal"
        >Thêm mới Spec</el-button
      >
    </div>

    <!-- Form tạo mới -->
    <ProductSpecForm
      :products="products"
      :visible="showAddModal"
      mode="create"
      @update:visible="showAddModal = $event"
      @submit-form="handleCreateProductSpec"
    />

    <!-- Form edit -->
    <ProductSpecForm
      :products="products"
      :visible="showEditModal"
      :initialData="selectedProductSpec"
      mode="update"
      @update:visible="showEditModal = $event"
      @submit-form="handleUpdateProductSpec"
    />

    <!-- Xác nhận xoá -->
    <el-dialog
      v-model="showDeleteModal"
      title="Xác nhận xoá"
      width="400px"
      :close-on-click-modal="false"
      :close-on-press-escape="false"
    >
      <span> Bạn có muốn xoá Spec này không? </span>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="showDeleteModal = false">Không</el-button>
          <el-button
            type="danger"
            @click="handleDeleteProductSpec(selectedSpecId!)"
          >
            Có
          </el-button>
        </span>
      </template>
    </el-dialog>

    <!-- List -->
    <ProductSpecList
      ref="productSpecRef"
      @edit-spec="handleOpenEditProductSpec"
      @delete-spec="handleOpenDeleteProductSpec"
    />
  </div>
</template>

<style lang="css" scoped></style>
