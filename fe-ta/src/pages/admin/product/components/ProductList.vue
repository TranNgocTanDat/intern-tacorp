<template>
  <div class="demo-page-wrapper" style="z-index: 0">
    <vxe-grid ref="gridRef" v-bind="gridOptions" v-on="gridEvents">
      <!-- Cột hiển thị media -->
      <template #media="{ row }">
        <img
          v-if="row.thumbnailUrl"
          :src="row.thumbnailUrl"
          alt="Product Media"
          style="max-width: 100px; max-height: 100px"
        />
        <span v-else>Chưa có ảnh</span>
      </template>

      <!-- Cột action -->
      <template #actions="{ row }">
        <el-button-group class="ml-4">
          <el-button type="primary" :icon="Edit" @click="handleEdit(row)" />
          <el-button type="primary" :icon="Share" />
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
  ProductFilterRequest,
  ProductResponse,
} from "@/models/Product"; // model Product
import productApi from "@/services/productApi"; // gọi API
import { reactive, ref, watch } from "vue";
import type {
  VxeGridProps,
  VxeGridPropTypes,
  VxeGridListeners,
} from "vxe-table";

import { Delete, Edit, Share } from "@element-plus/icons-vue";

// --- Props và Emits ---
const props = defineProps<{
  products?: ProductResponse[];
  loading?: boolean;
}>();

const emit = defineEmits<{
  (e: "edit-product", product: ProductResponse): void;
  (e: "delete-product", id: number): void;
}>();

// --- Event handler ---
const handleEdit = (product: ProductResponse) => {
  emit("edit-product", product);
};

const handleDelete = (id: number) => {
  emit("delete-product", id);
};

// --- Ref và reactive ---
const gridRef = ref<any>(null);
defineExpose({ gridRef });

const loading = ref(false);

const handleGetProducts = async () => {
  loading.value = true;
  try {
    const response = await productApi.getAllProducts();
    return response;
  } catch (error) {
    console.error("Lỗi khi lấy danh sách product:", error);
    return [];
  } finally {
    loading.value = false;
  }
};

const handleSearchProducts = async (request: ProductFilterRequest) => {
  loading.value = true;
  try {
    const response = await productApi.filterProducts(request);
    return response;
  } catch (error) {
    console.error("Lỗi khi tìm kiếm product:", error);
    return [];
  } finally {
    loading.value = false;
  }
};

// --- Filter ---
const filterData = reactive<ProductFilterRequest>({
  productName: undefined,
  categoryName: "",
  slug: undefined,
  shortDescription: undefined,
  longDescription: undefined,

  minPrice: undefined,
  maxPrice: undefined,

  isFeatured: undefined,
  isActive: undefined,

  minViewsCount: undefined,
  maxViewsCount: undefined,

  createdName: "",
  updatedName:"",

  fromUpdateTime: undefined,
  toUpdateTime: undefined,

  note: undefined,
});

// --- Grid options ---
const gridOptions = reactive<
  VxeGridProps<ProductResponse> & {
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
        field: "categoryName",
        title: "Danh mục",
        span: 6,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "Nhập danh mục" },
        },
      },
      {
        field: "priceFrom",
        title: "Giá từ",
        span: 6,
        itemRender: {
          name: "VxeInput",
          props: { type: "number", placeholder: "Giá nhỏ nhất" },
        },
      },
      {
        field: "priceTo",
        title: "Giá đến",
        span: 6,
        itemRender: {
          name: "VxeInput",
          props: { type: "number", placeholder: "Giá lớn nhất" },
        },
      },
      {
        field: "isFeatured",
        title: "Nổi bật",
        span: 6,
        itemRender: {
          name: "VxeSelect",
          options: [
            { label: "Có", value: true },
            { label: "Không", value: false },
          ],
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
    { field: "productName", title: "Tên sản phẩm", minWidth: 180 },
    { field: "categoryName", title: "Danh mục", minWidth: 140 },
    { field: "originalPrice", title: "Giá gốc", minWidth: 100 },
    { field: "discountPrice", title: "Giá sale", minWidth: 100 },
    {
      field: "isFeatured",
      title: "Nổi bật",
      minWidth: 100,
      formatter: ({ cellValue }) => (cellValue ? "Có" : "Không"),
    },
    {
      field: "thumbnailUrl",
      title: "Media",
      minWidth: 150,
      slots: { default: "media" },
    },
    { field: "viewsCount", title: "Lượt xem", minWidth: 100 },  
    { field: "createdName", title: "Người tạo", minWidth: 180 },
    { field: "updatedName", title: "Người chỉnh sửa", minWidth: 180 },
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
          ? await handleSearchProducts(cleanForm)
          : await handleGetProducts();

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
  () => props.products,
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
