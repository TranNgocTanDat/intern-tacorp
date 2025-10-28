<template>
  <div class="demo-page-wrapper" style="z-index: 0">
    <vxe-grid ref="gridRef" v-bind="gridOptions" v-on="gridEvents">
      <!-- Cột hành động -->
      <template #actions="{ row }">
        <el-button-group class="ml-4">
          <el-button type="primary" :icon="Edit" @click="handleEdit(row)" />
          <el-button
            type="danger"
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
  ProductSpecResponse,
  ProductSpecFilterRequest,
} from "@/models/ProductSpec";
import productSpecApi from "@/services/productSpecApi";
import { reactive, ref, watch } from "vue";
import type {
  VxeGridProps,
  VxeGridPropTypes,
  VxeGridListeners,
} from "vxe-table";

import { Delete, Edit } from "@element-plus/icons-vue";

// --- Props và Emits ---
const props = defineProps<{
  specs?: ProductSpecResponse[];
  loading?: boolean;
}>();

const emit = defineEmits<{
  (e: "edit-spec", spec: ProductSpecResponse): void;
  (e: "delete-spec", id: number): void;
}>();

// --- Event handler ---
const handleEdit = (spec: ProductSpecResponse) => {
  emit("edit-spec", spec);
};

const handleDelete = (id: number) => {
  emit("delete-spec", id);
};

// --- Ref và reactive ---
const gridRef = ref<any>(null);
defineExpose({ gridRef });

const loading = ref(false);

// --- API ---
const handleGetSpecs = async () => {
  loading.value = true;
  try {
    const response = await productSpecApi.getProductSpecs();
    return Array.isArray(response) ? response : [response];
  } catch (error) {
    console.error("Lỗi khi lấy danh sách spec:", error);
    return [];
  } finally {
    loading.value = false;
  }
};

const handleSearchSpecs = async (request: ProductSpecFilterRequest) => {
  loading.value = true;
  try {
    const response = await productSpecApi.filterProductSpecs(request);
    return response;
  } catch (error) {
    console.error("Lỗi khi tìm kiếm spec:", error);
    return [];
  } finally {
    loading.value = false;
  }
};

// --- Filter ---
const filterData = reactive<ProductSpecFilterRequest>({
  productName: undefined,
  specKey: undefined,
  specValue: undefined,
  orderIndex: undefined,
  createdName: "",
  updatedName: "",
  fromUpdateTime: undefined,
  toUpdateTime: undefined,
  note: undefined,
});

// --- Grid options ---
const gridOptions = reactive<
  VxeGridProps<ProductSpecResponse> & {
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
        field: "productName",
        title: "Tên sản phẩm",
        span: 6,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "Nhập tên sản phẩm" },
        },
      },
      {
        field: "specKey",
        title: "Tên thông số",
        span: 6,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "Nhập tên thông số" },
        },
      },
      {
        field: "specValue",
        title: "Giá trị",
        span: 6,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "Nhập giá trị" },
        },
      },
      {
        field: "createdName",
        title: "Người tạo",
        span: 6,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "Nhập tên người tạo" },
        },
      },
      {
        field: "updatedName",
        title: "Người chỉnh sửa",
        span: 6,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "Nhập tên người chỉnh sửa" },
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
    { field: "productName", title: "Sản phẩm", minWidth: 120 },
    { field: "specKey", title: "Thông số", minWidth: 120 },
    { field: "specValue", title: "Giá trị", minWidth: 150 },
    { field: "orderIndex", title: "Thứ tự", minWidth: 100 },
    { field: "note", title: "Ghi chú", minWidth: 150 },
    { field: "createdName", title: "Người tạo", minWidth: 180 },
    { field: "updatedName", title: "Người chỉnh sửa", minWidth: 180 },
    {
      field: "actions",
      title: "Hành động",
      width: 150,
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
          ? await handleSearchSpecs(cleanForm)
          : await handleGetSpecs();

        return {
          data,
          total: data.length,
        };
      },
    },
  },
});

// --- Watch khi props truyền xuống ---
watch(
  () => props.specs,
  (val) => {
    if (val && Array.isArray(val)) {
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
