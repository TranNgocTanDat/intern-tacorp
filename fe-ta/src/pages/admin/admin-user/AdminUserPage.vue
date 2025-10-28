<script setup lang="ts">
import { ref } from "vue";
import AdminForm from "./components/AdminForm.vue";
import AdminList from "./components/AdminList.vue";
import type { AdminUserRequest, AdminUserResponse } from "@/models/AdminUser";
import { useAdminStore } from "@/store/adminStore";

// Store Pinia
const adminStore = useAdminStore();

// Dialog & selection
const showAddModal = ref(false);
const showEditModal = ref(false);
const showDeleteModal = ref(false);
const selectedUser = ref<AdminUserResponse | null>(null);
const selectedUserId = ref<number | null>(null);

// Ref AdminList để commit proxy query (nếu dùng VXE Grid)
const adminUserRef = ref<any>(null);

/* ------------------- CREATE ------------------- */
const handleCreateUser = async (request: AdminUserRequest) => {
  try {
    await adminStore.createAdmin(request);
    showAddModal.value = false;
    // refresh list grid
    adminUserRef.value?.gridRef?.commitProxy("query");
  } catch (err) {
    alert("Đã xảy ra lỗi khi tạo người dùng.");
  }
};

/* ------------------- EDIT ------------------- */
const handleOpenEditUser = (user: AdminUserResponse) => {
  selectedUser.value = { ...user };
  showEditModal.value = true;
};

const handleEditUser = async (request: AdminUserRequest) => {
  if (!selectedUser.value) return;

  try {
    await adminStore.updateAdmin(selectedUser.value.id, request);
    showEditModal.value = false;
    selectedUser.value = null;
    adminUserRef.value?.gridRef?.commitProxy("query");
  } catch (err) {
    alert("Đã xảy ra lỗi khi cập nhật người dùng.");
  }
};

/* ------------------- DELETE ------------------- */
const handleOpenDeleteUser = (id: number) => {
  selectedUserId.value = id;
  showDeleteModal.value = true;
};

const handleDeleteUser = async (id: number) => {
  try {
    await adminStore.deleteAdmin(id);
    showDeleteModal.value = false;
    selectedUserId.value = null;
    adminUserRef.value?.gridRef?.commitProxy("query");
  } catch (err) {
    alert("Đã xảy ra lỗi khi xoá người dùng.");
  }
};
</script>

<template>
  <div class="management-page">
    <div class="page-top flex justify-between items-center mb-4">
      <h1 class="title-page text-xl font-bold">Admin Users</h1>
      <el-button type="primary" @click="showAddModal = true">Thêm mới Admin</el-button>
    </div>

    <!-- Dialog Create -->
    <AdminForm
      :visible="showAddModal"
      @update:visible="showAddModal = $event"
      :initialData="null"
      mode="create"
      @submit-form="handleCreateUser"
    />

    <!-- Dialog Edit -->
    <AdminForm
      :visible="showEditModal"
      @update:visible="showEditModal = $event"
      :initialData="selectedUser"
      mode="update"
      @submit-form="handleEditUser"
    />

    <!-- Dialog Delete -->
    <el-dialog
      v-model="showDeleteModal"
      title="Xác nhận xoá"
      width="400px"
      :close-on-click-modal="false"
      :close-on-press-escape="false"
    >
      <span>Bạn có muốn xoá người dùng này không?</span>
      <template #footer>
        <el-button @click="showDeleteModal = false">Không</el-button>
        <el-button
          type="danger"
          @click="handleDeleteUser(selectedUserId!)"
        >
          Có
        </el-button>
      </template>
    </el-dialog>

    <!-- Admin List -->
    <AdminList
      ref="adminUserRef"
      @edit-admin-user="handleOpenEditUser"
      @delete-admin-user="handleOpenDeleteUser"
    />
  </div>
</template>

<style scoped>
.management-page {
  padding: 1rem;
}
.page-top {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
</style>
