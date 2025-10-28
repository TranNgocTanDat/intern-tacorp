<script lang="ts" setup>
import type { CategoryRequest, CategoryResponse } from "@/models/Category";
import categoryApi from "@/services/categoryApi";
import { onMounted, ref } from "vue";
import CategoryForm from "./components/CategoryForm.vue";
import CategoryList from "./components/CategoryList.vue";
import { usePartnerStore } from "@/store/partnerStore";

// biến lưu trạng thái
const loading = ref(false);

// biến lưu category
const selectedCategory = ref<CategoryResponse | null>(null);
// biên lưu id category
const selectedCategoryId = ref<number | null>(null);

// biến hiển thị dialog tạo mới
const showAddModal = ref(false);
// biến hiển thị dialog edit
const showEditModal = ref(false);
// biến hiển thị dialog delete
const showDeleteModal = ref(false);

const categoriesRef = ref<any>(null);

const categories = ref<CategoryResponse[]>([]);
const getCategories = async () => {
  loading.value = true;
  try {
    const response = await categoryApi.getCategoriesWithDetails();
    categories.value = response;
  } catch (error) {
    console.error("Lỗi khi lấy danh mục:", error);
    alert("Đã xảy ra lỗi khi lấy danh mục.");
  } finally {
    loading.value = false;
  }
};

onMounted(() => {
  getCategories();
});

// Hàm mở dialog thêm mới
const handleOpenAddModal = () => {
  showAddModal.value = true;
};

// call api tạo mới category
const handleCreateCategory = async (request: CategoryRequest) => {
  loading.value = true;
  try {
    await categoryApi.addCategory(request);
    showAddModal.value = false;
    categoriesRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("Lỗi khi tạo category:", error);
    alert("Đã xảy ra lỗi khi tạo category.");
  } finally {
    loading.value = false;
  }
};

// Mở dialog sửa category
const handleOpenEditCategory = async (category: CategoryResponse) => {
  showEditModal.value = true;
  selectedCategory.value = { ...category };
};

// Hàm xử lý sửa category
const handleEditCategory = async (request: CategoryRequest) => {
  if (!selectedCategory.value) return;
  try {
    await categoryApi.updateCategory(selectedCategory.value.id, request);
    showEditModal.value = false;
    categoriesRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("Lỗi khi sửa category:", error);
    alert("Đã xảy ra lỗi khi sửa category.");
  } finally {
    loading.value = false;
  }
};

// Mở dialog xóa category
const handleOpenDeleteCategory = async (id: number) => {
  showDeleteModal.value = true;
  selectedCategoryId.value = id;
};
// Xóa category
const handleDeleteCategory = async (id: number) => {
  try {
    await categoryApi.deleteCategory(id);
    showDeleteModal.value = false;
    selectedCategoryId.value = null;
    categoriesRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("Lỗi khi xoá category:", error);
    alert("Đã xảy ra lỗi khi xoá category.");
  } finally {
    loading.value = false;
  }
};

const partnerStore = usePartnerStore();

onMounted(async () => {
  if (!partnerStore.partners.length) {
    await partnerStore.fetchPartners();
  }
});
</script>

<template>
  <div class="management-page">
    <div class="page-top">
      <h1 class="title-page">Quản lý danh mục</h1>

      <!-- Nút mở dialog -->
      <el-button class="btn-add" type="primary" @click="handleOpenAddModal"
        >Thêm mới Admin</el-button
      >
    </div>

    <CategoryForm
      :partners="partnerStore.partners"
      :categories="categories"
      v-if="showAddModal"
      :visible="showAddModal"
      mode="create"
      @update:visible="showAddModal = $event"
      @submit-form="handleCreateCategory"
    />

    <CategoryForm
      :partners="partnerStore.partners"
      :categories="categories"
      v-if="showEditModal && selectedCategory"
      :visible="showEditModal"
      :initialData="selectedCategory"
      mode="update"
      @update:visible="showEditModal = $event"
      @submit-form="handleEditCategory"
    />
    <el-dialog
      v-model="showDeleteModal"
      title="Xác nhận xoá"
      width="400px"
      :close-on-click-modal="false"
      :close-on-press-escape="false"
    >
      <span> Bạn có muốn xoá danh mục này không? </span>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="showDeleteModal = false">Không</el-button>
          <el-button
            type="danger"
            @click="handleDeleteCategory(selectedCategoryId!)"
          >
            Có
          </el-button>
        </span>
      </template>
    </el-dialog>

    <CategoryList
      ref="categoriesRef"
      :loading="loading"
      @edit-category="handleOpenEditCategory"
      @delete-category="handleOpenDeleteCategory"
    />
  </div>
</template>

<style lang="css" scoped></style>
