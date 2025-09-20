<script lang="ts" setup>
import type {
  HeroSectionRequest,
  HeroSectionResponse,
} from "@/models/HeroSection";
import HeroSectionFrom from "./components/HeroSectionFrom.vue";
import { onMounted, ref } from "vue";
import heroSectionApi from "@/services/heroSectionApi";
import HeroSectionList from "./components/HeroSectionList.vue";

// Danh sách HeroSection
const heroSections = ref<HeroSectionResponse[]>([]);
const loading = ref(false);
const selectedHeroSection = ref<HeroSectionResponse | null>(null);
const selectedHeroSectionId = ref<number | null>(null);

// Biến điều khiển hiển thị dialog tạo mới
const showAddModal = ref(false);
// hiển thị dialog edit
const showEditModal = ref(false);
// hiển thị dialog delete
const showDeleteModal = ref(false);

// Load HeroSections
const loadingHeroSections = async () => {
  try {
    const res = await heroSectionApi.getAllHeroSections();
    heroSections.value = res;
  } catch (err) {
    console.error("Load hero sections failed", err);
  }
};

onMounted(() => {
  loadingHeroSections();
});

// Hàm xử lý tạo mới HeroSection
const handleCreateHeroSection = async (request: HeroSectionRequest) => {
  loading.value = true;
  try {
    await heroSectionApi.createHeroSection(request);

    // refresh list after create
    await loadingHeroSections();
    showAddModal.value = false;
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
    await heroSectionApi.updateHeroSection(
      selectedHeroSection.value.id,
      request
    );
    // refresh list after create
    await loadingHeroSections();
    showEditModal.value = false;
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
    // refresh list after create
    await loadingHeroSections();
    showDeleteModal.value = false;
    selectedHeroSectionId.value = null;
  } catch (error) {
    console.error("Lỗi khi xoá HeroSection:", error);
    alert("Đã xảy ra lỗi khi xoá HeroSection.");
  }
};
</script>

<template>
  <div>
    <h1 class="text-2xl font-bold mb-4">Trang quản lý Admin Users</h1>

    <!-- Nút mở dialog -->
    <el-button type="primary" @click="showAddModal = true"
      >Thêm mới Admin</el-button
    >
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
  </div>
  <HeroSectionList
    :heroSections="heroSections"
    :loading="loading"
    @edit-hero-section="handleOpenEditHeroSection"
    @delete-hero-section="handleOpenDeleteHeroSection"
    />
</template>

<style lang="css" scoped></style>
