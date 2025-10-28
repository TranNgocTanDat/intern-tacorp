<script lang="ts" setup>
import type {
  HeroSectionRequest,
  HeroSectionResponse,
} from "@/models/HeroSection";
import HeroSectionFrom from "./components/HeroSectionForm.vue";
import { ref } from "vue";
import heroSectionApi from "@/services/heroSectionApi";
import HeroSectionList from "./components/HeroSectionList.vue";
import { useHeroSectionStore } from "@/store/heroSectionStore";

// Danh sách HeroSection
const loading = ref(false);
const selectedHeroSection = ref<HeroSectionResponse | null>(null);
const selectedHeroSectionId = ref<number | null>(null);

// Biến điều khiển hiển thị dialog tạo mới
const showAddModal = ref(false);
// hiển thị dialog edit
const showEditModal = ref(false);
// hiển thị dialog delete
const showDeleteModal = ref(false);

const heroListRef = ref<any>(null);

const heroSectionStore = useHeroSectionStore();


// Hàm xử lý tạo mới HeroSection
const handleCreateHeroSection = async (request: HeroSectionRequest) => {
  loading.value = true;
  try {
    await heroSectionStore.create(request);
    showAddModal.value = false;

    // Gọi lại vxe-grid query
    heroListRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("Lỗi khi tạo HeroSection:", error);
    alert("Đã xảy ra lỗi khi tạo HeroSection.");
  } finally {
    loading.value = false;
  }
};

// Mở dialog sửa HeroSection
const handleOpenEditHeroSection = async (hero: HeroSectionResponse) => {
  showEditModal.value = true;
  selectedHeroSection.value = { ...hero };
};
// Hàm xử lý sửa HeroSection
const handleEditHeroSection = async (request: HeroSectionRequest) => {
  if (!selectedHeroSection.value) return;
  try {
    heroSectionStore.update(selectedHeroSection.value.id, request);

    showEditModal.value = false;
    // Gọi lại vxe-grid query
    heroListRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("Lỗi khi sửa HeroSection:", error);
    alert("Đã xảy ra lỗi khi sửa HeroSection.");
  }
};
// Mở dialog xóa HeroSection
const handleOpenDeleteHeroSection = async (id: number) => {
  showDeleteModal.value = true;
  selectedHeroSectionId.value = id;
};
// Xóa HeroSection
const handleDeleteHeroSection = async (id: number) => {
  try {
    await heroSectionApi.deleteHeroSection(id);
    showDeleteModal.value = false;

    selectedHeroSectionId.value = null;
    // Gọi lại vxe-grid query
    heroListRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("Lỗi khi xoá HeroSection:", error);
    alert("Đã xảy ra lỗi khi xoá HeroSection.");
  }
};
</script>

<template>
  <div class="management-page">
    <div class="page-top">
      <h1 class="title-page">HeroSections</h1>

      <!-- Nút mở dialog -->
      <el-button class="btn-add" type="primary" @click="showAddModal = true"
        >Thêm mới Admin</el-button
      >
    </div>
    <HeroSectionFrom
      :visible="showAddModal"
      mode="create"
      @update:visible="showAddModal = $event"
      @submit-form="handleCreateHeroSection"
    />
    <HeroSectionFrom
      :visible="showEditModal"
      mode="update"
      :initialData="selectedHeroSection"
      @update:visible="showEditModal = $event"
      @submit-form="handleEditHeroSection"
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
            @click="handleDeleteHeroSection(selectedHeroSectionId!)"
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
    <HeroSectionList
      ref="heroListRef"
      :loading="loading"
      @edit-hero-section="handleOpenEditHeroSection"
      @delete-hero-section="handleOpenDeleteHeroSection"
    />
  </div>
</template>

<style lang="css" scoped></style>
