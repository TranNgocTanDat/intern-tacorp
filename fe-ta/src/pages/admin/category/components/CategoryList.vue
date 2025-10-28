<template>
  <div class="demo-page-wrapper" style="z-index: 0">
    <vxe-grid ref="gridRef" v-bind="gridOptions" v-on="gridEvents">
      <template #actions="{ row }">
        <el-button-group class="ml-4">
          <el-button type="primary" :icon="Edit" @click="handleEdit(row)" />
          <el-button
            type="primary"
            :icon="Share"
            @click="handleViewProducts(row)"
          />
          <el-button
            type="primary"
            :icon="Delete"
            @click="handleDelete(row.id)"
          />
        </el-button-group>
      </template>
      <template #partnerLogo="{ row }">
        <img
          v-if="row.partner?.logoUrl"
          :src="row.partner.logoUrl"
          alt="Logo"
          style="
            width: 40px;
            height: 40px;
            border-radius: 8px;
            object-fit: contain;
          "
        />
        <span v-else>-</span>
      </template>
    </vxe-grid>
  </div>
</template>

<script setup lang="ts">
import { reactive, ref, watch } from "vue";
import type {
  VxeGridProps,
  VxeGridPropTypes,
  VxeGridListeners,
} from "vxe-table";

import { Delete, Edit, Share } from "@element-plus/icons-vue";
import type {
  CategoryFilterRequest,
  CategoryResponse,
} from "@/models/Category";
import type { ProductResponse } from "@/models/Product";
import categoryApi from "@/services/categoryApi";

const props = defineProps<{
  categories?: CategoryResponse[];
  loading?: boolean;
}>();

const emit = defineEmits<{
  (e: "edit-category", category: CategoryResponse): void;
  (e: "delete-category", id: number): void;
}>();

const gridRef = ref<any>(null);
defineExpose({ gridRef });

const loading = ref(false);

// Modal products
const showProductsModal = ref(false);
const selectedProducts = ref<ProductResponse[]>([]);

const handleEdit = (category: CategoryResponse) => {
  emit("edit-category", category);
};

const handleDelete = (id: number) => {
  emit("delete-category", id);
};

const handleViewProducts = (row: any) => {
  selectedProducts.value = row.products || [];
  showProductsModal.value = true;
};

const handleGetCategories = async () => {
  loading.value = true;
  try {
    const response = await categoryApi.getCategories();
    return response;
  } catch (error) {
    console.error("Lỗi khi lấy danh sách danh mục:", error);
    return [];
  } finally {
    loading.value = false;
  }
};

const handleSearchCategories = async (request: CategoryFilterRequest) => {
  loading.value = true;
  try {
    const response = await categoryApi.filterCategories(request);
    return response;
  } catch (error) {
    console.error("Lỗi khi tìm kiếm danh mục:", error);
    return [];
  } finally {
    loading.value = false;
  }
};

// Reactive filter
const filterData = reactive<CategoryFilterRequest>({
  name: "",
  description: "",
  isActive: undefined,
  parentId: undefined,
  updateTimeFrom: "",
  updateTimeTo: "",
  note: "",
});

const gridOptions = reactive<
  VxeGridProps<CategoryResponse> & {
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
    data: filterData,
    items: [
      {
        field: "name",
        title: "Tên danh mục",
        span: 6,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "Nhập tên danh mục" },
        },
      },
      {
        field: "description",
        title: "Mô tả",
        span: 12,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "Nhập mô tả", type: "textarea", rows: 2 },
        },
      },
      {
        field: "parentId",
        title: "Parent ID",
        span: 6,
        itemRender: {
          name: "VxeInputNumber",
          props: { placeholder: "Nhập ParentId" },
        },
      },
      {
        field: "updateTimeFrom",
        title: "Ngày bắt đầu",
        span: 6,
        itemRender: { name: "VxeDatePicker", props: { type: "date" } },
      },
      {
        field: "updateTimeTo",
        title: "Ngày kết thúc",
        span: 6,
        itemRender: { name: "VxeDatePicker", props: { type: "date" } },
      },
      {
        field: "isActive",
        title: "Trạng thái",
        span: 6,
        itemRender: {
          name: "VxeSelect",
          options: [
            { label: "Hoạt động", value: true },
            { label: "Ngưng", value: false },
          ],
        },
      },
      {
        field: "createdName",
        title: "Người tạo",
        span: 6,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "Nhập người tạo" },
        },
      },
      {
        field: "updatedName",
        title: "Người chỉnh sửa",
        span: 6,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "Nhập chỉnh sửa" },
        },
      },
      {
        span: 6,
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
    { field: "name", title: "Tiêu đề", minWidth: 150 },
    { field: "parentName", title: "Danh mục cha", minWidth: 150 },
    {
      field: "isActive",
      title: "Trạng thái",
      minWidth: 100,
      formatter: ({ cellValue }) => (cellValue ? "Hoạt động" : "Ngưng"),
    },
    {
      field: "partner",
      title: "Logo Hãng",
      minWidth: 120,
      slots: { default: "partnerLogo" }, // 👈 thêm slot tùy chỉnh
    },

    { field: "updateTime", title: "Cập nhật", minWidth: 130 },
    { field: "createdName", title: "Người tạo", minWidth: 120 },
    { field: "updatedName", title: "Người chỉnh sửa", minWidth: 120 },
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
        const data = isSearching
          ? await handleSearchCategories(cleanForm)
          : await handleGetCategories();
        return { data, total: data.length };
      },
    },
  },
});

watch(
  () => props.categories,
  (val) => {
    if (val && Array.isArray(val)) {
      gridOptions.data = val;
      // gridOptions.proxyConfig = undefined;
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
