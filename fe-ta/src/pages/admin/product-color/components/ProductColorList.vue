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

      <!-- Cột hiển thị màu -->
      <template #color="{ row }">
        <div class="color-box">
          <div
            v-if="row.colorCode"
            class="color-circle"
            :style="{ backgroundColor: row.colorCode }"
          ></div>
          <span>{{ row.colorName }}</span>
        </div>
      </template>

      <!-- Cột hình ảnh -->
      <template #media="{ row }">
        <img
          v-if="row.mediaList?.length"
          :src="row.mediaList[0].mediaFileUrl"
          alt="Ảnh màu"
          style="max-width: 60px; max-height: 60px; object-fit: cover"
        />
        <span v-else>Không có ảnh</span>
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
  ProductColorFilterRequest,
  ProductColorResponse,
} from "@/models/ProductColor";
import { useProductColorStore } from "@/store/productColorStore";

// === Emits ===
const emit = defineEmits<{
  (e: "edit-color", color: ProductColorResponse): void;
  (e: "delete-color", id: number): void;
}>();

const handleEdit = (color: ProductColorResponse) => {
  emit("edit-color", color);
};

const handleDelete = (id: number) => {
  emit("delete-color", id);
};

// === Store ===
const colorStore = useProductColorStore();
const gridRef = ref<any>(null);
defineExpose({ gridRef });

// Load dữ liệu ban đầu
onMounted(async () => {
  if (colorStore.productColors.length === 0) {
    await colorStore.getAllProductColors();
  }
});

// === Form lọc ===
const filterData = reactive<ProductColorFilterRequest>({
  productId: 0,
  productName: "",
  colorName: "",
  colorCode: "",
  isAvailable: undefined,
  createdName: "",
  updatedName: "",
  note: "",
});

// === Grid cấu hình ===
const gridOptions = reactive<
  VxeGridProps<ProductColorResponse> & {
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
        field: "colorName",
        title: "Tên màu",
        span: 6,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "Nhập tên màu" },
        },
      },
      {
        field: "colorCode",
        title: "Mã màu",
        span: 6,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "VD: #FF0000" },
        },
      },
      {
        field: "isAvailable",
        title: "Trạng thái",
        span: 6,
        itemRender: {
          name: "VxeSelect",
          options: [
            { label: "Hiển thị", value: true },
            { label: "Ẩn", value: false },
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
      field: "colorName",
      title: "Tên màu",
      minWidth: 160,
      align: "left",
      slots: { default: "color" },
    },
    {
      field: "colorCode",
      title: "Mã màu",
      minWidth: 120,
      align: "center",
    },
    {
      field: "mediaList",
      title: "Ảnh minh họa",
      width: 120,
      align: "center",
      slots: { default: "media" },
    },
    {
      field: "isAvailable",
      title: "Trạng thái",
      width: 130,
      align: "center",
      formatter: ({ cellValue }) => (cellValue ? "Hiển thị" : "Ẩn"),
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
          ? (await colorStore.filterProductColors(cleanForm as ProductColorFilterRequest), colorStore.productColors)
          : (await colorStore.getAllProductColors(), colorStore.productColors);

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
    console.log("Đang tải danh sách ProductColor...");
  },
};
</script>

<style scoped>
.color-box {
  display: flex;
  align-items: center;
  gap: 8px;
}
.color-circle {
  width: 18px;
  height: 18px;
  border-radius: 50%;
  border: 1px solid #ccc;
}
</style>
