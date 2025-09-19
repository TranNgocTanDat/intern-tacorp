<script setup lang="ts">
import type { AdminUserRequest, AdminUserResponse } from "@/models/AdminUser";
import { ref, watch } from "vue";

const props = defineProps<{
  initialData?: AdminUserResponse | null;
  mode?: "create" | "update" ;
  visible?: boolean;
}>();

const emit = defineEmits<{
  (e: "submit-form", request: AdminUserRequest): void;
  (e: "update:visible", value: boolean): void;
}>();

const username = ref("");
const password = ref("");
const fullName = ref("");
const email = ref("");
const phone = ref("");
const isActive = ref(true);

// Nạp dữ liệu khi có initialData (edit/update)
watch(
  () => props.initialData,
  (val) => {
    if (val) {
      username.value = val.username;
      fullName.value = val.fullName ?? "";
      email.value = val.email ?? "";
      phone.value = val.phone ?? "";
      isActive.value = val.isActive;
    }
  },
  { immediate: true }
);

// Reset Form
const resetForm = () => {
  username.value = "";
  password.value = "";
  fullName.value = "";
  email.value = "";
  phone.value = "";
  isActive.value = true;
};

const submitForm = () => {
  if (!username.value || (!props.initialData && !password.value)) {
    alert("Vui lòng nhập tên đăng nhập và mật khẩu.");
    return;
  }

  emit("submit-form", {
    username: username.value,
    password: password.value,
    fullName: fullName.value,
    email: email.value,
    phone: phone.value,
    isActive: isActive.value,
  });

  if (props.mode === "create") {
    resetForm();
  }

  emit("update:visible", false);
};
</script>

<template>
  <el-dialog
    :title="props.mode === 'create' ? 'Thêm mới Admin' : props.initialData ? 'Cập nhật AdminUser' : ''"
    :model-value="props.visible"
    width="600px"
    @close="emit('update:visible', false)"
  >
    <form @submit.prevent="submitForm" class="space-y-4">
      <el-input v-model="username" placeholder="Tên đăng nhập" clearable />

      <el-input
        v-if="!props.initialData"
        v-model="password"
        type="password"
        placeholder="Mật khẩu"
        show-password
        clearable
      />

      <el-input v-model="fullName" placeholder="Họ tên" clearable />
      <el-input v-model="email" placeholder="Email" clearable />
      <el-input v-model="phone" placeholder="Số điện thoại" clearable />

      <el-checkbox v-model="isActive">Hoạt động</el-checkbox>
    </form>
    <template #footer>
      <div class="flex justify-end gap-2 mt-4">
        <el-button @click="emit('update:visible', false)">Hủy</el-button>
        <el-button type="primary" native-type="submit" @click="submitForm">
          {{
            props.mode === "create"
              ? "Thêm mới"
              : props.initialData
              ? "Lưu"
              : ""
          }}
        </el-button>
      </div>
    </template>
  </el-dialog>
</template>
