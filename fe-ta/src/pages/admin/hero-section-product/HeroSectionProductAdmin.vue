<script lang="ts" setup>
import type {
  HeroSectionProductRequest,
  HeroSectionProductResponse,
} from "@/models/HeroSectionProduct";
import heroSectionProductApi from "@/services/heroSectionProductApi";
import { computed, onMounted, ref } from "vue";
import type { HeroSectionResponse } from "@/models/HeroSection";
import HeroSectionProductForm from "@/pages/admin/hero-section-product/components/HeroSectionProductForm.vue";
import type { ProductResponse } from "@/models/Product";
import heroSectionApi from "@/services/heroSectionApi";
import productApi from "@/services/productApi";
import HeroSectionProductList from "./components/HeroSectionProductList.vue";

// biến lưu danh sách hero section product
const heroSectionProducts = ref(<HeroSectionProductResponse[]>[]);
// biến lưu trạng thái tải
const loading = ref(false);
// biến lưu hero section product
const selectedHeroSectionProduct = ref<HeroSectionProductResponse | null>(null);
// biên lưu id hero section product
const selectedHeroSectionProductId = ref<number | null>(null);

// biến lưu danh sách hero section
const heroSections = ref(<HeroSectionResponse[]>[]);
// biến lưu danh sách sản phẩm
const products = ref(<ProductResponse[]>[]);

// biến hiển thị dialog tạo mới
const showAddModal = ref(false);
// biến hiển thị dialog edit
const showEditModal = ref(false);
// biến hiển thị dialog delete
const showDeleteModal = ref(false);

const heroProductListRef = ref<any>(null);

const canOpenDialog = computed(() => {
  return heroSections.value.length > 0 && products.value.length > 0;
});

// ✅ Hàm mở dialog thêm mới
const handleOpenAddModal = () => {
  if (!canOpenDialog.value) {
    alert("Vui lòng chờ tải dữ liệu Hero Section và Sản phẩm xong.");
    return;
  }
  showAddModal.value = true;
};

// call api load danh sách hero section product
const loadHeroSectionProducts = async () => {
  try {
    const response = await heroSectionProductApi.getAllHeroSectionProducts();
    heroSectionProducts.value = response;
  } catch (err) {
    console.error("Load hero section products failed", err);
  } finally {
    loading.value = false;
  }
};

// call api tạo mới hero section product
const handleCreateHeroSectionProduct = async (
  request: HeroSectionProductRequest
) => {
  loading.value = true;
  try {
    await heroSectionProductApi.createHeroSectionProduct(request);
    showAddModal.value = false;
     heroProductListRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("Lỗi khi tạo HeroSectionProduct:", error);
    alert("Đã xảy ra lỗi khi tạo HeroSectionProduct.");
  } finally {
    loading.value = false;
  }
};

// mở dialog sửa hero section product
const handleOpenEditHeroSectionProduct = async (
  hero: HeroSectionProductResponse
) => {
  showEditModal.value = true;
  selectedHeroSectionProduct.value = { ...hero };
};
// hàm xử lý sự kiện sửa hero section product
const handleUpdateHeroSectionProduct = async (
  request: HeroSectionProductRequest
) => {
  if (!selectedHeroSectionProduct.value) return;
  try {
    await heroSectionProductApi.updateHeroSectionProduct(
      selectedHeroSectionProduct.value.id,
      request
    );
    showEditModal.value = false;
     heroProductListRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("Lỗi khi cập nhật HeroSectionProduct:", error);
    alert("Đã xảy ra lỗi khi cập nhật HeroSectionProduct.");
  }
};
// mở dialog xóa hero section product
const handleOpenDeleteHeroSectionProduct = async (id: number) => {
  showDeleteModal.value = true;
  selectedHeroSectionProductId.value = id;
};
// hàm xử lý sự kiện xóa hero section product
const handleDeleteHeroSectionProduct = async (id: number) => {
  try {
    await heroSectionProductApi.deleteHeroSectionProduct(id);
    showDeleteModal.value = false;
    selectedHeroSectionProductId.value = null;
     heroProductListRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("Lỗi khi xoá HeroSectionProduct:", error);
    alert("Đã xảy ra lỗi khi xoá HeroSectionProduct.");
  }
};

// call api load danh sách hero section
const loadHeroSections = async () => {
  try {
    const response = await heroSectionApi.getAllHeroSections();
    heroSections.value = response;
  } catch (err) {
    console.error("Load hero sections failed", err);
  }
};
// call api load danh sách sản phẩm
const loadProducts = async () => {
  try {
    const response = await productApi.getAllProducts();
    products.value = response;
  } catch (err) {
    console.error("Load products failed", err);
  }
};
// gọi hàm load danh sách hero section và sản phẩm
onMounted(() => {
  loadHeroSectionProducts();
  loadHeroSections();
  loadProducts();
});
</script>

<template>
  <div class="management-page">
    <div class="page-top">
      <h1 class="title-page">HeroSectionProduct</h1>

      <!-- Nút mở dialog -->
      <el-button class="btn-add" type="primary" @click="handleOpenAddModal"
        >Thêm mới Hero Section Product</el-button
      >
    </div>
    <HeroSectionProductForm
      :visible="showAddModal"
      mode="create"
      @update:visible="showAddModal = $event"
      @submit-form="handleCreateHeroSectionProduct"
      :heroSections="heroSections"
      :products="products"
    />
    <HeroSectionProductForm
      :visible="showEditModal"
      mode="update"
      :initialData="selectedHeroSectionProduct"
      @update:visible="showEditModal = $event"
      @submit-form="handleUpdateHeroSectionProduct"
      :heroSections="heroSections"
      :products="products"
    />

    <div
      v-if="showDeleteModal"
      style="
        background-color: rgba(255, 255, 255, 0.5);
        position: fixed;
        top: 0;
        right: 0;
        bottom: 0;
        left: 0;
        display: flex;
        justify-content: center;
        align-items: center;
        border: 1px;
        z-index: 1000;
      "
    >
      <div
        style="
          width: 400px;
          height: 300px;
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
        <h2 style="top: 0; margin-bottom: 30px">
          Bạn có muốn xóa loại thiết bị không
        </h2>
        <div style="margin-top: 30px">
          <button
            class="px-4 py-2 bg-black text-white rounded mr-4"
            @click="
              handleDeleteHeroSectionProduct(selectedHeroSectionProductId!)
            "
          >
            Có
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
    <HeroSectionProductList
      ref="heroProductListRef"
      :loading="loading"
      @edit-hero-section-product="handleOpenEditHeroSectionProduct"
      @delete-hero-section-product="handleOpenDeleteHeroSectionProduct"
    />
  </div>
</template>

<style lang="css" scoped></style>
