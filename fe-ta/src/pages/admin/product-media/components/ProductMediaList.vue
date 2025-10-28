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
      <!-- Cột hiển thị Media -->
      <template #media="{ row }">
        <img
          v-if="row.mediaType === 'main' && row.mediaFileUrl"
          :src="row.mediaFileUrl"
          alt="Media"
          style="max-width: 100px; max-height: 100px"
        />
        <img
          v-else-if="row.mediaType === 'description' && row.mediaFileUrl"
          :src="row.mediaFileUrl"
          alt="Media"
          style="max-width: 100px; max-height: 100px"
        />
        <video
          v-else-if="row.mediaType === 'video' && row.mediaFileUrl"
          :src="row.mediaFileUrl"
          controls
          style="max-width: 150px; max-height: 100px"
        />

        <span v-else>Không có media</span>
      </template>
    </vxe-grid>
  </div>
</template>

<script setup lang="ts">
import type {
  ProductMediaResponse,
  ProductMediaFilterRequest,
} from "@/models/ProductMedia";
import productMediaApi from "@/services/productMediaApi";
import { reactive, ref, watch } from "vue";
import type {
  VxeGridProps,
  VxeGridPropTypes,
  VxeGridListeners,
} from "vxe-table";

import { Delete, Share, Edit } from "@element-plus/icons-vue";

// --- Props và Emits ---
const props = defineProps<{
  medias?: ProductMediaResponse[];
  loading?: boolean;
}>();

const emit = defineEmits<{
  (e: "edit-media", media: ProductMediaResponse): void;
  (e: "delete-media", id: number): void;
}>();

// --- Event handler ---
const handleEdit = (media: ProductMediaResponse) => {
  emit("edit-media", media);
};

const handleDelete = (id: number) => {
  emit("delete-media", id);
};

// --- Ref và reactive ---
const gridRef = ref<any>(null);
defineExpose({ gridRef });

const loading = ref(false);

const handleGetMedias = async () => {
  loading.value = true;
  try {
    const response = await productMediaApi.getAllProductMedia();
    return response;
  } catch (error) {
    console.error("Lỗi khi lấy danh sách media:", error);
    return [];
  } finally {
    loading.value = false;
  }
};

const handleSearchMedias = async (request: ProductMediaFilterRequest) => {
  loading.value = true;
  try {
    const response = await productMediaApi.filterProductMedia(request);
    return response;
  } catch (error) {
    console.error("Lỗi khi tìm kiếm media:", error);
    return [];
  } finally {
    loading.value = false;
  }
};

// --- Filter ---
const filterData = reactive<ProductMediaFilterRequest>({
  productName: undefined,
  colorName: undefined,
  mediaFileUrl: undefined,
  mediaType: undefined,
  descriptionMedia: undefined,
  isPrimary: undefined,
  createdName: "",
  updatedName: "",
  fromUpdateTime: undefined,
  toUpdateTime: undefined,
  note: undefined,
});

// --- Grid options ---
const gridOptions = reactive<
  VxeGridProps<ProductMediaResponse> & {
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
        field: "colorName",
        title: "Tên màu sắc",
        span: 6,
        itemRender: {
          name: "VxeInput",
          props: { placeholder: "Nhập tên màu sắc" },
        },
      },
      {
        field: "mediaType",
        title: "Loại media",
        span: 6,
        itemRender: {
          name: "VxeSelect",
          options: [
            { label: "Ảnh chính", value: "main" },
            { label: "Video", value: "video" },
            { label: "Ảnh mô tả", value: "description" },
           
          ],
        },
      },
      {
        field: "isPrimary",
        title: "Ảnh đại diện",
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
    { field: "productName", title: "sản phẩm", minWidth: 180, sortable: true },
    { field: "colorName", title: "Màu sắc", minWidth: 180, sortable: true },
    { field: "mediaType", title: "Loại media", minWidth: 100 , sortable: true},
    { field: "descriptionMedia", title: "Mô tả", minWidth: 150, sortable: true },
    {
      field: "isPrimary",
      title: "Ảnh chính",
      minWidth: 100,
      formatter: ({ cellValue }) => (cellValue ? "Có" : "Không"),
    },
    
    {
      field: "mediaFileUrl",
      title: "Media",
      minWidth: 150,
      slots: { default: "media" },
       sortable: true
    },
    { field: "createdName", title: "Người tạo", minWidth: 180 , sortable: true},
    { field: "updatedName", title: "Người chỉnh sửa", minWidth: 180, sortable: true },
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
          ? await handleSearchMedias(cleanForm)
          : await handleGetMedias();

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
  () => props.medias,
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
