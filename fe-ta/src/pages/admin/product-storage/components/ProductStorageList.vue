<template>
  <div class="demo-page-wrapper" style="z-index: 0">
    <vxe-grid ref="gridRef" v-bind="gridOptions" v-on="gridEvents">
      <!-- Cột hành động -->
      <template #actions="{ row }">
        <el-button-group>
          <el-button type="primary" :icon="Edit" @click="handleEdit(row)" />
          <el-button type="danger" :icon="Delete" @click="handleDelete(row.id)" />
        </el-button-group>
      </template>
    </vxe-grid>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from "vue";
import { Edit, Delete } from "@element-plus/icons-vue";
import type {
  VxeGridProps,
  VxeGridListeners,
  VxeGridPropTypes,
} from "vxe-table";
import type {
  ProductStorageFilterRequest,
  ProductStorageResponse,
} from "@/models/ProductStorage";
import { useProductStorageStore } from "@/store/productStorageStore";

// === Emits ===
const emit = defineEmits<{
  (e: "edit-storage", storage: ProductStorageResponse): void;
  (e: "delete-storage", id: number): void;
}>();

const handleEdit = (storage: ProductStorageResponse) => {
  emit("edit-storage", storage);
};

const handleDelete = (id: number) => {
  emit("delete-storage", id);
};

// === Store ===
const storageStore = useProductStorageStore();
const gridRef = ref<any>(null);
defineExpose({ gridRef });

// Load dữ liệu ban đầu
onMounted(async () => {
  if (storageStore.productStorages.length === 0) {
    await storageStore.getAllProductStorages();
  }
});

// === Form lọc ===
const filterData = reactive<ProductStorageFilterRequest>({
  productId: 0,
  productName: "",
  storageName: "",
  additionalPrice: undefined,
  createdName: "",
  updatedName: "",
  note: "",
});

// === Grid cấu hình ===
const gridOptions = reactive<
  VxeGridProps<ProductStorageResponse> & {
    pagerConfig: VxeGridPropTypes.PagerConfig;
  }
>({
  border: true,
  stripe: true,
  height: "100%",
  showOverflow: "title",
  formConfig: {
    titleWidth: 100,
    titleAlign: "center",
    data: filterData,
    items: [
      {
        field: "storageName",
        title: "Tên dung lượng",
        span: 6,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "Nhập dung lượng, VD: 128GB" },
        },
      },
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
        field: "isAvailable",
        title: "Trạng thái",
        span: 6,
        itemRender: {
          name: "VxeSelect",
          options: [
            { label: "Còn hàng", value: true },
            { label: "Hết hàng", value: false },
          ],
        },
      },
      {
        span: 6,
        align: "right",
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
    { type: "seq", width: 60, title: "#", align: "center" },
    { field: "productName", title: "Tên sản phẩm", minWidth: 180 },
    {
      field: "storageName",
      title: "Tên dung lượng",
      minWidth: 160,
      align: "left",
    },
    {
      field: "additionalPrice",
      title: "Giá cộng thêm (VNĐ)",
      minWidth: 160,
      align: "right",
      formatter: ({ cellValue }) =>
        cellValue ? cellValue.toLocaleString("vi-VN") : "0",
    },
    {
      field: "isAvailable",
      title: "Trạng thái",
      width: 130,
      align: "center",
      formatter: ({ cellValue }) => (cellValue ? "Còn hàng" : "Hết hàng"),
    },
    { field: "createdName", title: "Người tạo", minWidth: 150 },
    { field: "updatedName", title: "Người cập nhật", minWidth: 150 },
    {
      field: "actions",
      title: "Hành động",
      width: 120,
      align: "center",
      fixed: "right",
      slots: { default: "actions" },
    },
  ],
  pagerConfig: {
    pageSize: 10,
    pageSizes: [10, 20, 50],
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
          Object.entries(form).filter(([_, v]) => v !== null && v !== "" && v !== undefined)
        );

        const isSearching = Object.keys(cleanForm).length > 0;
        const data = isSearching
          ? (await storageStore.filterProductStorages(cleanForm as ProductStorageFilterRequest), storageStore.productStorages)
          : (await storageStore.getAllProductStorages(), storageStore.productStorages);

        return {
          data,
          total: data.length,
        };
      },
    },
  },
});

// === Sự kiện grid ===
const gridEvents: VxeGridListeners = {
  proxyQuery() {
    console.log("Đang tải danh sách ProductStorage...");
  },
};
</script>

<style scoped>
.demo-page-wrapper {
  padding: 8px;
}
</style>
