<script lang="ts" setup>
import { ref, onMounted } from "vue";
import type { PartnerRequest, PartnerResponse } from "@/models/Partner";
import { usePartnerStore } from "@/store/partnerStore";
import PartnerForm from "./components/PartnerForm.vue";
import PartnerList from "./components/PartnerList.vue";

// 🏪 Store
const partnerStore = usePartnerStore();

// ⚙️ State
const loading = ref(false);
const selectedPartner = ref<PartnerResponse | null>(null);
const selectedPartnerId = ref<number | null>(null);

const showAddModal = ref(false);
const showEditModal = ref(false);
const showDeleteModal = ref(false);

const partnerListRef = ref<any>(null);

// 🟢 Fetch dữ liệu ban đầu
onMounted(async () => {
  loading.value = true;
  await partnerStore.fetchPartners();
  loading.value = false;
});

// 🟢 Tạo Partner
const handleCreatePartner = async (request: PartnerRequest) => {
  loading.value = true;
  try {
    await partnerStore.createPartner(request);
    showAddModal.value = false;
    partnerListRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("❌ Lỗi khi tạo Partner:", error);
    alert("Đã xảy ra lỗi khi tạo đối tác.");
  } finally {
    loading.value = false;
  }
};

// 🟡 Mở dialog sửa
const handleOpenEditPartner = (partner: PartnerResponse) => {
  selectedPartner.value = { ...partner };
  showEditModal.value = true;
};

// 🟠 Cập nhật Partner
const handleEditPartner = async (request: PartnerRequest) => {
  if (!selectedPartner.value) return;
  loading.value = true;
  try {
    await partnerStore.updatePartner(selectedPartner.value.id, request);
    showEditModal.value = false;
    partnerListRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("❌ Lỗi khi sửa Partner:", error);
    alert("Đã xảy ra lỗi khi sửa đối tác.");
  } finally {
    loading.value = false;
  }
};

// 🔴 Mở dialog xoá
const handleOpenDeletePartner = (id: number) => {
  selectedPartnerId.value = id;
  showDeleteModal.value = true;
};

// ⚫ Xoá Partner
const handleDeletePartner = async () => {
  if (!selectedPartnerId.value) return;
  loading.value = true;
  try {
    await partnerStore.deletePartner(selectedPartnerId.value);
    showDeleteModal.value = false;
    selectedPartnerId.value = null;
    partnerListRef.value?.gridRef?.commitProxy("query");
  } catch (error) {
    console.error("❌ Lỗi khi xoá Partner:", error);
    alert("Đã xảy ra lỗi khi xoá đối tác.");
  } finally {
    loading.value = false;
  }
};
</script>

<template>
  <div class="management-page">
    <div class="page-top">
      <h1 class="title-page">Quản lý đối tác</h1>
      <el-button type="primary" class="btn-add" @click="showAddModal = true">
        + Thêm mới đối tác
      </el-button>
    </div>

    <!-- 🟢 Form thêm -->
    <PartnerForm
      :visible="showAddModal"
      mode="create"
      @update:visible="showAddModal = $event"
      @submit-form="handleCreatePartner"
    />

    <!-- 🟠 Form sửa -->
    <PartnerForm
      :visible="showEditModal"
      mode="update"
      :initialData="selectedPartner"
      @update:visible="showEditModal = $event"
      @submit-form="handleEditPartner"
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
        <h2 style="margin-bottom: 20px">Bạn có chắc muốn xoá đối tác này?</h2>
        <div style="margin-top: 20px">
          <button
            class="px-4 py-2 bg-black text-white rounded mr-4"
            @click="handleDeletePartner"
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

    <!-- 📋 Danh sách Partner -->
    <PartnerList
      ref="partnerListRef"
      :loading="partnerStore.loading"
      @edit-partner="handleOpenEditPartner"
      @delete-partner="handleOpenDeletePartner"
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
