<template>
  <div class="demo-page-wrapper" style="z-index: 0;">
    <vxe-grid v-bind="gridOptions" v-on="gridEvents">
      <template #actions="{ row }">
        <button
          class="px-3 py-1 bg-black text-white rounded hover:bg-blue-600 mr-2"
          @click="handleEdit(row)"
        >
          Sửa
        </button>
        <button
          class="px-3 py-1 bg-black text-white rounded hover:bg-red-600"
          @click="handleDelete(row.id)"
        >
          Xoá
        </button>
      </template>
    </vxe-grid>
  </div>
</template>

<script setup lang="ts">
// import { getAdminUsers, searchAdminUsers } from "@/services/adminApi";
import type { AdminUserResponse, AdminUserSearchRequest } from "@/models/AdminUser";
import adminApi from "@/services/adminApi";
import { reactive, ref } from "vue";
import { watch } from 'vue';
import type {
  VxeGridProps,
  VxeGridPropTypes,
  VxeGridListeners,
} from "vxe-table";

const props = defineProps<{
  items?: AdminUserResponse[];
}>();

const emit = defineEmits<{
  (e: "edit-admin-user", user: AdminUserResponse): void;
  (e: "delete-admin-user", id: number): void;
}>();

const handleEdit = (user: AdminUserResponse) => {
  emit("edit-admin-user", user);
};

const handleDelete = (id: number) => {
  emit("delete-admin-user", id);
};

const loading = ref(false);

const handleGetUsers = async () => {
  loading.value = true;
  try {
    const response = await adminApi.getAllAdmin();
    return response;
  } catch (error) {
    console.error("Lỗi khi lấy danh sách người dùng:", error);
    return [];
  } finally {
    loading.value = false;
  }
};

const handleSearchUsers = async (request: AdminUserSearchRequest) => {
  loading.value = true;
  try {
    const response = await adminApi.searchAdminUsers(request);
    return response;
  } catch (error) {
    console.error("Lỗi khi tìm kiếm người dùng:", error);
    return [];
  } finally {
    loading.value = false;
  }
};

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
    titleAlign: "right",
    data: {
      username: "",
      fullName: "",
      email: "",
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
        span: 6,
        collapseNode: true,
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
  pagerConfig: {
    pageSize: 10,
    pageSizes: [10, 20, 50, 100],
  },
  proxyConfig: {
    form: true,
    response: {
      result: "data",
      total: "total",
    },
    ajax: {
      async query({ form }) {
        const cleanForm = Object.fromEntries(
          Object.entries(form).filter(
            ([_, v]) => v !== null && v !== "" && v !== undefined
          )
        );

        const isSearching = Object.keys(cleanForm).length > 0;

        const data = isSearching
          ? await handleSearchUsers(cleanForm)
          : await handleGetUsers();

        return {
          data,
          total: data.length,
        };
      },
    },
  },
});

// If parent passes `items`, use it as grid data and disable proxy
watch(
  () => props.items,
  (val) => {
    if (val && Array.isArray(val)) {
      // assign direct data and disable proxy so grid shows parent data
      // @ts-ignore
      gridOptions.data = val;
      // @ts-ignore
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
