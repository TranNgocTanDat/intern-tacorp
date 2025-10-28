<template>
  <div class="demo-page-wrapper" style="z-index: 0">
    <vxe-grid ref="gridRef" v-bind="gridOptions" v-on="gridEvents">
      <template #actions="{ row }">
        <el-button-group class="ml-4">
          <el-button type="primary" :icon="Edit" @click="handleEdit(row)" />
          <el-button type="primary" :icon="Share" />
          <el-button
            type="primary"
            :icon="Delete"
            @click="handleDelete(row.id)"
          />
        </el-button-group>
      </template>
    </vxe-grid>
  </div>
</template>

<script setup lang="ts">
import type {
  AdminUserResponse,
  AdminUserSearchRequest,
} from "@/models/AdminUser";
import { ref, reactive, onMounted, watch } from "vue";
import type {
  VxeGridProps,
  VxeGridPropTypes,
  VxeGridListeners,
} from "vxe-table";
import { Delete, Edit, Share } from "@element-plus/icons-vue";
import { useAdminStore } from "@/store/adminStore";

const props = defineProps<{ items?: AdminUserResponse[] }>();
const emit = defineEmits<{
  (e: "edit-admin-user", user: AdminUserResponse): void;
  (e: "delete-admin-user", id: number): void;
}>();

const handleEdit = (user: AdminUserResponse) => emit("edit-admin-user", user);
const handleDelete = (id: number) => emit("delete-admin-user", id);

const gridRef = ref<any>(null);
defineExpose({ gridRef });

const adminStore = useAdminStore();

// Load dữ liệu khi mount
onMounted(() => {
  if (adminStore.admins.length === 0) {
    adminStore.loadAdmins();
  }
});

// Hàm search dùng store action
const handleSearchUsers = async (request: AdminUserSearchRequest) => {
  return await adminStore.searchAdmins(request);
};

// Cấu hình grid
const gridOptions = reactive<
  VxeGridProps<AdminUserResponse> & {
    pagerConfig: VxeGridPropTypes.PagerConfig;
  }
>({
  border: true,
  stripe: true,
  showOverflow: "title",
  height: "100%",
  formConfig: {
    titleWidth: 80,
    titleAlign: "center",
    data: {
      username: "",
      fullName: "",
      email: "",
      phone: "",
      isActive: null,
    },
    items: [
      {
        field: "username",
        title: "Tên đăng nhập",
        span: 6,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "Nhập tên đăng nhập" },
        },
      },
      {
        field: "fullName",
        title: "Họ tên",
        span: 6,
        itemRender: { name: "VxeInput", props: { placeholder: "Nhập họ tên" } },
      },
      {
        field: "email",
        title: "Email",
        span: 6,
        itemRender: { name: "VxeInput", props: { placeholder: "Nhập email" } },
      },
      {
        field: "phone",
        title: "Số điện thoại",
        span: 6,
        folding: true,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "Nhập số điện thoại" },
        },
      },
      {
        field: "isActive",
        title: "Trạng thái",
        span: 6,
        folding: true,
        itemRender: {
          name: "VxeSelect",
          props: { placeholder: "Chọn trạng thái" },
          options: [
            { value: null, label: "Tất cả" },
            { value: true, label: "Hoạt động" },
            { value: false, label: "Ngưng" },
          ],
        },
      },
      {
        span: 6,
        // collapseNode: true,
        itemRender: {
          name: "VxeButtonGroup",
          options: [
            { type: "submit", content: "Tìm kiếm", status: "primary" },
            { type: "reset", content: "Làm mới" },
          ],
        },
      },
    ],
  },
  columns: [
    { type: "seq", width: 60, title: "#" },
    { field: "username", title: "Tên đăng nhập", minWidth: 150 },
    { field: "fullName", title: "Họ tên", minWidth: 150 },
    { field: "email", title: "Email", minWidth: 200 },
    { field: "phone", title: "Số điện thoại", minWidth: 130 },
    {
      field: "isActive",
      title: "Trạng thái",
      minWidth: 100,
      formatter: ({ cellValue }) => (cellValue ? "Hoạt động" : "Ngưng"),
    },
    {
      field: "actions",
      title: "Hành động",
      width: 180,
      align: "center",
      fixed: "right",
      slots: { default: "actions" },
    },
  ],
  pagerConfig: { pageSize: 10, pageSizes: [10, 20, 50, 100] },
  proxyConfig: {
    form: true,
    response: { result: "data", total: "total" },
    ajax: {
      async query({ form }) {
        const cleanForm = Object.fromEntries(
          Object.entries(form).filter(
            ([_, v]) => v !== null && v !== "" && v !== undefined
          )
        );
        const isSearching = Object.keys(cleanForm).length > 0;
        let data: AdminUserResponse[] = [];

        if (isSearching) {
          data = await handleSearchUsers(cleanForm); // Gọi search nếu có điều kiện
        } else {
          // Form rỗng → gọi lại API luôn, không dùng store
          await adminStore.loadAdmins(); // ← luôn gọi API
          data = adminStore.admins;
        }

        return { data, total: data.length };
      },
    },
  },
});

// Nếu parent truyền items thì dùng trực tiếp
watch(
  () => props.items,
  (val) => {
    if (val && Array.isArray(val)) {
      gridOptions.data = val as AdminUserResponse[];
      gridOptions.proxyConfig = undefined;
    }
  },
  { immediate: true }
);

const gridEvents: VxeGridListeners = {
  proxyQuery() {
    console.log("proxy query chạy");
  },
};
</script>

<style scoped></style>
