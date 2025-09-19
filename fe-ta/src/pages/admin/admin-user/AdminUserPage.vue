<script lang="ts" setup>
import { ref } from "vue";
import AdminForm from "./components/AdminForm.vue";
import type { AdminUserRequest, AdminUserResponse } from "@/models/AdminUser";
import adminApi from "@/services/adminApi";
import AdminList from "./components/AdminList.vue";
import { onMounted } from 'vue';

// Danh sách người dùng Admin
const adminUser = ref<AdminUserResponse[]>([]);
const loading = ref(false);
const selectedUser = ref<AdminUserResponse | null>(null);
const selectedUserId = ref<number | null>(null);

// Biến điều khiển hiển thị dialog tạo mới
const showAddModal = ref(false);

// hiển thị dialog edit
const showEditModal = ref(false);

// hiển thị dialog delete
const showDeleteModal = ref(false);

// Hàm xử lý tạo mới admin
const loadUsers = async () => {
  try {
    const res = await adminApi.getAllAdmin();
    adminUser.value = res;
  } catch (err) {
    console.error('Load users failed', err);
  }
};

onMounted(() => {
  loadUsers();
});

const handleCreateUser = async (request: AdminUserRequest) => {
  loading.value = true;
  try {
    await adminApi.createAdmin(request);
    // refresh list after create
    await loadUsers();
    showAddModal.value = false;
  } catch (error) {
    console.error("Lỗi khi tạo người dùng:", error);
    alert("Đã xảy ra lỗi khi tạo người dùng.");
  } finally {
    loading.value = false;
  }
};

const handleOpenEditUser = async (user: AdminUserResponse) => {
  showEditModal.value = true;
  selectedUser.value = { ...user };
};

// Hàm xử lý sửa admin
const handleEditUser = async (request: AdminUserRequest) => {
  if (!selectedUser.value) return;
  try {
    const response = await adminApi.updateAdmin(selectedUser.value.id, request);
    const index = adminUser.value.findIndex((user) => user.id === response.id);
    if (index !== -1) {
      adminUser.value[index] = response;
    }
    showEditModal.value = false;
    selectedUser.value = null;
    await loadUsers();
  } catch (error) {
    console.error("Lỗi khi cập nhật người dùng:", error);
    alert("Đã xảy ra lỗi khi cập nhật người dùng.");
  } finally {
    loading.value = false;
  }
};

const handleOpenDeleteUser = async (id: number) => {
  showDeleteModal.value = true;
  selectedUserId.value = id;
};
// Hàm xử lý xoá admin
const handleDeleteUser = async (id: number) => {
  try {
    await adminApi.deleteAdmin(id);
    await loadUsers();
    showDeleteModal.value = false;
  } catch (error) {
    console.error("Lỗi khi xoá người dùng:", error);
    alert("Đã xảy ra lỗi khi xoá người dùng.");
  }
};
</script>

<template>
  <div class="p-6">
    <h1 class="text-2xl font-bold mb-4">Trang quản lý Admin Users</h1>

    <!-- Nút mở dialog -->
    <el-button type="primary" @click="showAddModal = true"
      >Thêm mới Admin</el-button
    >

    <!-- Dialog tạo mới admin -->
    <AdminForm
      :visible="showAddModal"
      @update:visible="showAddModal = $event"
      :initialData="null"
      mode="create"
      @submit-form="handleCreateUser"
    />

    <!-- Dialog tạo mới admin -->
    <AdminForm
      :visible="showEditModal"
      @update:visible="showEditModal = $event"
      :initialData="selectedUser"
      mode="create"
      @submit-form="handleEditUser"
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
            @click="handleDeleteUser(selectedUserId!)"
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


  <AdminList
    :items="adminUser"
    @edit-admin-user="handleOpenEditUser"
    @delete-admin-user="handleOpenDeleteUser"
  />
</template>
